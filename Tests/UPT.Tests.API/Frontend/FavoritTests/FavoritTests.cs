using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using UPT.Features.Features.FavoritFeatures.Requests;
using UPT.Tests.API.Frontend.FavoritTests.Base;

namespace UPT.Tests.API.Frontend.FavoritTests;

internal class FavoritTests : ApiBaseTests<IFavoritProvider>
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
        response.Content!.ClientId.Should().NotBe(0);
    }

    [Test]
    public async Task Add_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<AddFavoritCommand>()
           .With(x => x.ClientId, 2)
            .With(x => x.TrainerId, 1)
            .Create();

        // act
        var response = await Provider.Add(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.ClientId.Should().NotBe(0);
    }

    [Test]
    public async Task Delete_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<DeleteFavoritCommand>()
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
