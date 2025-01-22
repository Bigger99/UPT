using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using UPT.Data.SeedData;
using UPT.Features.Features.AutorizationFeatures.Requests;
using UPT.Tests.API.Frontend.AuthorizeTests.Base;

namespace UPT.Tests.API.Frontend.AuthorizeTests;

internal class AuthorizeTests : ApiBaseTests<IAuthorizeProvider>
{
    [Test]
    public async Task LogIn_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<LoginRequest>()
            .With(x => x.EmailAddress, SeedTestsConsts.DefaultEmail)
            .With(x => x.Password, SeedTestsConsts.DefaultPassword)
            .Create();

        // act
        var response = await Provider.Login(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.AccessToken.Should().NotBeNull();
    }

    [Test]
    public async Task Register_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<RegisterCommand>()
            .With(x => x.EmailAddress, SeedTestsConsts.DefaultEmail)
            .With(x => x.Password, SeedTestsConsts.DefaultPassword)
            .Create();

        // act
        var response = await Provider.Register(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Test]
    public async Task RestorePassword_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<RestorePasswordCommand>()
            .With(x => x.EmailAddress, SeedTestsConsts.DefaultEmail)
            .Create();

        // act
        var response = await Provider.RestorePassword(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
    }    
    
    [Test]
    public async Task EditPassword_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<EditPasswordCommand>()
            .With(x => x.EmailAddress, SeedTestsConsts.DefaultEmail)
            .With(x => x.OldPassword, SeedTestsConsts.DefaultPassword)
            .With(x => x.NewPassword, SeedTestsConsts.DefaultPassword)
            .Create();

        // act
        var response = await Provider.EditPassword(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();

    }    
    
    [Test]
    public async Task RefreshAccessToken_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<RefreshAccessTokenRequest>()
            
            .Create();

        // act
        var response = await Provider.RefreshAccessToken(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
    }
}
