namespace SimpleBankingApi.Utilities;

public static class AccountNumberGenerator
{
    private static readonly Random _random = new Random();
    public static string GenerateAcct()
    {
        Random random = new Random();
        long digits = random.NextInt64(1000000000,10000000000);
        return digits.ToString();
    }
}