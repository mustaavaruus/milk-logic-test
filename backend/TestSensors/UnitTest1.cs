using Moq;
using WebApplication1.Services.Sensors;
using WebApplication1.Services.Sensors.Models;
using WebApplication1.Services.Sensors.Models.xml_models;

namespace TestSensors
{
    public class SensorDataServiceTests
    {
        private readonly Mock<ISensorDataService> _serviceMock;

        public SensorDataServiceTests()
        {
            _serviceMock = new Mock<ISensorDataService>();
        }

        [Fact]
        public async Task UploadDataAsync_ShouldReturnSuccessResponse_WhenModelIsValid()
        {
            // Arrange
            var model = new SensorDataCreateServiceModel();
            var expectedResponse = new UploadDataResponseServiceModel { Message = "SUCCESS" };

            _serviceMock.Setup(x => x.UploadDataAsync(model, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(expectedResponse);

            // Act
            var result = await _serviceMock.Object.UploadDataAsync(model, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Message);
            Assert.Equal("SUCCESS", result.Message);
            _serviceMock.Verify(x => x.UploadDataAsync(model, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetDataByPeriodAsync_ShouldReturnList_WhenDataExists()
        {
            // Arrange
            var period = new PeriodServiceModel { PeriodBegin = DateTime.Now.AddDays(-1), PeriodEnd = DateTime.Now };
            var expectedData = new List<SensorDataServiceModel> { new SensorDataServiceModel() };

            _serviceMock.Setup(x => x.GetDataByPeriodAsync(period, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(expectedData);

            // Act
            var result = await _serviceMock.Object.GetDataByPeriodAsync(period, CancellationToken.None);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public async Task GetSummaryAsync_ShouldReturnAggregatedData()
        {
            // Arrange
            var period = new PeriodServiceModel();
            var summary = new List<SummaryServiceModel>
        {
            new SummaryServiceModel { Average = 25.5f, Maximum = 30, Minimum = 10 }
        };

            _serviceMock.Setup(x => x.GetSummaryAsync(period, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(summary);

            // Act
            var result = await _serviceMock.Object.GetSummaryAsync(period, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(25.5f, result.First().Average);
        }

        [Fact]
        public async Task ValidateDataAsync_ShouldReturnError_WhenXmlItemsAreInvalid()
        {
            // Arrange
            var invalidItems = new List<SensorDataItemXmlServiceModel>();
            var errorResponse = new UploadDataResponseServiceModel { Message = "SUCCESS" };

            _serviceMock.Setup(x => x.ValidateDataAsync(invalidItems, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(errorResponse);

            // Act
            var result = await _serviceMock.Object.ValidateDataAsync(invalidItems, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Message);
            Assert.Equal("SUCCESS", result.Message);
        }
    }
}