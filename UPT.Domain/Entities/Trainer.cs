using System.ComponentModel.DataAnnotations;
using UPT.Domain.Base;
using UPT.Infrastructure.Enums;

namespace UPT.Domain.Entities;

public class Trainer : HasIdBase
{
    public User User { get; protected set; } = default!;

    /// <summary>
    /// Описаие тренера на странице профиля
    /// </summary>
    [MaxLength(255)]
    public string? Description { get; protected set; } = default!;

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
    /// Программы тренировок по которым тренер тренирует
    /// </summary>
    public List<TrainingProgram> TrainingPrograms { get; protected set; } = default!;

    public List<Client> Clients { get; protected set; } = default!;

    public Gym Gym { get; protected set; } = default!;

    public int? DialogCount { get; protected set; } = default!;

    public PurchasedProduct? PurchasedProduct { get; set; }

    public bool IsDeleted { get; protected set; } = false;

    public Trainer(User user, int experience, bool medicGrade,
        bool workInjuries, bool workSportsmens, List<TrainingProgram> trainingPrograms, Gym gym, string? description)
    {
        User = user;
        Experience = experience;
        MedicGrade = medicGrade;
        WorkInjuries = workInjuries;
        WorkSportsmens = workSportsmens;
        TrainingPrograms = trainingPrograms;
        Gym = gym;
        Description = description;
    }

    public void SetClients(List<Client> clients)
    {
        Clients = clients;
    }

    public void SetDialogCount(int count)
    {
        DialogCount = count;
    }

    public void SetPurchasedProduct(PurchasedProduct purchasedProduct)
    {
        PurchasedProduct = purchasedProduct;
    }

    public void DialogCountDecrement()
    {
        DialogCount--;
    }

    public void Delete()
    {
        IsDeleted = true;
    }

    public void Update(int experience, bool medicGrade, bool workInjuries, bool workSportsmens, List<TrainingProgram> trainingPrograms, Gym gym, string? description)
    {
        Experience = experience;
        MedicGrade = medicGrade;
        WorkInjuries = workInjuries;
        WorkSportsmens = workSportsmens;
        TrainingPrograms = trainingPrograms;
        Gym = gym;
        Description = description;
    }

    // for EF
    protected Trainer()
    {

    }
}
