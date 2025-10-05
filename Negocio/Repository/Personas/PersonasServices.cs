using AutoMapper;
using Datos.Models.Personas;
using Negocio.DTO.Personas;
using Negocio.Interfaces.Personas;


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

        public async Task<PersonasDTO> CrearPersonaAsync(PersonasDTO persona)
        {
            var entity = _mapper.Map<PersonasEntity>(persona);
            var result = await _repositoryPersonas.ICrearPersonaAsync(entity);
            return _mapper.Map<PersonasDTO>(result);
        }

        public async Task<PersonasDTO> ActualizaPersonaAsync(PersonasDTO persona)
        {
            var entity = _mapper.Map<PersonasEntity>(persona);
            var result = await _repositoryPersonas.IActualizaPersonaAsync(entity);
            return _mapper.Map<PersonasDTO>(result);
        }

        public async Task<List<PersonasListaDTO>> ListarPersonaAsync(int? Id = null)
        {
            var result = await _repositoryPersonas.IListarPersonaAsync(Id);
            return _mapper.Map<List<PersonasListaDTO>>(result);
        }

        public async Task<PersonasDTO> EliminarPersonaAsync(int Id)
        {
            var result = await _repositoryPersonas.IEliminarPersonaAsync(Id);
            return _mapper.Map<PersonasDTO>(result);
        }
    }
}
