namespace SimpleBankingApi.DTOs.Responses;

public class BalanceResponse
{
    public string AccountNumber { get; set; }
    public string CustomerName { get; set; }
    public decimal Balance { get; set; }
}