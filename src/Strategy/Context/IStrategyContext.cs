namespace StrategyPatternWithDIExamples.Strategy.Context;

public interface IStrategyContext
{
    string ExecuteStrategy(
        string strategyName,
        string message);
}