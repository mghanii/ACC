using ACC.Common.Extensions;
using ACC.Common.Repository;
using ACC.Services.Tracking.Domain;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.Repositories
{
    public class TrackingHistoryRepository : ITrackingHistoryRepository
    {
        private readonly IRepository<TrackingHistory, string> _repository;

        public TrackingHistoryRepository(IRepository<TrackingHistory, string> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(TrackingHistory history)
        {
            await _repository.AddAsync(history)
                      .AnyContext();
        }

        public async Task<TrackingHistory> GetAsync(string id)
        {
            return await _repository.GetAsync(id)
                .AnyContext();
        }
    }
}