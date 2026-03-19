using System.Runtime.Serialization;

namespace WebApplication1.Controllers.Sensors.Models
{
    /// <summary>
    /// Модель для добавления значения в БД
    /// </summary>
    [DataContract]
    public class SensorDataCreateContractModel
    {
        public int SensorId { get; set; }

        public float Value { get; set; }
    }
}
