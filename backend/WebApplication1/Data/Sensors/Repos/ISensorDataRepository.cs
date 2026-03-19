using WebApplication1.Data.Sensors.Models;

namespace WebApplication1.Data.Sensors.Repos
{
    /// <summary>
    /// интерфейс репозитория сигналов с датчиков 
    /// </summary>
    public interface ISensorDataRepository : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<SensorData>> ReadAllAsync(CancellationToken cancellationToken);
        Task<SensorData?> ReadByIdAsync(int sensorDataId, CancellationToken cancellationToken);
        Task CreateAsync(SensorData model, CancellationToken cancellationToken);
        Task UpdateAsync(SensorData model, CancellationToken cancellationToken);
        Task DeleteAsync(int sensorDataId, CancellationToken cancellationToken);
        Task SaveAsync();

        Task<List<SensorData>> GetByPeriodAsync(
            DateTime? periodBegin, 
            DateTime? periodEnd, 
            CancellationToken cancellationToken);

        Task<List<SummaryRepositoryModel>> GetSummaryByPeriodAsync(
            DateTime? periodBegin,
            DateTime? periodEnd,
            CancellationToken cancellationToken);
    }
}
