
using CrustyAPI.Data;
using CrustyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CrustyAPI.Services;

public class RewardService
{
    private readonly AppDbContext _context;

    public RewardService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RewardSpinResult> SpinAsync(int customerId, int orderId)
    {
        // The same order can never receive a second spin.
        var existingReward = await _context.CustomerRewards
            .FirstOrDefaultAsync(r =>
                r.CustomerID == customerId &&
                r.OrderID == orderId);

        if (existingReward != null)
        {
            return RewardSpinResult.FromReward(existingReward);
        }

        var order = await _context.Orders
            .FirstOrDefaultAsync(o =>
                o.OrderID == orderId &&
                o.CustomerID == customerId);

        if (order == null)
        {
            throw new RewardException(
                "Order not found for this customer.");
        }

        if (string.Equals(
                order.OrderStatus,
                "Cancelled",
                StringComparison.OrdinalIgnoreCase))
        {
            throw new RewardException(
                "Cancelled orders are not eligible for a reward spin.");
        }

        var isPaid = await _context.Payments.AnyAsync(p =>
            p.OrderID == orderId &&
            p.PaymentStatus == "Paid");

        if (!isPaid)
        {
            throw new RewardException(
                "This order must have a paid payment before spinning.");
        }

        // Prize selection happens on the server, not in JavaScript.
        var prize = SelectPrize();

        var reward = new CustomerReward
        {
            CustomerID = customerId,
            OrderID = orderId,
            PrizeName = prize.Name,
            DiscountPercent = prize.DiscountPercent,
            RewardCode = prize.DiscountPercent > 0
                ? $"CRAB-{Guid.NewGuid():N}".ToUpperInvariant()[..17]
                : null,
            WonAtUtc = DateTime.UtcNow,
            ExpiresAtUtc = prize.DiscountPercent > 0
                ? DateTime.UtcNow.AddDays(30)
                : null,
            IsRedeemed = false
        };

        _context.CustomerRewards.Add(reward);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            // Another request may have saved this order's spin first.
            // The unique OrderID index prevents a second reward.
            var savedReward = await _context.CustomerRewards
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.OrderID == orderId);

            if (savedReward != null &&
                savedReward.CustomerID == customerId)
            {
                return RewardSpinResult.FromReward(savedReward);
            }

            throw;
        }

        return RewardSpinResult.FromReward(reward);
    }

    private static PrizeOption SelectPrize()
    {
        // Weights total 100.
        // Adjust these odds to suit your business.
        var roll = Random.Shared.Next(100);

        return roll switch
        {
            < 45 => new PrizeOption("No prize this time", 0),
            < 75 => new PrizeOption("5% Off", 5),
            < 90 => new PrizeOption("10% Off", 10),
            < 98 => new PrizeOption("15% Off", 15),
            _ => new PrizeOption("20% Off", 20)
        };
    }

    private sealed record PrizeOption(
        string Name,
        decimal DiscountPercent);
}

public sealed class RewardSpinResult
{
    public int CustomerRewardID { get; init; }
    public int OrderID { get; init; }
    public string PrizeName { get; init; } = string.Empty;
    public decimal DiscountPercent { get; init; }
    public string? RewardCode { get; init; }
    public DateTime WonAtUtc { get; init; }
    public DateTime? ExpiresAtUtc { get; init; }
    public bool IsRedeemed { get; init; }

    public static RewardSpinResult FromReward(CustomerReward reward)
    {
        return new RewardSpinResult
        {
            CustomerRewardID = reward.CustomerRewardID,
            OrderID = reward.OrderID,
            PrizeName = reward.PrizeName,
            DiscountPercent = reward.DiscountPercent,
            RewardCode = reward.RewardCode,
            WonAtUtc = reward.WonAtUtc,
            ExpiresAtUtc = reward.ExpiresAtUtc,
            IsRedeemed = reward.IsRedeemed
        };
    }
}

public sealed class RewardException : Exception
{
    public RewardException(string message) : base(message)
    {
    }
}
