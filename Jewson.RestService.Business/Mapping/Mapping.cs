using AutoMapper;
using Jewson.Data.DataModels;
using Jewson.RestService.Business.DTO;

namespace Jewson.RestService.Business.Mapping
{
    /// <summary>
    /// Auto Mapper configuration
    /// </summary>
    public class Mapping
    {
        public static IMapper Mapper { get; private set; }

        public static MapperConfiguration Configure()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                ConfigureMappings(cfg);
            });

            Mapper = configuration.CreateMapper();
            return configuration;
        }

        /// <summary>
        /// Setup model mapping
        /// </summary>
        /// <param name="cfg"></param>
        private static void ConfigureMappings(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Branch, BranchDTO>();
            cfg.CreateMap<Specialism, SpecialismDTO>();
        }
    }
}
