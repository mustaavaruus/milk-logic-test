using AutoMapper;
using System.Text;
using WebApplication1.Data.Sensors.Models;
using WebApplication1.Data.Sensors.Repos;
using WebApplication1.Services.Sensors.Enums;
using WebApplication1.Services.Sensors.Models;
using WebApplication1.Services.Sensors.Models.xml_models;

namespace WebApplication1.Services.Sensors
{
    /// <summary>
    /// Класс с бизнес-логикой для sensor-data
    /// </summary>
    public class SensorDataService: ISensorDataService
    {
        private const string SuccessUploadMessage = "Данные успешно переданы!";
        private const string SuccessValidatedMessage = "Данные успешно провалидированы! Ошибок нет!";

        private readonly IMapper _mapper;
        private readonly ISensorDataRepository _sensorDataRepository;

        public SensorDataService(
                IMapper mapper,
                ISensorDataRepository sensorDataRepository
            ) 
        { 
            _mapper = mapper;
            _sensorDataRepository = sensorDataRepository;
        }


        /// <summary>
        /// Принимает данные от эмулятора датчиков и сохраняет их в базе.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<UploadDataResponseServiceModel> UploadDataAsync(SensorDataCreateServiceModel model, CancellationToken cancellationToken)
        {
            ValidateSensorData(model);

            var entity = _mapper.Map<SensorData>(model);
            entity.Timestamp = DateTime.UtcNow;

            await _sensorDataRepository.CreateAsync(entity, cancellationToken);

            return new UploadDataResponseServiceModel
            {
                Message = SuccessUploadMessage,
            };
        }


        /// <summary>
        /// Возвращает данные за заданный период времени.
        /// </summary>
        /// <param name="period"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<SensorDataServiceModel>> GetDataByPeriodAsync(PeriodServiceModel period, CancellationToken cancellationToken)
        {
            // Валидация
            ValidatePeriod(period);

            var result = _sensorDataRepository
                .GetByPeriodAsync(period.PeriodBegin, period.PeriodEnd, cancellationToken);

            return _mapper.Map<List<SensorDataServiceModel>>(await result);
        }

        /// <summary>
        /// Возвращает агрегированные данные (среднее, максимум, минимум) за указанный период.
        /// </summary>
        /// <param name="period"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<SummaryServiceModel>> GetSummaryAsync(
            PeriodServiceModel period, 
            CancellationToken cancellationToken)
        {
            // Валидация
            ValidatePeriod(period);

            var result = _sensorDataRepository
                .GetSummaryByPeriodAsync(period.PeriodBegin, period.PeriodEnd, cancellationToken);

            return _mapper.Map<List<SummaryServiceModel>>(await result);
        }

        /// <summary>
        /// Валидирует данные с фронта в .XML
        /// </summary>
        /// <param name="items"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Task<UploadDataResponseServiceModel> ValidateDataAsync(List<SensorDataItemXmlServiceModel> items, CancellationToken cancellationToken)
        {
            if (items == null || items.Count == 0)
            {
                throw new ArgumentNullException($"Объект {nameof(items)} пуст");
            }

            foreach (var item in items)
            {
                ValidateSensorData(_mapper.Map<SensorDataCreateServiceModel>(item));
            }

            return Task.FromResult(new UploadDataResponseServiceModel
            {
                Message = SuccessValidatedMessage,
            });
        }

        // Валидируем период
        private void ValidatePeriod(PeriodServiceModel period)
        {
            if (period == null)
            {
                throw new ArgumentNullException($"Объект {nameof(PeriodServiceModel)} пуст");
            }

            if (period.PeriodBegin.HasValue && 
                period.PeriodEnd.HasValue && 
                period.PeriodBegin > period.PeriodEnd)
            {
                throw new ArgumentException($"Дата начала периода ({period.PeriodBegin}) больше даты окончания периода ({period.PeriodEnd})");
            }
        }

        // Валидируем данные
        private void ValidateSensorData(SensorDataCreateServiceModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException($"Объект {nameof(SensorDataCreateServiceModel)} пуст");
            }

            if (Enum.IsDefined(typeof(SensorEnum), model.SensorId) == false)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var value in Enum.GetValues<SensorEnum>())
                {
                    sb.Append(" ");
                    sb.Append($"{(int)value} ({value})");
                }

                throw new ArgumentException($"Неверное значение {nameof(model.SensorId)}. Возможные значения:{sb.ToString()}");
            }
        }
    }
}
