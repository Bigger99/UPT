using Microsoft.EntityFrameworkCore;
using UPT.Domain.Entities;
using UPT.Infrastructure.Enums;

namespace UPT.Data.SeedData;

internal class UPTDbDataSeed
{
    private readonly UPTDbContext _dbContext;

    public const string ClientName1 = nameof(ClientName1);
    public const string ClientName2 = nameof(ClientName2);
    public const string ClientName3 = nameof(ClientName3);

    public const string TrainerName1 = nameof(TrainerName1);
    public const string TrainerName2 = nameof(TrainerName2);
    public const string TrainerName3 = nameof(TrainerName3);

    private UPTDbDataSeed(UPTDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public static void SeedData(UPTDbContext dbContext)
    {
        var seeder = new UPTDbDataSeed(dbContext);
        seeder.SetData();
    }

    private void SetData()
    {
        if (_dbContext.Users.Any())
            return;

        _dbContext.Database.BeginTransaction();
        try
        {
            SetCitiesSeed();
            SetUserSeed();
            SetGymsSeed();
            SetTrainersSeed();
            SetClientsSeed();
            SetFavoriteSeed();
            SetFeedbackSeed();
            SetPaymentsSeed();
            SetNotificationSeed();
            SetNewsSeed();
            SetGoalsSeed();

            _dbContext.SaveChanges();
            _dbContext.Database.CommitTransaction();
        }
        catch (Exception e)
        {
            _dbContext.Database.RollbackTransaction();
            Console.WriteLine(e);
            throw;
        }
    }

    private void SetCitiesSeed()
    {
        var moscow = new City("Москва");
        var saintPetersburg = new City("Санкт-Петербург");

        _dbContext.Cities.AddRange([moscow, saintPetersburg]);
        _dbContext.SaveChanges();
    }

    private void SetUserSeed()
    {
        var city = _dbContext.Cities.First();

        var client1 = new User($"{ClientName1}@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K");
        client1.EditUserData(ClientName1, "+79999999999", $"{ClientName1}@mail.ru", city, Gender.Male, true, true, null);

        var client2 = new User($"{ClientName2}@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K");
        client2.EditUserData(ClientName2, "+79999999999", $"{ClientName2}@mail.ru", city, Gender.Male, true, true, null);

        var client3 = new User($"{ClientName3}@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K");
        client3.EditUserData(ClientName3, "+79999999999", $"{ClientName3}@mail.ru", city, Gender.Male, true, true, null);

        var trainer1 = new User($"{TrainerName1}@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K");
        trainer1.EditUserData(TrainerName1, "+79999999999", $"{TrainerName1}@mail.ru", city, Gender.Male, true, true, null);

        var trainer2 = new User($"{TrainerName2}@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K");
        trainer2.EditUserData(TrainerName2, "+79999999999", $"{TrainerName2}@mail.ru", city, Gender.Male, true, true, null);

        var trainer3 = new User($"{TrainerName3}@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K");
        trainer3.EditUserData(TrainerName3, "+79999999999", $"{TrainerName3}@mail.ru", city, Gender.Male, true, true, null);

        _dbContext.Users.AddRange([client1, client2, client3, trainer1, trainer2, trainer3]);
        _dbContext.SaveChanges();
    }

    private void SetGymsSeed()
    {
        var city = _dbContext.Cities.First();

        var gyms = new List<Gym>
        {
            new("Gym1", new TimeOnly(6, 0), new TimeOnly(22, 0), city, "55.7558, 37.6173"),
            new("Gym2", new TimeOnly(7, 0), new TimeOnly(23, 0), city, "55.7601, 37.6189"),
            new("Gym3", new TimeOnly(6, 30), new TimeOnly(21, 30), city, "55.7625, 37.6156"),
            new("Gym4", new TimeOnly(5, 0), new TimeOnly(23, 0), city, "55.7541, 37.6202"),
            new("Gym5", new TimeOnly(6, 0), new TimeOnly(22, 30), city, "55.7563, 37.6135"),
            new("Gym6", new TimeOnly(7, 0), new TimeOnly(22, 0), city, "55.7588, 37.6214"),
            new("Gym7", new TimeOnly(8, 0), new TimeOnly(20, 0), city, "55.7592, 37.6148"),
            new("Gym8", new TimeOnly(6, 30), new TimeOnly(21, 30), city, "55.7536, 37.6151"),
            new("Gym9", new TimeOnly(5, 30), new TimeOnly(22, 0), city, "55.7600, 37.6170"),
            new("Gym10", new TimeOnly(6, 0), new TimeOnly(23, 0), city, "55.7522, 37.6165")
        };

        _dbContext.Gyms.AddRange(gyms);
        _dbContext.SaveChanges();
    }

    private void SetTrainersSeed()
    {
        var trainer1 = _dbContext.Users.First(x => x.Name == TrainerName1);
        var trainer2 = _dbContext.Users.First(x => x.Name == TrainerName2);
        var trainer3 = _dbContext.Users.First(x => x.Name == TrainerName3);
        var gyms = _dbContext.Gyms.First();

        var trainers = new List<Trainer>
        {
            new(trainer1, 3, true, true, true,
            [
                TrainingProgram.CorrectionAndWeightLoss, 
                TrainingProgram.MuscleGain, 
                TrainingProgram.CompetitionsPreparation, 
                TrainingProgram.RestorationMusculoskeletalSystem
            ], gyms, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."),
            new(trainer2, 5, true, true, true,
            [
                TrainingProgram.CorrectionAndWeightLoss,
                TrainingProgram.MuscleGain,
            ], gyms, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."),
            new(trainer3, 6, true, true, true,
            [
                TrainingProgram.CompetitionsPreparation,
                TrainingProgram.RestorationMusculoskeletalSystem
            ], gyms, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.")
        };

        _dbContext.Trainers.AddRange(trainers);
        _dbContext.SaveChanges();
    }

    private void SetClientsSeed()
    {
        var user1 = _dbContext.Users.First(x => x.Name == ClientName1);
        var user2 = _dbContext.Users.First(x => x.Name == ClientName2);
        var user3 = _dbContext.Users.First(x => x.Name == ClientName3);
        var trainer1 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName1);
        var trainer2 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName2);
        var trainer3 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName3);

        var client1 = new Client(user1, height: 170, weight: 70,
                volumeBreast: 10.0, volumeWaist: 20.0, volumeAbdomen: 30.0,
                volumeButtock: 40.0, volumeHip: 50.0);
        client1.SetTrainer(trainer1);

        var client2 = new Client(user2, height: 170, weight: 70,
                volumeBreast: 10.0, volumeWaist: 20.0, volumeAbdomen: 30.0,
                volumeButtock: 40.0, volumeHip: 50.0);
        client2.SetTrainer(trainer2);

        var client3 = new Client(user3, height: 170, weight: 70,
            volumeBreast: 10.0, volumeWaist: 20.0, volumeAbdomen: 30.0,
            volumeButtock: 40.0, volumeHip: 50.0);
        client3.SetTrainer(trainer3);


        _dbContext.Clients.AddRange([client1, client2, client3]);
        _dbContext.SaveChanges();
    }

    private void SetFavoriteSeed()
    {
        var client1 = _dbContext.Clients.Include(x => x.User).First(x => x.User.Name == ClientName1);
        var client2 = _dbContext.Clients.Include(x => x.User).First(x => x.User.Name == ClientName2);
        var client3 = _dbContext.Clients.Include(x => x.User).First(x => x.User.Name == ClientName3);
        var trainer1 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName1);
        var trainer2 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName2);
        var trainer3 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName3);

        var favorites = new List<Favorite>
        {
            new (client1, [trainer1]),
            new (client2, [trainer2]),
            new (client3, [trainer1, trainer3]),
        };

        _dbContext.Favorits.AddRange(favorites);
        _dbContext.SaveChanges();
    }

    private void SetFeedbackSeed()
    {
        var client1 = _dbContext.Clients.Include(x => x.User).First(x => x.User.Name == ClientName1);
        var client2 = _dbContext.Clients.Include(x => x.User).First(x => x.User.Name == ClientName2);
        var client3 = _dbContext.Clients.Include(x => x.User).First(x => x.User.Name == ClientName3);
        var trainer1 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName1);
        var trainer2 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName2);
        var trainer3 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName3);

        var feedbacks = new List<Feedback>
        {
            new (DateTime.UtcNow, 5.0, "Cool", client1, trainer1),
            new (DateTime.UtcNow, 4.0, "Good", client2, trainer1),
            new (DateTime.UtcNow, 4.0, "Good", client2, trainer2),
            new (DateTime.UtcNow, 3.0, "So-so", client3, trainer3),
        };

        _dbContext.Feedbacks.AddRange(feedbacks);
        _dbContext.SaveChanges();
    }

    private void SetPaymentsSeed()
    {
        var trainer1 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName1);
        var trainer2 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName2);
        var trainer3 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName3);

        var payments = new List<Payment>
        {
            new (trainer1.User, DateTime.UtcNow, "Month Pro", 1000.0m),
            new (trainer2.User, DateTime.UtcNow, "Month Deluxe", 1500.0m),
            new (trainer3.User, DateTime.UtcNow, "Trial", 0m),
        };

        _dbContext.Payments.AddRange(payments);
        _dbContext.SaveChanges();
    }

    private void SetNotificationSeed()
    {
        var trainer1 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName1);
        var trainer2 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName2);
        var trainer3 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName3);

        var notifications = new List<Notification>
        {
            new ("NotificationTitle1", DateTime.UtcNow, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", trainer1.User),
            new ("NotificationTitle2", DateTime.UtcNow, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", trainer2.User),
            new ("NotificationTitle3", DateTime.UtcNow, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", trainer3.User),
        };

        _dbContext.Notifications.AddRange(notifications);
        _dbContext.SaveChanges();
    }

    private void SetNewsSeed()
    {
        var trainer1 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName1);
        var trainer2 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName2);
        var trainer3 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName3);

        var news = new List<News>
        {
            new ("NewsTitle1", DateTime.UtcNow, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", trainer1.User, null),
            new ("NewsTitle2", DateTime.UtcNow, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", trainer2.User, null),
            new ("NewsTitle3", DateTime.UtcNow, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", trainer3.User, null),
        };

        _dbContext.News.AddRange(news);
        _dbContext.SaveChanges();
    }

    private void SetGoalsSeed()
    {
        var client1 = _dbContext.Clients.Include(x => x.User).First(x => x.User.Name == ClientName1);
        var client2 = _dbContext.Clients.Include(x => x.User).First(x => x.User.Name == ClientName2);
        var client3 = _dbContext.Clients.Include(x => x.User).First(x => x.User.Name == ClientName3);
        var trainer1 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName1);
        var trainer2 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName2);
        var trainer3 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == TrainerName3);

        var goals = new List<Goal>
        {
            new (client1, TrainingProgram.CorrectionAndWeightLoss, 70.0, 65.0,
                Deadline.Mounth3, [DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday], TimeOfDay.Evening, false),
            new (client2, TrainingProgram.MuscleGain, 70.0, 75.0,
                Deadline.Mounth6, [DayOfWeek.Tuesday, DayOfWeek.Thursday], TimeOfDay.Evening, false),
            new (client3, TrainingProgram.CompetitionsPreparation, 70.0, 70.0,
                Deadline.Mounth12, [DayOfWeek.Saturday, DayOfWeek.Sunday], TimeOfDay.Day, false),
        };

        _dbContext.Goals.AddRange(goals);
        _dbContext.SaveChanges();
    }
}