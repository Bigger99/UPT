using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Refit;
using UPT.Data.SeedData;
using UPT.Features.Features.AutorizationFeatures.Requests;
using UPT.Features.Features.ClientFeatures.Requests;
using UPT.Features.Features.UserFeatures.Requests;
using UPT.Infrastructure.Models;
using UPT.Tests.API.Frontend.AuthorizeTests.Base;
using UPT.Tests.API.Frontend.ClientTests.Base;
using UPT.Tests.API.Frontend.UserTests.Base;

namespace UPT.Tests.API.Frontend.ClientTests;

internal class ClientTests : ApiBaseTests<IClientProvider>
{
    [Test]
    public async Task Get_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // act
        var response = await Provider.Get(1);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.VolumeButtock.Should().NotBe(0);
    }

    [Test]
    public async Task GetAll_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // act
        var response = await Provider.GetAll();

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.Count.Should().NotBe(0);
    }

    [Test]
    public async Task GetByUserId_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // act
        var response = await Provider.GetByUserId(1);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.VolumeButtock.Should().NotBe(0);
    }

    [Test]
    public async Task GetFiltered_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<PagedFilterQuery<FilteredClientRequest>>()
            .Create();

        // act
        var response = await Provider.GetFiltered(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.Count().Should().NotBe(0);
    }

    [Ignore("")]
    [Test]
    public async Task Create_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var authorizeProvider = RestService.For<IAuthorizeProvider>(AppFactory.CreateClient());
        var requestModel = Fixture.Build<RegisterCommand>()
            .With(x => x.EmailAddress, SeedTestsConsts.DefaultEmailForTests)
            .With(x => x.Password, SeedTestsConsts.DefaultPassword)
            .Create();
        await authorizeProvider.Register(requestModel);


        var userProvider = RestService.For<IUserProvider>(AppFactory.CreateClient());
        var requestModelGetByEmailQuery = Fixture.Build<GetByEmailQuery>()
            .With(x => x.EmailAddress, SeedTestsConsts.DefaultEmailForTests)
            .Create();
        var responseUser = await userProvider.GetByEmail(requestModelGetByEmailQuery);


        var createRequestModel = Fixture.Build<CreateClientCommand>()
            .With(x => x.UserId, responseUser.Content!.Id)
            .Create();

        // act
        var response = await Provider.Create(createRequestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content!.Should().NotBe(0);
    }

    [Test]
    public async Task Update_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<UpdateClientCommand>()
            .With(x => x.ClientId, 1)
            .Create();

        // act
        var response = await Provider.Update(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
    }

    [Test]
    public async Task SetTrainer_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<SetClientTrainerCommand>()
            .With(x => x.ClientId, 1)
            .With(x => x.TrainerId, 1)
            .Create();

        // act
        var response = await Provider.SetTrainer(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.Height.Should().NotBe(0);
    }

    [Test]
    public async Task Delete_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // act
        var response = await Provider.Delete(1);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
