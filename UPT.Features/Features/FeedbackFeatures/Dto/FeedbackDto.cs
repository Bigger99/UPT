using Mapster;
using System.ComponentModel.DataAnnotations;
using UPT.Domain.Entities;
using UPT.Features.Features.ClientFeatures.Dto;
using UPT.Features.Features.TrainerFeatures.Dto;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.FeedbackFeatures.Dto;

public class FeedbackDto : IDto
{
    public int Id { get; protected set; } = default!;
    public string? Name { get; protected set; } = default!;
    public DateTime Date { get; protected set; } = default!;

    [Range(0.0, 5.0)]
    public double Rating { get; protected set; } = default!;

    [MaxLength(255)]
    public string Text { get; protected set; } = default!;

    public ClientDto Creator { get; protected set; } = default!;
    public TrainerDto Trainer { get; protected set; } = default!;

    static FeedbackDto()
    {
        TypeAdapterConfig<Feedback, FeedbackDto>
            .NewConfig()
            .Map(dest => dest.Creator, src => src.Creator.Adapt<ClientDto>())
            .Map(dest => dest.Trainer, src => src.Trainer.Adapt<TrainerDto>());
    }
}
