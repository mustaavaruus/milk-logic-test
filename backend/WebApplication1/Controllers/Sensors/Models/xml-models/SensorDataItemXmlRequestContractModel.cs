using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WebApplication1.Controllers.Sensors.Models
{
    [DataContract]
    public class SensorDataItemXmlRequestContractModel
    {
        [XmlElement("sensorId")]
        public int SensorId { get; set; }

        [XmlElement("timestamp")]
        public DateTime Timestamp { get; set; }

        [XmlElement("value")]
        public float Value { get; set; }
    }
}
