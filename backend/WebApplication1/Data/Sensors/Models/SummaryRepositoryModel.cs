namespace WebApplication1.Data.Sensors.Models
{
    public class SummaryRepositoryModel
    {
        public int SensorId { get; set; }
        public float? Average { get; set; }
        public float? Maximum { get; set; }
        public float? Minimum { get; set; }
    }
}
