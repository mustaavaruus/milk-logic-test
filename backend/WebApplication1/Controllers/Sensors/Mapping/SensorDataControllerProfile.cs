using AutoMapper;
using WebApplication1.Controllers.Sensors.Models;
using WebApplication1.Services.Sensors.Models;
using WebApplication1.Services.Sensors.Models.xml_models;

namespace WebApplication1.Controllers.Sensors.Mapping
{
    public class SensorDataControllerProfile : Profile
    {
        public SensorDataControllerProfile() 
        {
            CreateMap<PeriodContractModel, PeriodServiceModel>();
            CreateMap<SensorDataContractModel, SensorDataServiceModel>().ReverseMap();
            CreateMap<SummaryServiceModel, SummaryContractModel>();
            CreateMap<SensorDataCreateContractModel, SensorDataCreateServiceModel>();
            CreateMap<UploadDataResponseServiceModel, UploadDataResponseContractModel>();

            CreateMap<SensorDataItemXmlRequestContractModel, SensorDataItemXmlServiceModel>();
        }
    }
}
