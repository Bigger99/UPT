using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace UPT.Common;

public class TestsBase
{
    private readonly Action<IServiceCollection> _additionalServicesConfiguration;
    private readonly ConcurrentDictionary<string, AsyncServiceScope> _scopes = new();
    private ServiceProvider _serviceProvider;

    protected TestsBase(Action<IServiceCollection>? configureServices = null)
    {
        _additionalServicesConfiguration = configureServices ?? (_ => { });
    }

    [OneTimeSetUp]
    protected void SetUpContainer()
    {
        var serviceCollection = new ServiceCollection();
        RegisterTestDependencies(serviceCollection);
        _additionalServicesConfiguration(serviceCollection);
        _serviceProvider = serviceCollection.BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
        InitializeTestDependencies();
    }

    [OneTimeTearDown]
    protected async Task DisposeContainer()
    {
        await _serviceProvider.DisposeAsync();
    }

    [SetUp]
    protected void CreateScope()
    {
        _scopes.GetOrAdd(ScopeKey, _ => _serviceProvider.CreateAsyncScope());
    }

    [TearDown]
    protected async Task DisposeScope()
    {
        if (_scopes.TryGetValue(ScopeKey, out var scope))
        {
            await scope.DisposeAsync();
        }
    }

    private string ScopeKey => TestContext.CurrentContext.Test.ID;

    protected T GetScoped<T>() where T : notnull
    {
        if (!_scopes.TryGetValue(ScopeKey, out var scope))
        {
            throw new ArgumentException($"Scope for key {ScopeKey} is not created");
        }

        return scope.ServiceProvider.GetRequiredService<T>();
    }

    private void RegisterTestDependencies(IServiceCollection serviceCollection)
    {
        foreach (var field in GetTypeFields())
        {
            var testDepAttr = GetTestDependencyAttributeOrDefault(field);
            if (testDepAttr is not null && (testDepAttr as TestDependencyAttribute)!.ShouldRegister)
            {
                serviceCollection.AddSingleton(field.FieldType);
            }
        }
    }

    private void InitializeTestDependencies()
    {
        foreach (var field in GetTypeFields())
        {
            var testDepAttr = GetTestDependencyAttributeOrDefault(field);
            if (testDepAttr is not null)
            {
                field.SetValue(this, _serviceProvider.GetRequiredService(field.FieldType));
            }
        }
    }

    private FieldInfo[] GetTypeFields() => GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

    private static Attribute? GetTestDependencyAttributeOrDefault(FieldInfo field) => field.GetCustomAttributes(typeof(TestDependencyAttribute)).SingleOrDefault();
}
