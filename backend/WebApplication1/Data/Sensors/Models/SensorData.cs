using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.Sensors.Models
{
    [Table("sensor_data")]
    public class SensorData
    {
        [Column("id")]
        [Required]
        public int Id { get; set; }

        [Column("sensorid")]
        [Required]
        public int SensorId { get; set; }

        [Column("timestamp")]
        [Required]
        public DateTime Timestamp { get; set; }

        [Column("value")]
        [Required]
        public float Value { get; set; }
    }
}
