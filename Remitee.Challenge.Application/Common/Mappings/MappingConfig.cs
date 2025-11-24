using Mapster;



namespace Remitee.Challenge.Application.Common.Mappings
{
    public static class MappingConfig
    {
        public static void RegisterMappings(TypeAdapterConfig config)
        {
            //config.NewConfig<ProcessItem, MonitorDto>()
            //    .Map(dest => dest.ProcessDescription, src => src.ProcessDescription); // Ejemplo (puede ser automático)


            //config.NewConfig<ProcessItem, LookupDto>()
            //     .Map(dest => dest.Description, src => src.ProcessDescription);



            //// Mapping List<Rol> -> AvailableRolesByPerfilDto
            //config.NewConfig<List<Rol>, AvailableRolesByPerfilDto>()
            //    .Map(dest => dest.Roles, src => src.Adapt<List<RolResumenDto>>());





        }
    }
}
