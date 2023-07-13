using AutoMapper;
using MapperApi.Model.Entity;
using MapperApi.Model.ResponseDto;

namespace MapperApi.AutoMapper
{
    /// <summary>
    /// 实体映射为DTO配置
    /// </summary>
    public class EntityToDtoMappingProfile: Profile
    {
        /// <summary>
        /// 映射配置
        /// </summary>
        public EntityToDtoMappingProfile()
        {
            CreateMap<ActivityEntities, ActivityResponseDto>();
        }
    }
}
