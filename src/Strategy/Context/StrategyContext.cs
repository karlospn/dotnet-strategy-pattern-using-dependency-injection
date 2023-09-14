namespace StrategyPatternWithDIExamples.Strategy.Context
{
    public class StrategyContext : IStrategyContext
    {
        private readonly IEnumerable<IStrategy> _strategies;
        public StrategyContext(IEnumerable<IStrategy> strategies)
        {
            _strategies = strategies;
        }

        public string ExecuteStrategy(
            string strategyName, 
            string message)
        {
            var instance = _strategies.FirstOrDefault(x =>
                x.Name.Equals(strategyName, StringComparison.InvariantCultureIgnoreCase));

            return instance is not null ?
                instance.Execute(message) :
                string.Empty;
        }
    }
}
