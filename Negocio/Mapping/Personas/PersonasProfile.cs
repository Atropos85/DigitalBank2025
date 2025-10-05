using AutoMapper;
using Datos.Models.Personas;
using Negocio.DTO.Personas;
using Negocio.Enumerable.Personas;
using static Negocio.Enumerable.Personas.Enumerables;

public class PersonasProfile : Profile
{
    public PersonasProfile()
    {
        CreateMap<PersonasEntity, PersonasDTO>();
        CreateMap<PersonasDTO, PersonasEntity>();
        CreateMap<PersonasEntity, PersonasListaDTO>()
            .ForMember(dest => dest.SexoDesc,
                       opt => opt.MapFrom(src => SexoEnum.Sexos
                                                 .FirstOrDefault(s => s.Valor == src.Sexo)!.Descripcion));
    }
}
