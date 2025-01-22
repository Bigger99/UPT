using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using UPT.Common;

namespace UPT.Tests.API;

internal class ApiBaseTests<TProvider>() : TestsBase(ConfigureServices)
{
    [TestDependency(shouldRegister: true)]
    protected readonly PgFixture PgFixture = default!;

    [TestDependency(shouldRegister: true)]
    protected readonly Fixture Fixture = default!;

    protected AppFactory AppFactory => GetScoped<AppFactory>();

    protected TProvider Provider = default!;

    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        Fixture.Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));
        await PgFixture.Start();
    }

    [SetUp]
    public async Task Setup()
    {
        await using var ctx = PgFixture.BuildDataContext();
        await ctx.Database.EnsureDeletedAsync();
        await ctx.Database.EnsureCreatedAsync();
        Provider = RestService.For<TProvider>(AppFactory.CreateClient());
    }

    private static void ConfigureServices(IServiceCollection serviceCollection) => serviceCollection.AddScoped<AppFactory>();

}
