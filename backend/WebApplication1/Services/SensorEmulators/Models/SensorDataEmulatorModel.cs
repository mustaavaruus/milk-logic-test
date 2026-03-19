using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Services.SensorEmulators.Models
{
    public class SensorDataEmulatorModel
    {
        public int SensorId { get; set; }

        public float Value { get; set; }
    }
}
