using Mapster;

namespace GreenFood.Application.Mapster
{
    public static class MapperConfig
    {
        public static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = new TypeAdapterConfig
            {
                Compiler = exp => exp.Compile()
            };

            new RegisterMapper().Register(config);

            return config;
        }
    }
}
