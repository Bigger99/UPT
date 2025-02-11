﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using UPT.Domain.Entities;
using UPT.Infrastructure.Enums;

namespace UPT.Data.SeedData;

internal class UPTDbDataSeed
{
    private readonly UPTDbContext _dbContext;
    private const string TrainerStartMail = "trainer";

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
        var kazan = new City("Казань");
        var novosibirsk = new City("Новосибирск");
        var yekaterinburg = new City("Екатеринбург");

        _dbContext.Cities.AddRange([moscow, saintPetersburg, kazan, novosibirsk, yekaterinburg]);
        _dbContext.SaveChanges();
    }

    private void SetUserSeed()
    {
        var cities = _dbContext.Cities.Take(5).ToList();

        var clients = new List<User>();

        var client1 = new User("ivanov@mail.ru", SeedTestsConsts.DefaultPasswordHash); // Password: "password123"
        client1.EditUserData("Иванов Иван", "+79999999999", SeedTestsConsts.DefaultEmail, cities[0], Gender.Male, true, true, null);
        clients.Add(client1);

        var client2 = new User("petrova@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K");// Password: "password123"
        client2.EditUserData("Петрова Анна", "+79999999999", "petrova@mail.ru", cities[1], Gender.Female, true, true, null);
        clients.Add(client2);

        var client3 = new User("sidorov@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K"); // Password: "password123"
        client3.EditUserData("Сидоров Николай", "+79999999999", "sidorov@mail.ru", cities[2], Gender.Male, true, true, null);
        clients.Add(client3);

        var client4 = new User("smirnova@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K"); // Password: "password123"
        client4.EditUserData("Смирнова Ольга", "+79999999999", "smirnova@mail.ru", cities[3], Gender.Female, true, true, null);
        clients.Add(client4);

        var client5 = new User("vasiliev@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K"); // Password: "password123"
        client5.EditUserData("Васильев Алексей", "+79999999999", "vasiliev@mail.ru", cities[4], Gender.Male, true, true, null);
        clients.Add(client5);

        for (int i = 0; i < 20; i++)
        {
            var cityIndex = i % cities.Count; // Rotate through cities
            var gender = i % 2 == 0 ? Gender.Male : Gender.Female;
            var firstName = gender == Gender.Male ? $"Иван{i}" : $"Анна{i}";
            var lastName = gender == Gender.Male ? $"Смирнов" : $"Кузнецова";
            var patronymic = gender == Gender.Male ? $"Алексеевич" : $"Ивановна";
            var client = new User($"{TrainerStartMail}{i}@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K"); // Password: "password123"

            client.EditUserData($"{lastName} {firstName} {patronymic}", $"+79999999{i:D3}", $"client{i}@mail.ru", cities[cityIndex], gender, true, true, null);
            clients.Add(client);
        }

        var trainers = new List<User>();

        var trainer1 = new User($"{TrainerStartMail}1@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K"); // Password: "password123"
        trainer1.EditUserData("Кузнецов Андрей", "+79999999999", "trainer1@mail.ru", cities[0], Gender.Male, true, true, null);
        trainers.Add(trainer1);

        var trainer2 = new User($"{TrainerStartMail}2@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K"); // Password: "password123"
        trainer2.EditUserData("Лебедева Наталья", "+79999999999", "trainer2@mail.ru", cities[1], Gender.Female, true, true, null);
        trainers.Add(trainer2);

        var trainer3 = new User($"{TrainerStartMail}3@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K"); // Password: "password123"
        trainer3.EditUserData("Попов Дмитрий", "+79999999999", "trainer3@mail.ru", cities[2], Gender.Male, true, true, null);
        trainers.Add(trainer3);

        var trainer4 = new User($"{TrainerStartMail}4@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K"); // Password: "password123"
        trainer4.EditUserData("Новикова Елена", "+79999999999", "trainer4@mail.ru", cities[3], Gender.Female, true, true, null);
        trainers.Add(trainer4);

        var trainer5 = new User($"{TrainerStartMail}5@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K"); // Password: "password123"
        trainer5.EditUserData("Зайцев Михаил", "+79999999999", "trainer5@mail.ru", cities[4], Gender.Male, true, true, null);
        trainers.Add(trainer5);

        for (int i = 0; i < 20; i++)
        {
            var cityIndex = i % cities.Count; // Rotate through cities
            var gender = i % 2 == 0 ? Gender.Male : Gender.Female;
            var firstName = gender == Gender.Male ? $"Иван{i}" : $"Анна{i}";
            var lastName = gender == Gender.Male ? $"Смирнов" : $"Кузнецова";
            var patronymic = gender == Gender.Male ? $"Алексеевич" : $"Ивановна";
            var trainer = new User($"{TrainerStartMail}{i}@mail.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K"); // Password: "password123"

            trainer.EditUserData($"{lastName} {firstName} {patronymic}", $"+79999999{i:D3}", $"trainer{i}@mail.ru", cities[cityIndex], gender, true, true, null);
            trainers.Add(trainer);
        }

        _dbContext.Users.AddRange(clients);
        _dbContext.Users.AddRange(trainers);
        _dbContext.SaveChanges();
    }


    private void SetGymsSeed()
    {
        var cities = _dbContext.Cities.Take(5).ToList();

        var gymLocations = new Dictionary<string, string[]>
        {
            { "Москва", new[] { "55.7558, 37.6173", "55.7601, 37.6189", "55.7625, 37.6156", "55.7541, 37.6202", "55.7563, 37.6135" } },
            { "Санкт-Петербург", new[] { "59.9343, 30.3351", "59.9377, 30.3156", "59.9405, 30.3146", "59.9431, 30.3204", "59.9505, 30.3165" } },
            { "Казань", new[] { "55.7961, 49.1064", "55.7981, 49.1095", "55.8003, 49.1127", "55.8025, 49.1158", "55.8047, 49.1190" } },
            { "Новосибирск", new[] { "55.0084, 82.9357", "55.0105, 82.9378", "55.0126, 82.9400", "55.0147, 82.9421", "55.0168, 82.9443" } },
            { "Екатеринбург", new[] { "56.8389, 60.6057", "56.8411, 60.6088", "56.8433, 60.6110", "56.8455, 60.6132", "56.8477, 60.6154" } }
        };

        const string GoldGym = nameof(GoldGym);
        const string DDX = nameof(DDX);
        const string Spirit = nameof(Spirit);
        const string FijiSportClub = nameof(FijiSportClub);
        const string WorldClass = nameof(WorldClass);

        var gymNames = new string[] { GoldGym, DDX, Spirit, FijiSportClub, WorldClass };

        foreach (var city in cities)
        {
            var locations = gymLocations[city.Name!];
            var i = 0;
            var gyms = new List<Gym>();

            foreach (var location in locations)
            {
                if (i > gymNames.Length - 1)
                {
                    i = 0;
                }

                var gym = new Gym(gymNames[i], new TimeOnly(6, 0), new TimeOnly(22, 0), city, location);
                gyms.Add(gym);
            }

            _dbContext.Gyms.AddRange(gyms);
        }

        _dbContext.SaveChanges();
    }

    private void SetTrainersSeed()
    {
        var userTrainers = _dbContext.Users.Where(x => x.EmailAddress!.StartsWith(TrainerStartMail)).ToList();
        var gyms = _dbContext.Gyms.Include(x => x.City).ToList();
        var trainers = new List<Trainer>();

        for (int i = 0; i < userTrainers.Count; i++)
        {
            var gym = gyms[i];
            var trainer = new Trainer(userTrainers[i], new Random().Next(3, 10), true, true, true,
                GetRandomUniqueEnumValues<TrainingProgram>(3).ToList(),
                gym, "Высококвалифицированный тренер.");

            trainers.Add(trainer);
        }

        _dbContext.Trainers.AddRange(trainers);
        _dbContext.SaveChanges();
    }

    private void SetClientsSeed()
    {
        var userClients = _dbContext.Users.Where(x => !x.EmailAddress!.StartsWith(TrainerStartMail)).ToList();
        var trainers = _dbContext.Trainers.ToList();

        var clients = new List<Client>();

        for (int i = 0; i < userClients.Count; i++)
        {
            var trainer = trainers[i];
            var client = new Client(userClients[i], height: 170 + new Random().Next(0, 10), weight: 70 + new Random().Next(0, 10),
                volumeBreast: 90.0 + new Random().Next(0, 5), volumeWaist: 60.0 + new Random().Next(0, 5),
                volumeAbdomen: 70.0 + new Random().Next(0, 5), volumeButtock: 100.0 + new Random().Next(0, 5), volumeHip: 90.0 + new Random().Next(0, 5));
            client.SetTrainer(trainer);

            clients.Add(client);
        }

        _dbContext.Clients.AddRange(clients);
        _dbContext.SaveChanges();
    }

    private void SetFavoriteSeed()
    {
        var clients = _dbContext.Clients.Include(c => c.User).ToList();
        var trainers = _dbContext.Trainers.Include(t => t.User).ToList();

        var favorites = clients.Select(client => new Favorite(client, trainers.Take(2).ToList())).ToList();

        _dbContext.Favorits.AddRange(favorites);
        _dbContext.SaveChanges();
    }

    private void SetFeedbackSeed()
    {
        var clients = _dbContext.Clients.Include(c => c.User).ToList();
        var trainers = _dbContext.Trainers.Include(t => t.User).ToList();

        var feedbacks = clients.Select((client, index) => new Feedback(
            DateTime.UtcNow.AddDays(-index), 4.0 + (index % 2 == 0 ? -0.5 : 0.5), "Отличный тренер!", client, trainers[index % trainers.Count])).ToList();

        _dbContext.Feedbacks.AddRange(feedbacks);
        _dbContext.SaveChanges();
    }

    private void SetPaymentsSeed()
    {
        var trainers = _dbContext.Trainers.Include(t => t.User).ToList();

        var payments = trainers.Select((trainer, index) => new Payment(
            trainer.User, DateTime.UtcNow.AddMonths(-index), "Pro", 1000.0m + index * 500)).ToList();

        _dbContext.Payments.AddRange(payments);
        _dbContext.SaveChanges();
    }

    private void SetNotificationSeed()
    {
        var trainers = _dbContext.Trainers.Include(t => t.User).ToList();

        var notifications = trainers.Select((trainer, index) => new Notification(
            $"Ваша подписка офрмлена", DateTime.UtcNow.AddDays(-index), "Подписка активна.", trainer.User)).ToList();

        _dbContext.Notifications.AddRange(notifications);
        _dbContext.SaveChanges();
    }

    private void SetNewsSeed()
    {
        var trainers = _dbContext.Trainers.Include(t => t.User).ToList();

        var news = trainers.Select((trainer, index) => new News(
            $"Забег в парке {index}", DateTime.UtcNow.AddDays(-index), "Забег в парке, ждём всех желающих", trainer.User, null)).ToList();

        _dbContext.News.AddRange(news);
        _dbContext.SaveChanges();
    }

    private void SetGoalsSeed()
    {
        var clients = _dbContext.Clients.Include(c => c.User).ToList();
        var trainers = _dbContext.Trainers.Include(c => c.User).ToList();
        var trainingProgramCount = Enum.GetValues<TrainingProgram>().Length;

        var goals = clients.Select((client, index) =>
        {
            var goal = new Goal(
            client, GetRandomEnumValue<TrainingProgram>(), 70 + index, 75 + index,
            GetRandomEnumValue<Deadline>(), GetRandomUniqueEnumValues<DayOfWeek>(3).ToList(), TimeOfDay.Evening, false);

            if (index % 2 == 0)
            {
                goal.SetTrainer(trainers[index]);
            }

            return goal;
        }).ToList();

        _dbContext.Goals.AddRange(goals);
        _dbContext.SaveChanges();
    }

    #region Helpers

    public static T GetRandomEnumValue<T>() where T : Enum
    {
        var values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(new Random().Next(values.Length))!;
    }

    public static IEnumerable<T> GetRandomUniqueEnumValues<T>(int count) where T : Enum
    {
        var values = Enum.GetValues(typeof(T)).Cast<T>().OrderBy(_ => Guid.NewGuid()).Take(count);
        return values;
    }

    #endregion
}