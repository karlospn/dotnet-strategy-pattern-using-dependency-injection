using Microsoft.AspNetCore.Mvc;
using StrategyPatternWithDIExamples.Strategy.Context;

namespace StrategyPatternWithDIExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StrategiesController(IStrategyContext strategyContext) : ControllerBase
    {
        [HttpGet()]
        public ActionResult Get(string strategyName, string message)
        {
            var result = strategyContext.ExecuteStrategy(strategyName, message);
            
            if (string.IsNullOrEmpty(result))
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}