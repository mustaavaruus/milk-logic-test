using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;
using WebApplication1.Controllers.Sensors.Models;
using WebApplication1.Services.Sensors;
using WebApplication1.Services.Sensors.Models;
using WebApplication1.Services.Sensors.Models.xml_models;

namespace WebApplication1.Controllers.Sensors
{
    [Route("api/")]
    [ApiController]
    [SwaggerTag("Сигнал с датчиков")]
    public class SensorDataController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISensorDataService _sensorDataService;
        public SensorDataController(
            IMapper mapper,
            ISensorDataService sensorDataService
            )
        {
            _mapper = mapper;
            _sensorDataService = sensorDataService;
        }

        /// <summary>
        /// Принимает данные от эмулятора датчиков и сохраняет их в базе.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// 
        [HttpPost("data")]
        [SwaggerOperation(
        Summary = "Принимает данные от эмулятора датчиков и сохраняет их в базе",
        Description = "Принимает данные от эмулятора датчиков и сохраняет их в базе.",
        OperationId = "UploadData")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(UploadDataResponseContractModel), Description = "Данные добавлены успешно")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Ошибка добавления данных")]
        [ProducesResponseType(typeof(UploadDataResponseContractModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UploadDataResponseContractModel>> UploadData(
            [FromBody] SensorDataCreateContractModel model,
            CancellationToken cancellationToken)
        {
            var periodParameter = _mapper.Map<SensorDataCreateServiceModel>(model);
            var result = await _sensorDataService.UploadDataAsync(periodParameter, cancellationToken);

            return Ok(_mapper.Map<UploadDataResponseContractModel>(result));
        }

        /// <summary>
        /// Возвращает данные за заданный период времени.
        /// </summary>
        /// <param name="period"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("data")]
        [ProducesResponseType(typeof(List<SensorDataContractModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
        Summary = "Возвращает данные за заданный период времени",
        Description = "Возвращает данные за заданный период времени.",
        OperationId = "GetByPeriod")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<SensorDataContractModel>), Description = "Данные прочитаны успешно")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Ошибка чтения данных")]
        public async Task<ActionResult<List<SensorDataContractModel>>> GetByPeriod(
            [FromQuery] PeriodContractModel period,
            CancellationToken cancellationToken)
        {
            var periodParameter = _mapper.Map<PeriodServiceModel>(period);
            return Ok(_mapper.Map<List<SensorDataContractModel>>(
                await _sensorDataService
                    .GetDataByPeriodAsync(periodParameter, cancellationToken)));
        }

        /// <summary>
        /// Возвращает агрегированные данные (среднее, максимум, минимум) за указанный период.
        /// </summary>
        /// <param name="period"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("sensors/summary")]
        [ProducesResponseType(typeof(SummaryContractModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
        Summary = "Возвращает агрегированные данные (среднее, максимум, минимум) за указанный период",
        Description = "Возвращает агрегированные данные (среднее, максимум, минимум) за указанный период.",
        OperationId = "GetSummaryByPeriod")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SensorDataContractModel), Description = "Данные прочитаны успешно")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Ошибка чтения данных")]
        public async Task<ActionResult<List<SummaryContractModel>>> GetSummaryByPeriod(
            [FromQuery] PeriodContractModel period,
            CancellationToken cancellationToken)
        {
            var periodParameter = _mapper.Map<PeriodServiceModel>(period);
            return Ok(_mapper.Map< List<SummaryContractModel>>(
                await _sensorDataService
                    .GetSummaryAsync(periodParameter, cancellationToken)));
        }

        /// <summary>
        /// Функционал для валидации данных, отправляемых с фронтенда в формате XML. 
        /// </summary>
        /// <param name="IncomingXML"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("validate-xml-data")]
        [Consumes("application/xml")]
        [ProducesResponseType(typeof(UploadDataResponseContractModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
        Summary = "Функционал для валидации данных, отправляемых с фронтенда в формате XML",
        Description = "Реализованный функционал для валидации данных, отправляемых с фронтенда в формате XML. После успешной обработки данных сервис должен возвращать статусное сообщение.",
        OperationId = "ValidateData")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SensorDataContractModel), Description = "Данные прочитаны успешно")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Ошибка чтения данных")]
        public async Task<ActionResult<UploadDataResponseContractModel>> ValidateData([FromBody] SensorDataXmlRequestContractModel IncomingXML, CancellationToken cancellationToken)
        {

            var dataForValidation = _mapper.Map<List<SensorDataItemXmlServiceModel>>(IncomingXML?.Signals);
            var result = await _sensorDataService.ValidateDataAsync(dataForValidation, cancellationToken);
            

            return Ok(_mapper.Map<UploadDataResponseContractModel>(result));
        }
    }
}
