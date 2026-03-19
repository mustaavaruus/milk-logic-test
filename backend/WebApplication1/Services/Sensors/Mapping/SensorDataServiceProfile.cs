using AutoMapper;
using WebApplication1.Data.Sensors.Models;
using WebApplication1.Services.Sensors.Models;
using WebApplication1.Services.Sensors.Models.xml_models;

namespace WebApplication1.Services.Sensors.Mapping
{
    public class SensorDataServiceProfile : Profile

    {
        public SensorDataServiceProfile() 
        {
            CreateMap<SummaryRepositoryModel, SummaryServiceModel>();
            CreateMap<SensorData, SensorDataServiceModel>().ReverseMap();
            CreateMap<SensorDataCreateServiceModel, SensorData>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Timestamp, opt => opt.Ignore());

            CreateMap<SensorDataItemXmlServiceModel, SensorDataCreateServiceModel>();
        }
    }
}
