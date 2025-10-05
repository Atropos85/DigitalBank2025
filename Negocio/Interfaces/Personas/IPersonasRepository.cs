using Datos.Models.Personas;

namespace Negocio.Interfaces.Personas
{
    public interface IPersonasRepository
    {
        Task<PersonasEntity> ICrearPersonaAsync(PersonasEntity persona);
        Task<PersonasEntity> IActualizaPersonaAsync(PersonasEntity persona);
        Task<List<PersonasEntity>> IListarPersonaAsync(int? Id = null);
        Task<PersonasEntity> IEliminarPersonaAsync(int Id);
    }
}
