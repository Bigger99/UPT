using UPT.Domain.Base;
using UPT.Infrastructure.Enums;

namespace UPT.Domain.Entities;

public class Trainer : HasIdBase
{
    public Person Person { get; protected set; } = default!;

    /// <summary>
    /// Стаж работы
    /// </summary>
    public int Experience { get; protected set; } = default!;

    /// <summary>
    /// Наличие медицинского образования
    /// </summary>
    public bool MedicGrade { get; protected set; } = default!;

    /// <summary>
    /// Работа с травмами
    /// </summary>
    public bool WorkInjuries { get; protected set; } = default!;

    /// <summary>
    /// Работас спортсменами
    /// </summary>
    public bool WorkSportsmens { get; protected set; } = default!;

    /// <summary>
    /// Восстановление опорно-двигательного аппарата
    /// </summary>
    public TrainingProgram TrainingProgram { get; protected set; } = default!;
    
    public Gym Gym { get; protected set; } = default!;

    public Trainer(Person person, int experience, bool medicGrade,
        bool workInjuries, bool workSportsmens, TrainingProgram trainingProgram, Gym gym)
    {
        Person = person;
        Experience = experience;
        MedicGrade = medicGrade;
        WorkInjuries = workInjuries;
        WorkSportsmens = workSportsmens;
        TrainingProgram = trainingProgram;
        Gym = gym;
    }

    // for EF
    protected Trainer()
    {

    }
}
