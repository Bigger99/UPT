using FluentAssertions;
using FluentAssertions.Execution;
using UPT.Tests.API.Frontend.UserTests.Base;

namespace UPT.Tests.API.Frontend.UserTests;

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
        // act
        var response = await Provider.Add(1, 1);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.Count.Should().NotBe(0);
    }

    [Test]
    public async Task Delete_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // act
        var response = await Provider.Delete(1, 1);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
