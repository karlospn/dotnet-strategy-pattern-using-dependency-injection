namespace StrategyPatternWithDIExamples.Strategy
{
    public interface IStrategy
    {
        string Name { get; }
        string Execute(string message);
    }
}
