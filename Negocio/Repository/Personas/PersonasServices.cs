using AutoMapper;
using Datos.Models.Personas;
using Microsoft.Data.SqlClient;
using Negocio.DTO.Personas;
using Negocio.Interfaces.Personas;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Negocio.Services.Personas
{
    public class PersonasService : IPersonasService
    {
        private readonly IPersonasRepository _repositoryPersonas;
        private readonly IMapper _mapper;

        public PersonasService(IPersonasRepository repositoryPersonas, IMapper mapper)
        {
            _repositoryPersonas = repositoryPersonas;
            _mapper = mapper;
        }
        public Task<PersonasDTO> CrearPersonaAsync(PersonasDTO persona) =>
        EjecutarOperacionAsync(async () =>
        {
            var entity = _mapper.Map<PersonasEntity>(persona);
            var result = await _repositoryPersonas.ICrearPersonaAsync(entity);
            return _mapper.Map<PersonasDTO>(result);
        });

        public Task<PersonasDTO> ActualizaPersonaAsync(PersonasDTO persona) =>
        EjecutarOperacionAsync(async () =>
        {
            var entity = _mapper.Map<PersonasEntity>(persona);
            var result = await _repositoryPersonas.IActualizaPersonaAsync(entity);
            return _mapper.Map<PersonasDTO>(result);
        });

        public Task<List<PersonasListaDTO>> ListarPersonaAsync(int? Id = null) =>
        EjecutarOperacionAsync(async () =>
        {
            var result = await _repositoryPersonas.IListarPersonaAsync(Id);
            return _mapper.Map<List<PersonasListaDTO>>(result);
        });

        public Task<PersonasDTO> EliminarPersonaAsync(int Id) =>
        EjecutarOperacionAsync(async () =>
        {
            var result = await _repositoryPersonas.IEliminarPersonaAsync(Id);
            return _mapper.Map<PersonasDTO>(result);
        });

        private async Task<T> EjecutarOperacionAsync<T>(Func<Task<T>> operacion)
        {
            try
            {
                return await operacion();
            }
            catch (DomainValidationException)
            {
                throw;
            }
            catch (SqlException ex)
            {
                var errors = new Dictionary<string, string>
        {
            { "SQL", $"Error al acceder a la base de datos: {ex.Message}" }
        };

                throw new DomainValidationException(
                    message: "Error de base de datos.",
                    errors: errors,
                    statusCode: 500
                );
            }
            catch (Exception ex)
            {
                var errors = new Dictionary<string, string>
        {
            { "General", $"Ocurrió un error inesperado: {ex.Message}" }
        };

                throw new DomainValidationException(
                    message: "Error interno del servidor.",
                    errors: errors,
                    statusCode: 500
                );
            }
        }

    }
}
