public class MemoryEntry : Entry
{
    public required string Image { get; set; }
    public override void Display() => Console.WriteLine($"Memory: {Content} (Image: {Image})");
    public override string Summarize() => $"Memory: {Content}";
}
