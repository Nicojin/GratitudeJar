public class MilestoneEntry : Entry
{
    public required string Milestone { get; set; }
    public override void Display() => Console.WriteLine($"Milestone: {Milestone} - {Content}");
    public override string Summarize() => $"Milestone: {Milestone}";
}
