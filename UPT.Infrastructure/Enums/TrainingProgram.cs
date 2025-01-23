using System.Text.Json.Serialization;

namespace UPT.Infrastructure.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TrainingProgram
{
    /// <summary>
    /// Коррекция и снижение веса
    /// </summary>
    CorrectionAndWeightLoss,

    /// <summary>
    /// Набор мышечной массы
    /// </summary>
    MuscleGain,

    /// <summary>
    /// Подготовка к соревнованиям
    /// </summary>
    CompetitionsPreparation,

    /// <summary>
    /// Восстановление опорно-двигательного аппарата
    /// </summary>
    RestorationMusculoskeletalSystem,
}
