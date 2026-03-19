
using System;
using System.Net.Http;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Text.Json;
using System.Timers;
using WebApplication1.Services.SensorEmulators;
using WebApplication1.Services.SensorEmulators.Models;

namespace WebApplication1.Services.Senders
{
    public class SensorsBackgroundService : BackgroundService
    {
        private const int TimePeriod = 1;
        private readonly IHttpClientFactory _httpClientFactory;

        public SensorsBackgroundService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                var tasks = new List<Task>
                {
                    CallHttpEndpointAsync(1),
                    CallHttpEndpointAsync(2),
                    CallHttpEndpointAsync(3)
                };

                await Task.WhenAll(tasks);

                await Task.Delay(TimeSpan.FromSeconds(TimePeriod), stoppingToken);
            }
        }

        private async Task CallHttpEndpointAsync(int sensorId)
        {
            try
            {
                using var client = _httpClientFactory.CreateClient();
                var json = JsonSerializer.Serialize(SensorSignalsEmulatorService.GetData(sensorId));
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("http://localhost:5100/api/data", data);
                

                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception($"{response.StatusCode} {response.Content}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Возникла ошибка эмулятора датчиков: {ex.Message}");
            }
        }
    }
}
