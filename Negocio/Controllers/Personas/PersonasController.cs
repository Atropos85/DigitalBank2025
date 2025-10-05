using Datos.Models.Personas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Negocio.DTO.Personas;
using Negocio.Interfaces.Personas;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        IPersonasService _persona;

        public PersonasController(IPersonasService persona)
        {
            _persona = persona;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonasListaDTO>>> Get()
        {
            try
            {
                var lista = await _persona.ListarPersonaAsync();
                return lista;
            }
            catch (DomainValidationException ex)
            {
                return StatusCode(ex.StatusCode, new
                {
                    Message = ex.Message,
                    Errors = ex.Errors
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PersonasListaDTO>>> Get(int id)
        {
            try
            {
            var detalle =await _persona.ListarPersonaAsync(id);
            return detalle;
            }
            catch (DomainValidationException ex)
            {
                return StatusCode(ex.StatusCode, new
                {
                    Message = ex.Message,
                    Errors = ex.Errors
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PersonasDTO persona)
        {
            try
            {
                var creada = await _persona.CrearPersonaAsync(persona);
                return CreatedAtAction(nameof(Get), new { id = creada.IdPersona }, creada);
            }
            catch (DomainValidationException ex)
            {
                return StatusCode(ex.StatusCode, new
                {
                    Message = ex.Message,
                    Errors = ex.Errors
                });
            }
        }

   

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PersonasDTO persona)
        {
            try
            {
                var actualizada =await _persona.ActualizaPersonaAsync(persona);
                if (actualizada == null)
                    return NotFound();
                return NoContent();
            }
            catch (DomainValidationException ex)
            {
                return StatusCode(ex.StatusCode, new
                {
                    Message = ex.Message,
                    Errors = ex.Errors
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            { 
                var eliminada =await _persona.EliminarPersonaAsync(id);
                if (eliminada == null)
                    return NotFound();

                return NoContent();
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                    Errors = ex.Errors
                });
            }
        }
    }
}
