using System.Diagnostics.CodeAnalysis;
using Moq;
using StrategyPatternWithDIExamples.Strategy.Context;
using StrategyPatternWithDIExamples.Strategy;


namespace StrategyPatternWithDIExamples.Tests
{
    public class StrategyContextTests
    {
        private readonly Mock<IStrategy> _mockStrategy;
        private readonly StrategyContext _strategyContext;

        public StrategyContextTests()
        {
            _mockStrategy = new Mock<IStrategy>();
            _strategyContext = new StrategyContext(new List<IStrategy> { _mockStrategy.Object });
        }

        [Fact]
        public void ExecuteStrategy_Should_Return_Empty_When_Strategy_Not_Found()
        {
            _mockStrategy.Setup(x => x.Name).Returns("DifferentStrategy");

            var result = _strategyContext.ExecuteStrategy("TestStrategy", "message");

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void ExecuteStrategy_Should_Execute_Strategy_When_Found()
        {
            _mockStrategy.Setup(x => x.Name).Returns("TestStrategy");
            _mockStrategy.Setup(x => x.Execute(It.IsAny<string>())).Returns("test");

            var result = _strategyContext.ExecuteStrategy("TestStrategy", "message");

            Assert.Equal("test", result);
        }
    }

}
