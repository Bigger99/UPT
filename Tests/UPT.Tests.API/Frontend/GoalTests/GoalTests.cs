using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using UPT.Features.Features.GoalFeatures.Requests;
using UPT.Features.Features.NewsFeatures.Requests;
using UPT.Tests.API.Frontend.GoalTests.Base;

namespace UPT.Tests.API.Frontend.GoalTests;

internal class GoalTests : ApiBaseTests<IGoalProvider>
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
        response.Content!.Id.Should().NotBe(0);
    }

    [Test]
    public async Task GetAllClientGoals_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // act
        var response = await Provider.GetAllClientGoals(1);

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
        var requestModel = Fixture.Build<CreateGoalCommand>()
            .With(x => x.ClientId, 1)
            .Create();

        // act
        var response = await Provider.Create(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content!.Id.Should().NotBe(0);
    }

    [Test]
    public async Task Update_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<UpdateGoalCommand>()
            .With(x => x.GoalId, 1)
            .Create();

        // act
        var response = await Provider.Update(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content!.Id.Should().NotBe(0);
    }

    [Test]
    public async Task SetTrainer_WhenValidCalled_ResponseMustBeNonEmpty()
    {
        // arrange
        var requestModel = Fixture.Build<SetGoalTrainerCommand>()
            .With(x => x.GoalId, 1)
            .With(x => x.TrainerId, 1)
            .Create();

        // act
        var response = await Provider.SetTrainer(requestModel);

        // assert
        using var _ = new AssertionScope();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
        response.Content!.Id.Should().NotBe(0);
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
