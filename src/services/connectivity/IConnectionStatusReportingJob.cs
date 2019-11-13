using System.Threading.Tasks;

namespace ACC.Services.VehicleConnectivity
{
    public interface IConnectionStatusReportingJob
    {
        Task ExecuteAsync();
    }
}