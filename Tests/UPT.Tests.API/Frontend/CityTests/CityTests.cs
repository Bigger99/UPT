﻿using FluentAssertions;
using FluentAssertions.Execution;
using UPT.Tests.API.Frontend.CityTests.Base;

namespace UPT.Tests.API.Frontend.CityTests;

internal class CityTests : ApiBaseTests<ICityProvider>
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
        response.Content!.Name.Should().NotBeNullOrEmpty();
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
}
