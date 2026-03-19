using System.Runtime.Serialization;

namespace WebApplication1.Controllers.Sensors.Models
{
    /// <summary>
    /// Модель ответа
    /// </summary>
    [DataContract]
    public class UploadDataResponseContractModel
    {
        public string? Message { get; set; }
    }
}
