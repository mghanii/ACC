using System.Threading.Tasks;

namespace ACC.Common.Repository
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
    }
}