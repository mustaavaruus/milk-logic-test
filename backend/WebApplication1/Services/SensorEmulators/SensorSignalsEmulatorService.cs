using System;
using WebApplication1.Services.SensorEmulators.Models;

namespace WebApplication1.Services.SensorEmulators
{
    public static class SensorSignalsEmulatorService
    {
        public static SensorDataEmulatorModel GetData(int sensorId)
        {
            Random random = new Random();

            return new SensorDataEmulatorModel
            {
                SensorId = sensorId,
                Value = random.Next(0, 101)
            };
        }
    }
}
