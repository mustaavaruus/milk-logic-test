using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WebApplication1.Controllers.Sensors.Models
{
    [DataContract]
    public class SensorDataXmlRequestContractModel
    {
        [XmlElement("signals")]
        public List<SensorDataItemXmlRequestContractModel> Signals { get; set; } 
            = new List<SensorDataItemXmlRequestContractModel>();
    }
}
