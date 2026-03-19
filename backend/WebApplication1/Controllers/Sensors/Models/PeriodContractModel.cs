using System.Runtime.Serialization;

namespace WebApplication1.Controllers.Sensors.Models
{
    /// <summary>
    /// Модель запроса данных за период
    /// </summary>
    [DataContract]
    public class PeriodContractModel
    {
        [DataMember(Name = "period_begin")]
        public DateTime? PeriodBegin { get; set; }

        [DataMember(Name = "period_end")]
        public DateTime? PeriodEnd { get; set; }
    }
}
