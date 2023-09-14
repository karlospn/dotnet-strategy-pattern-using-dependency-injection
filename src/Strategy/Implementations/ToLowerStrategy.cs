namespace StrategyPatternWithDIExamples.Strategy.Implementations
{
    public class ToLowerStrategy : IStrategy
    {
        public string Name => nameof(ToLowerStrategy);
        public string Execute(string message)
        {
            return message.ToLower();
        }
    }
}
