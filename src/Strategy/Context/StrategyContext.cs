namespace StrategyPatternWithDIExamples.Strategy.Context
{
    public class StrategyContext(IEnumerable<IStrategy> strategies) : IStrategyContext
    {
        public string ExecuteStrategy(
            string strategyName, 
            string message)
        {
            var instance = strategies.FirstOrDefault(x =>
                x.Name.Equals(strategyName, StringComparison.InvariantCultureIgnoreCase));

            return instance is not null ?
                instance.Execute(message) :
                string.Empty;
        }
    }
}
