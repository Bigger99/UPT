using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using UPT.Features.Features.FeedbackFeatures.Requests;
using UPT.Tests.API.Frontend.UserTests.Base;

namespace UPT.Tests.API.Frontend.UserTests;

internal class FeedbackTests : ApiBaseTests<IFeedbackProvider>
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
        response.Content!.Count.Should().NotBe(0);
    }

    [Test]
    public async Task Add_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<AddFeedbackCommand>()
            .With(x => x.ClientId, 1)
            .With(x => x.TrainerId, 1)
            .Create();

        // act
        var response = await Provider.Add(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.Name.Should().NotBeNullOrEmpty();
    }

    [Test]
    public async Task Delete_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<DeleteFeedbackCommand>()
            .With(x => x.ClientId, 1)
            .With(x => x.TrainerId, 1)
            .Create();

        // act
        var response = await Provider.Delete(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
