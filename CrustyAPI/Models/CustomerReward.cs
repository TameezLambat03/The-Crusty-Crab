
namespace CrustyAPI.Models;

public class CustomerReward
{
    public int CustomerRewardID { get; set; }

    public int CustomerID { get; set; }

    public int OrderID { get; set; }

    // The prize selected by the API, never by the browser.
    public string PrizeName { get; set; } = string.Empty;

    // For example: 10 for a 10% discount, or 0 for no prize.
    public decimal DiscountPercent { get; set; }

    // Unique code used when redeeming a winning reward.
    public string? RewardCode { get; set; }

    public DateTime WonAtUtc { get; set; }

    public DateTime? ExpiresAtUtc { get; set; }

    public bool IsRedeemed { get; set; }

    public DateTime? RedeemedAtUtc { get; set; }
}
