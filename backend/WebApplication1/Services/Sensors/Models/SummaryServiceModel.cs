namespace WebApplication1.Services.Sensors.Models
{
    public class SummaryServiceModel
    {
        public int SensorId { get; set; }
        public float? Average { get; set; }
        public float? Maximum { get; set; }
        public float? Minimum { get; set; }
    }
}
