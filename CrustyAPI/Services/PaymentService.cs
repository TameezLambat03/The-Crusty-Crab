using CrustyAPI.Data;
using CrustyAPI.DTOs;
using CrustyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CrustyAPI.Services;

public class PaymentService
{
    private readonly AppDbContext _context;

    public PaymentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PaymentMethodResponse>> GetPaymentMethods()
    {
        return await _context.PaymentMethods
            .Select(pm => new PaymentMethodResponse
            {
                PaymentMethodID = pm.PaymentMethodID,
                MethodName = pm.MethodName
            })
            .ToListAsync();
    }

    public async Task<PaymentMethodResponse?> GetPaymentMethodById(int id)
    {
        return await _context.PaymentMethods
            .Where(pm => pm.PaymentMethodID == id)
            .Select(pm => new PaymentMethodResponse
            {
                PaymentMethodID = pm.PaymentMethodID,
                MethodName = pm.MethodName
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PaymentMethodResponse> CreatePaymentMethod(
        CreatePaymentMethodRequest request)
    {
        var paymentMethod = new PaymentMethod
        {
            MethodName = request.MethodName
        };

        _context.PaymentMethods.Add(paymentMethod);
        await _context.SaveChangesAsync();

        return new PaymentMethodResponse
        {
            PaymentMethodID = paymentMethod.PaymentMethodID,
            MethodName = paymentMethod.MethodName
        };
    }

    public async Task<bool> DeletePaymentMethod(int id)
    {
        var paymentMethod = await _context.PaymentMethods
            .FirstOrDefaultAsync(pm => pm.PaymentMethodID == id);

        if (paymentMethod == null)
        {
            return false;
        }

        _context.PaymentMethods.Remove(paymentMethod);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<PaymentResponse?> CreatePayment(
        CreatePaymentRequest request)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.OrderID == request.OrderID);

        if (order == null)
        {
            return null;
        }

        var paymentMethod = await _context.PaymentMethods
            .FirstOrDefaultAsync(pm =>
                pm.PaymentMethodID == request.PaymentMethodID);

        if (paymentMethod == null)
        {
            return null;
        }

        var payment = new Payment
        {
            OrderID = request.OrderID,
            PaymentMethodID = request.PaymentMethodID,
            Amount = request.Amount,
            PaymentStatus = "Pending",
            PaymentDate = DateTime.UtcNow
        };

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return await BuildPaymentResponse(payment);
    }

    public async Task<PaymentResponse?> GetPaymentById(int id)
    {
        var payment = await _context.Payments
            .FirstOrDefaultAsync(p => p.PaymentID == id);

        if (payment == null)
        {
            return null;
        }

        return await BuildPaymentResponse(payment);
    }

    public async Task<List<PaymentResponse>> GetPaymentsByOrder(int orderId)
    {
        var payments = await _context.Payments
            .Where(p => p.OrderID == orderId)
            .OrderByDescending(p => p.PaymentDate)
            .ToListAsync();

        var responses = new List<PaymentResponse>();

        foreach (var payment in payments)
        {
            responses.Add(await BuildPaymentResponse(payment));
        }

        return responses;
    }

    public async Task<PaymentResponse?> UpdatePaymentStatus(
        int paymentId,
        UpdatePaymentStatusRequest request)
    {
        var payment = await _context.Payments
            .FirstOrDefaultAsync(p => p.PaymentID == paymentId);

        if (payment == null)
        {
            return null;
        }

        payment.PaymentStatus = request.PaymentStatus;

        await _context.SaveChangesAsync();

        return await BuildPaymentResponse(payment);
    }

    private async Task<PaymentResponse> BuildPaymentResponse(
        Payment payment)
    {
        var paymentMethod = await _context.PaymentMethods
            .FirstAsync(pm =>
                pm.PaymentMethodID == payment.PaymentMethodID);

        return new PaymentResponse
        {
            PaymentID = payment.PaymentID,
            PaymentDate = payment.PaymentDate,
            Amount = payment.Amount,
            PaymentStatus = payment.PaymentStatus,
            PaymentMethodID = payment.PaymentMethodID,
            MethodName = paymentMethod.MethodName,
            OrderID = payment.OrderID
        };
    }
}