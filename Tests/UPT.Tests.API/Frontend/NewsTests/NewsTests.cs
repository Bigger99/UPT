using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using UPT.Features.Features.NewsFeatures.Requests;
using UPT.Tests.API.Frontend.UserTests.Base;

namespace UPT.Tests.API.Frontend.UserTests;

internal class NewsTests : ApiBaseTests<INewsProvider>
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
        response.Content!.Name.Should().NotBeNull();
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
    public async Task Add_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<UpdateNewsCommand>()
            .With(x => x.NewsId, 1)
            .With(x => x.UserId, 1)
            .Create();


        // act
        var response = await Provider.Update(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.Name.Should().Be(requestModel.Title);
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
