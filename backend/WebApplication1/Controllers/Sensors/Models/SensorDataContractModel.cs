using System.Runtime.Serialization;

namespace WebApplication1.Controllers.Sensors.Models
{
    /// <summary>
    /// Модель данных сенсора
    /// </summary>
    [DataContract]
    public class SensorDataContractModel
    {
        public int Id { get; set; }

        public int SensorId { get; set; }

        public DateTime Timestamp { get; set; }

        public float Value { get; set; }
    }
}
