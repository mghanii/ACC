using System.Threading.Tasks;

namespace ACC.Persistence.Mongo
{
    public interface IMongoDbSeeder
    {
        Task SeedAsync();
    }
}