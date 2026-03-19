using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Sensors.Models;

namespace WebApplication1.Data.Sensors
{
    public class SensorDataDbContext : DbContext
    {
        public DbSet<SensorData> SensorData { get; set; }

        public SensorDataDbContext(DbContextOptions<SensorDataDbContext> options) : base(options)
        {

        }
    }
}
