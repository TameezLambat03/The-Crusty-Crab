using System.ComponentModel.DataAnnotations;

namespace CrustyAPI.DTOs;

public class CreatePaymentMethodRequest
{
    [Required]
    public string MethodName { get; set; } = string.Empty;
}

public class CreatePaymentRequest
{
    [Required]
    public int OrderID { get; set; }

    [Required]
    public int PaymentMethodID { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }
}

public class UpdatePaymentStatusRequest
{
    [Required]
    public string PaymentStatus { get; set; } = string.Empty;
}

public class PaymentMethodResponse
{
    public int PaymentMethodID { get; set; }

    public string MethodName { get; set; } = string.Empty;
}

public class PaymentResponse
{
    public int PaymentID { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public string PaymentStatus { get; set; } = string.Empty;

    public int PaymentMethodID { get; set; }

    public string MethodName { get; set; } = string.Empty;

    public int OrderID { get; set; }
}