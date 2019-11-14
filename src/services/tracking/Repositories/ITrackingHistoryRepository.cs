using ACC.Services.Tracking.Domain;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.Repositories
{
    public interface ITrackingHistoryRepository
    {
        Task<TrackingHistory> GetAsync(string id);

        Task AddAsync(TrackingHistory history);
    }
}