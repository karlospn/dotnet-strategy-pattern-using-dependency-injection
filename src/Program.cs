
using StrategyPatternWithDIExamples.Strategy;
using StrategyPatternWithDIExamples.Strategy.Context;

namespace StrategyPatternWithDIExamples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var strategies = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(IStrategy).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

            foreach (var strategy in strategies)
            {
                builder.Services.AddTransient(
                    typeof(IStrategy), 
                    strategy);
            }

            builder.Services.AddTransient<IStrategyContext, StrategyContext>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}