using UPT.Domain.Entities;
using UPT.Infrastructure.Enums;

namespace UPT.Data.SeedData;

internal class UPTDbDataSeed
{
    private readonly UPTDbContext _dbContext;


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

        var user = new User("test@mai.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K");
        user.AddUserData("TestName", "+79999999999", "test@mai.ru", city, Role.Client, Gender.Male);

        _dbContext.Users.Add(user);
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

        var user = new User("test@mai.ru", "$2a$11$tZ74fcglcCTydem/c788NuSfM0R7K0dU.rMGo8tRJoEy0NRj8iA9K");
        user.AddUserData("TestName", "+79999999999", "test@mai.ru", city, Role.Client, Gender.Male);

        _dbContext.Gyms.AddRange(gyms);
        _dbContext.SaveChanges();
    }
}