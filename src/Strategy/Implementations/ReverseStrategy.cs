namespace StrategyPatternWithDIExamples.Strategy.Implementations
{
    public class ReverseStrategy : IStrategy
    {
        public string Name => nameof(ReverseStrategy);

        public string Execute(string message)
        {
            var charArray = message.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
