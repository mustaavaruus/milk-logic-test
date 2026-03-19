using WebApplication1.Services.Sensors.Models;
using WebApplication1.Services.Sensors.Models.xml_models;

namespace WebApplication1.Services.Sensors
{
    /// <summary>
    /// Интерфейс сервиса обработки данных с сенсора
    /// </summary>
    public interface ISensorDataService
    {
        /// <summary>
        /// Принимает данные от эмулятора датчиков и сохраняет их в базе.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UploadDataResponseServiceModel> UploadDataAsync(SensorDataCreateServiceModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает данные за заданный период времени.
        /// </summary>
        /// <param name="period"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<SensorDataServiceModel>> GetDataByPeriodAsync(PeriodServiceModel period, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает агрегированные данные (среднее, максимум, минимум) за указанный период.
        /// </summary>
        /// <param name="period"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<SummaryServiceModel>> GetSummaryAsync(PeriodServiceModel period, CancellationToken cancellationToken);

        /// <summary>
        /// Валидирует данные с фронта в .XML
        /// </summary>
        /// <param name="items"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UploadDataResponseServiceModel> ValidateDataAsync(List<SensorDataItemXmlServiceModel> items, CancellationToken cancellationToken);
    }
}
