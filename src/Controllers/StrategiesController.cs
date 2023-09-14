using Microsoft.AspNetCore.Mvc;
using StrategyPatternWithDIExamples.Strategy.Context;

namespace StrategyPatternWithDIExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StrategiesController : ControllerBase
    {
        private readonly IStrategyContext _strategyContext;
        
        public StrategiesController(IStrategyContext strategyContext)
        {
            _strategyContext = strategyContext;
        }

        [HttpGet()]
        public ActionResult Get(string strategyName, string message)
        {
            var result = _strategyContext.ExecuteStrategy(strategyName, message);
            
            if (string.IsNullOrEmpty(result))
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}