using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using UPT.Features.Features.PaymentFeatures.Requests;
using UPT.Tests.API.Frontend.PaymentTets.Base;

namespace UPT.Tests.API.Frontend.PaymentTets;

internal class PaymentTests : ApiBaseTests<IPaymentProvider>
{
    [Test]
    public async Task Get_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // act
        var response = await Provider.Get(26);

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
        var requestModel = Fixture.Build<AddPaymentCommand>()
            .With(x => x.UserId, 26)
            .Create();

        // act
        var response = await Provider.Add(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.Title.Should().NotBeNullOrEmpty();
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
