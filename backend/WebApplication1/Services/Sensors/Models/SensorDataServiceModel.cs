namespace WebApplication1.Services.Sensors.Models
{
    public class SensorDataServiceModel
    {
        public int? Id { get; set; }

        public int SensorId { get; set; }

        public DateTime Timestamp { get; set; }

        public float Value { get; set; }
    }
}
