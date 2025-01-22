using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using UPT.Data.SeedData;
using UPT.Features.Features.UserFeatures.Requests;
using UPT.Tests.API.Frontend.UserTests.Base;

namespace UPT.Tests.API.Frontend.UserTests;

internal class UserTests : ApiBaseTests<IUserProvider>
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
    }

    [Test]
    public async Task GetByEmail_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<GetByEmailQuery>()
            .With(x => x.EmailAddress, SeedTestsConsts.DefaultEmail)
            .Create();

        // act
        var response = await Provider.GetByEmail(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Test]
    public async Task Update_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<UpdateUserCommand>()
            .With(x => x.Id, 1)
            .Create();

        // act
        var response = await Provider.Update(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Test]
    public async Task EmailConfirmed_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<ConfirmeEmailCommand>()
            .With(x => x.UserId, 1)
            .Create();

        // act
        var response = await Provider.EmailConfirmed(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
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
