namespace StrategyPatternWithDIExamples.Strategy.Implementations
{
    public class ToUpperStrategy : IStrategy
    {
        public string Name => nameof(ToUpperStrategy);
        public string Execute(string message)
        {
            return message.ToUpper();
        }
    }
}
