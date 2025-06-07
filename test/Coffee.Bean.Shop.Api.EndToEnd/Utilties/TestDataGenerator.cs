namespace Coffee.Bean.Shop.Api.EndToEnd.Utilties;

public static class TestDataGenerator
{
    public static readonly Random Random = new Random();

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        return new string(Enumerable.Repeat(chars, length)
        .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    public static string RandomSentence(int wordCount)
    {
        return string.Join(" ", Enumerable.Range(0, wordCount)
        .Select(_ => RandomString(Random.Next(4, 10))));
    }

    public static string RandomImageUrl()
    {
        return $"https://example.com/{RandomString(8)}.jpg";
    }

    public static decimal RandomPrice()
    {
        return Math.Round((decimal)(Random.NextDouble() * (50.00 - 5.00) + 5.00), 2);
    }
}
