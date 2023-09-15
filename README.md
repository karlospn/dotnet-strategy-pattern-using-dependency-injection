# How to build a Strategy pattern using dependency injection with .NET

This repository contains an example that shows how easy is to build a Strategy pattern using dependency injection with .NET

# Components

![strategy-pattern](https://raw.githubusercontent.com/karlospn/dotnet-strategy-pattern-using-dependency-injection/main/docs/strategy.png)

The classes and objects participating in this pattern include:

- **Strategy  (IStrategy)**

Declares a common interface to all concrete implementation must implement. 

```csharp
public interface IStrategy
{
    string Name { get; }
    string Execute(string message);
}
```

- **ConcreteStrategy  (ReverseStrategy, ToLowerStrategy, ToUpperStrategy)**

Every concrete strategy implements a single behaviour using the **IStrategy** interface


```csharp
 public class ToUpperStrategy : IStrategy
{
    public string Name => nameof(ToUpperStrategy);
    public string Execute(string message)
    {
        return message.ToUpper();
    }
}
```

- **Context  (StrategyContext)**

Uses the ctor injection to obtain an **IEnumerable** that contain every **IStrategy** implementation.    
It also has a method to execute a concrete **IStrategy** implementation.


```csharp
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
```

## **How to register the strategies into the DI container**

The easiest way to register every strategy behavior in our DI (Dependency Injection) container is to do it like this:

```csharp
builder.Services.AddTransient<IStrategy, ToUpperStrategy>();
builder.Services.AddTransient<IStrategy, ToLowerStrategy>();
builder.Services.AddTransient<IStrategy, ReverseStrategy>();
```

However, every time we want to add a new behavior to our Strategy pattern, we must remember to register it. 

A better option is to use a bit of Reflection. This way, we can create as many behaviors as we want, and they will always be registered in the DI container.

```csharp
var strategies = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(IStrategy).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

foreach (var strategy in strategies)
{
    builder.Services.AddTransient(
        typeof(IStrategy), 
        strategy);
}
```

## How does it work

It's really simple.    
- Register every Strategy behaviour (which implements the same interface).

```csharp
builder.Services.AddTransient<IStrategy, ToUpperStrategy>();
builder.Services.AddTransient<IStrategy, ToLowerStrategy>();
builder.Services.AddTransient<IStrategy, ReverseStrategy>();
```

- In the ``StrategyContext`` class, the multiple ``IStrategy`` implementations we have registered are resolved into  an ``IEnumerable<IStrategy>``.

- Now, we can filter the collection of ``IStrategy`` to execute the desired behaviour.

```csharp
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
```