using System.Runtime.Serialization;

namespace WebApplication1.Controllers.Sensors.Models
{
    /// <summary>
    /// Содель суммарного вывода
    /// </summary>
    [DataContract]
    public class SummaryContractModel
    {
        public int SensorId { get; set; }
        public float? Average { get; set; }
        public float? Maximum { get; set; }
        public float? Minimum { get; set; }
    }
}
