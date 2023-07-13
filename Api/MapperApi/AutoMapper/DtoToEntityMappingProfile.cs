using AutoMapper;
using MapperApi.Model.Entity;
using MapperApi.Model.RequestDto;

namespace MapperApi.AutoMapper
{
    public class DtoToEntityMappingProfile : Profile
    {
        public DtoToEntityMappingProfile()
        {
            CreateMap<CreateActivityRequestDto, ActivityEntities>()
              .ForMember("KeyId", opt => opt.Ignore());
        }
    }
}
