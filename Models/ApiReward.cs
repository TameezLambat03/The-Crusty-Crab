namespace CrustyCrab.Models;

public class ApiReward
{
    public int CustomerRewardID { get; set; }
    public int OrderID { get; set; }
    public string PrizeName { get; set; } = string.Empty;
    public decimal DiscountPercent { get; set; }
    public string? RewardCode { get; set; }
    public DateTime WonAtUtc { get; set; }
    public DateTime? ExpiresAtUtc { get; set; }
    public bool IsRedeemed { get; set; }
}