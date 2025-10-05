using Negocio.DTO.Personas;
using Microsoft.AspNetCore.Mvc;
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
            var lista = await _persona.ListarPersonaAsync();
            return lista;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PersonasListaDTO>>> Get(int id)
        {
            var detalle =await _persona.ListarPersonaAsync(id);
            return detalle;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PersonasDTO persona)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creada = await _persona.CrearPersonaAsync(persona);

            return CreatedAtAction(nameof(Get), new { id = creada.IdPersona }, creada);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PersonasDTO persona)
        {
            var actualizada =await _persona.ActualizaPersonaAsync(persona);
            if (actualizada == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var eliminada =await _persona.EliminarPersonaAsync(id);
            if (eliminada == null)
                return NotFound();

            return NoContent();
        }
    }
}
