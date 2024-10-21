namespace UPT.Data.SeedData;

public static class UPTDbContextExtensions
{
    public static void SeedData(this UPTDbContext dbContext)
    {
        UPTDbDataSeed.SeedData(dbContext);
    }
}
