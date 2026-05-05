public class QuoteEntry : Entry
{
    public required string Author { get; set; }
    public override void Display() => Console.WriteLine($"Quote by {Author}: {Content}");
    public override string Summarize() => $"Quote: {Content}";
}
