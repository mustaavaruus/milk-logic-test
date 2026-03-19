using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Sensors.Models;

namespace WebApplication1.Data.Sensors.Repos
{
    public class SensorDataRepository : ISensorDataRepository
    {
        private bool disposed = false;
        private readonly SensorDataDbContext _context;

        public SensorDataRepository(SensorDataDbContext context)
        {
            _context = context;
        }

        #region CRUD (на всякий случай, может пригодится =) )

        /// <summary>
        /// CREATE
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task CreateAsync(SensorData model, CancellationToken cancellationToken)
        {
            await _context.SensorData.AddAsync(model, cancellationToken);
            await SaveAsync();
        }

        /// <summary>
        /// READ all
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<SensorData>> ReadAllAsync(CancellationToken cancellationToken)
        {
            return _context.SensorData.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// READ by id
        /// </summary>
        /// <param name="sensorDataId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<SensorData?> ReadByIdAsync(int sensorDataId, CancellationToken cancellationToken)
        {
            return _context.SensorData.Where(sd => sd.Id == sensorDataId).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// UPDATE
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task UpdateAsync(SensorData model, CancellationToken cancellationToken)
        {
            _context.Entry(model).State = EntityState.Modified;
            await SaveAsync();
        }

        /// <summary>
        /// DELETE
        /// </summary>
        /// <param name="sensorDataId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task DeleteAsync(int sensorDataId, CancellationToken cancellationToken)
        {
            SensorData? sensorData = await _context.SensorData
                .Where(sd => sd.SensorId == sensorDataId)
                .FirstOrDefaultAsync(cancellationToken);

            if (sensorData == null)
            {
                throw new ArgumentException($"Объект с идентификатором {sensorDataId} не найден");
            }

            _context.SensorData.Remove(sensorData);
            await SaveAsync();
        }

        #endregion CRUD

        #region for BL

        /// <summary>
        /// Возвращает данные за заданный период времени 
        /// </summary>
        /// <param name="periodBegin"></param>
        /// <param name="periodEnd"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<SensorData>> GetByPeriodAsync(
            DateTime? periodBegin, 
            DateTime? periodEnd, 
            CancellationToken cancellationToken)
        {
            var query = _context.SensorData
                .Where(sd => sd.Timestamp >= periodBegin && sd.Timestamp <= periodEnd);

            return query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Возвращает агрегированные данные (среднее, максимум, минимум) за указанный период.
        /// </summary>
        /// <param name="periodBegin"></param>
        /// <param name="periodEnd"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<SummaryRepositoryModel>> GetSummaryByPeriodAsync(
            DateTime? periodBegin, 
            DateTime? periodEnd, 
            CancellationToken cancellationToken)
        {
            return _context.SensorData
                .Where(sd => sd.Timestamp >= periodBegin && sd.Timestamp <= periodEnd)
                .GroupBy(sd => sd.SensorId)
                .Select(group => new SummaryRepositoryModel
                {
                    SensorId = group.Key,
                    Maximum = group.Max(sd => sd.Value),
                    Minimum = group.Min(sd => sd.Value),
                    Average = group.Average(sd => sd.Value),

                })
                .OrderBy(sd => sd.SensorId)
                .ToListAsync(cancellationToken);
        }
        #endregion for BL

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }
    }
}
