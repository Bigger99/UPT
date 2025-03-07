﻿using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using UPT.Features.Features.TrainerFeatures.Requests;
using UPT.Infrastructure.Models;
using UPT.Tests.API.Frontend.TrainerTests.Base;

namespace UPT.Tests.API.Frontend.TrainerTests;

internal class TrainerTests : ApiBaseTests<ITrainerProvider>
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
        response.Content!.Description.Should().NotBeNullOrEmpty();
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
        var response = await Provider.GetByUserId(26);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.Description.Should().NotBeNullOrEmpty();
    }

    [Test]
    public async Task GetFiltered_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<PagedFilterQuery<FilteredTrainerRequest>>()
            .Create();

        // act
        var response = await Provider.GetFiltered(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.Count().Should().NotBe(0);
    }

    [Test]
    public async Task Create_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<CreateTrainerCommand>()
            .With(x => x.UserId, 1)
            .With(x => x.GymId, 1)
            .Create();

        // act
        var response = await Provider.Create(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content!.Should().NotBe(0);
    }

    [Test]
    public async Task Update_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<UpdateTrainerCommand>()
            .With(x => x.TrainerId, 1)
            .With(x => x.ClientsIds, [1])
            .With(x => x.GymId, 1)
            .Create();

        // act
        var response = await Provider.Update(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.Description.Should().NotBeNullOrEmpty();
    }

    [Test]
    public async Task SetTrainer_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<SetTrainerClientsCommand>()
            .With(x => x.TrainerId, 1)
            .With(x => x.ClientIds, [1])
            .Create();

        // act
        var response = await Provider.SetClients(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.Description.Should().NotBeNullOrEmpty();
    }

    [Test]
    public async Task DialogCountDecremen_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // act
        var response = await Provider.DialogCountDecremen(1);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.Description.Should().NotBeNullOrEmpty();
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
