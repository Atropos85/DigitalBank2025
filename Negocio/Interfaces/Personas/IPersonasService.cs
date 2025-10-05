using Negocio.DTO.Personas;

namespace Negocio.Interfaces.Personas
{
    public interface IPersonasService
    {
        Task<PersonasDTO> CrearPersonaAsync(PersonasDTO persona);
        Task<PersonasDTO> ActualizaPersonaAsync(PersonasDTO persona);
        Task<List<PersonasDTO>> ListarPersonaAsync(int? Id = null);
        Task<PersonasDTO> EliminarPersonaAsync(int Id);
    }
}
