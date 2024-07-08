using Moq;
using StrategyPatternWithDIExamples.Strategy.Context;
using StrategyPatternWithDIExamples.Strategy.Implementations;
using StrategyPatternWithDIExamples.Strategy;

namespace StrategyPatternWithDIExamples.Tests
{
    public class StrategyTests
    {
        private readonly Mock<IStrategy> _mockStrategy;
        private readonly StrategyContext _strategyContext;

        public StrategyTests()
        {
            _mockStrategy = new Mock<IStrategy>();
            _strategyContext = new StrategyContext(new List<IStrategy> { _mockStrategy.Object });
        }

        [Fact]
        public void ReverseStrategy_Should_Reverse_String()
        {
            var strategy = new ReverseStrategy();
            var result = strategy.Execute("hello");
            Assert.Equal("olleh", result);
        }

        [Fact]
        public void ToLowerStrategy_Should_Convert_To_Lowercase()
        {
            var strategy = new ToLowerStrategy();
            var result = strategy.Execute("HELLO");
            Assert.Equal("hello", result);
        }

        [Fact]
        public void ToUpperStrategy_Should_Convert_To_Uppercase()
        {
            var strategy = new ToUpperStrategy();
            var result = strategy.Execute("hello");
            Assert.Equal("HELLO", result);
        }

        [Fact]
        public void StrategyContext_Should_Execute_Strategy()
        {
            _mockStrategy.Setup(x => x.Name).Returns("TestStrategy");
            _mockStrategy.Setup(x => x.Execute(It.IsAny<string>())).Returns("test");

            var result = _strategyContext.ExecuteStrategy("TestStrategy", "message");

            Assert.Equal("test", result);
        }
    }
}