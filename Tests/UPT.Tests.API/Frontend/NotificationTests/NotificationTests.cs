using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using UPT.Features.Features.NotificationFeatures.Requests;
using UPT.Tests.API.Frontend.UserTests.Base;

namespace UPT.Tests.API.Frontend.UserTests;

internal class NotificationTests : ApiBaseTests<INotificationProvider>
{
    [Test]
    public async Task GetCkecked_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // act
        var response = await Provider.GetCkecked(1);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.Count.Should().NotBe(0);
    }

    [Test]
    public async Task GetUnCkecked_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // act
        var response = await Provider.GetUnCkecked(1);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.Count.Should().NotBe(0);
    }

    [Test]
    public async Task Create_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<CreateNotificationCommand>()
            .With(x => x.UserId, 1)
            .Create();

        // act
        var response = await Provider.Create(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.Name.Should().Be(requestModel.Name);
    }

    [Test]
    public async Task SetChecked_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // act
        var response = await Provider.SetChecked(1);

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
