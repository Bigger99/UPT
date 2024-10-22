using UPT.Domain.Base;
using UPT.Infrastructure.Enums;

namespace UPT.Domain.Entities;

public class Trainer : HasIdBase
{
    public User User { get; protected set; } = default!;

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

    public List<TrainingProgram> TrainingPrograms { get; protected set; } = default!;

    public List<Client> Clients { get; protected set; } = default!;

    public Gym Gym { get; protected set; } = default!;

    public bool IsDeleted { get; protected set; } = false;

    public Trainer(User user, int experience, bool medicGrade,
        bool workInjuries, bool workSportsmens, List<TrainingProgram> trainingPrograms, Gym gym)
    {
        User = user;
        Experience = experience;
        MedicGrade = medicGrade;
        WorkInjuries = workInjuries;
        WorkSportsmens = workSportsmens;
        TrainingPrograms = trainingPrograms;
        Gym = gym;
    }

    public void Delete()
    {
        IsDeleted = true;
    }

    // for EF
    protected Trainer()
    {

    }

    public void Update(int experience, bool medicGrade, bool workInjuries, bool workSportsmens, List<TrainingProgram> trainingPrograms, List<Client> clients, Gym gym)
    {
        Experience = experience;
        MedicGrade = medicGrade;
        WorkInjuries = workInjuries;
        WorkSportsmens = workSportsmens;
        TrainingPrograms = trainingPrograms;
        Clients = clients;
        Gym = gym;
    }
}
