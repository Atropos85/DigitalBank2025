using AutoMapper;
using Datos.Models.Personas;
using Negocio.DTO.Personas;

public class PersonasProfile : Profile
{
    public PersonasProfile()
    {
        CreateMap<PersonasEntity, PersonasDTO>();
        CreateMap<PersonasDTO, PersonasEntity>();
    }
}
