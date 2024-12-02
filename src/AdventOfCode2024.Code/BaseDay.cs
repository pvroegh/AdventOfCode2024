namespace AdventOfCode2024.Code;

public abstract class BaseDay
{
    protected BaseDay(string? fileName = null)
    {
        Input = File.ReadAllLines($"Inputs/{fileName ?? GetType().Name}.txt");
    }

    protected string[] Input { get; set; }

    public virtual string Solution1() => "No solution 1 yet.";
    public virtual string Solution2() => "No solution 2 yet.";
}