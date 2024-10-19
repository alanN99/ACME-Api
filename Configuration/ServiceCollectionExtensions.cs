using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ACME_Api.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddMaps(typeof(Startup)));

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
