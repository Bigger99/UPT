using Microsoft.EntityFrameworkCore;
using UPT.Domain.Entities;
using UPT.Infrastructure.Enums;

namespace UPT.Data.SeedData;

internal class UPTDbDataSeed
{
    private readonly UPTDbContext _dbContext;

    public const string Client1 = "Client1";
    public const string Client2 = "Client2";
    public const string Client3 = "Client3";

    public const string Trainer1 = "Trainer1";
    public const string Trainer2 = "Trainer2";
    public const string Trainer3 = "Trainer3";

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

        var client1 = new User($"{Client1}@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K");
        client1.AddUserData(Client1, "+79999999999", $"{Client1}@mail.ru", city, Gender.Male);

        var client2 = new User($"{Client2}@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K");
        client2.AddUserData(Client2, "+79999999999", $"{Client2}@mail.ru", city, Gender.Male);

        var client3 = new User($"{Client3}@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K");
        client3.AddUserData(Client3, "+79999999999", $"{Client3}@mail.ru", city, Gender.Male);

        var trainer1 = new User($"{Trainer1}@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K");
        trainer1.AddUserData(Trainer1, "+79999999999", $"{Trainer1}@mail.ru", city, Gender.Male);

        var trainer2 = new User($"{Trainer2}@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K");
        trainer2.AddUserData(Trainer2, "+79999999999", $"{Trainer2}@mail.ru", city, Gender.Male);

        var trainer3 = new User($"{Trainer3}@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K");
        trainer3.AddUserData(Trainer3, "+79999999999", $"{Trainer3}@mail.ru", city, Gender.Male);

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
        var trainer1 = _dbContext.Users.First(x => x.Name == Trainer1);
        var trainer2 = _dbContext.Users.First(x => x.Name == Trainer2);
        var trainer3 = _dbContext.Users.First(x => x.Name == Trainer3);
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
        var user1 = _dbContext.Users.First(x => x.Name == Client1);
        var user2 = _dbContext.Users.First(x => x.Name == Client2);
        var user3 = _dbContext.Users.First(x => x.Name == Client3);
        var trainer1 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == Trainer1);
        var trainer2 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == Trainer2);
        var trainer3 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == Trainer3);

        var client1 = new Client(user1, TrainingProgram.CorrectionAndWeightLoss, height: 170, weight: 70,
                volumeBreast: 10.0, volumeWaist: 20.0, volumeAbdomen: 30.0,
                volumeButtock: 40.0, volumeHip: 50.0);
        client1.SetTrainer(trainer1);

        var client2 = new Client(user2, TrainingProgram.MuscleGain, height: 170, weight: 70,
                volumeBreast: 10.0, volumeWaist: 20.0, volumeAbdomen: 30.0,
                volumeButtock: 40.0, volumeHip: 50.0);
        client2.SetTrainer(trainer2);

        var client3 = new Client(user3, TrainingProgram.CompetitionsPreparation, height: 170, weight: 70,
            volumeBreast: 10.0, volumeWaist: 20.0, volumeAbdomen: 30.0,
            volumeButtock: 40.0, volumeHip: 50.0);
        client3.SetTrainer(trainer3);


        _dbContext.Clients.AddRange([client1, client2, client3]);
        _dbContext.SaveChanges();
    }

    private void SetFavoriteSeed()
    {
        var client1 = _dbContext.Clients.Include(x => x.User).First(x => x.User.Name == Client1);
        var client2 = _dbContext.Clients.Include(x => x.User).First(x => x.User.Name == Client2);
        var client3 = _dbContext.Clients.Include(x => x.User).First(x => x.User.Name == Client3);
        var trainer1 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == Trainer1);
        var trainer2 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == Trainer2);
        var trainer3 = _dbContext.Trainers.Include(x => x.User).First(x => x.User.Name == Trainer3);

        var favorites = new List<Favorite>
        {
            new (client1, [trainer1]),
            new (client2, [trainer2]),
            new (client3, [trainer1, trainer3]),
        };

        _dbContext.Favorits.AddRange(favorites);
        _dbContext.SaveChanges();
    }
}