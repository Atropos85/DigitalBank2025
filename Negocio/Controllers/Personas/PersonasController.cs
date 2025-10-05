using Negocio.DTO.Personas;
using Microsoft.AspNetCore.Mvc;
using Negocio.Interfaces.Personas;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        // GET: api/<PersonasController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonasDTO>>> Get()
        {
            var lista = await _persona.ListarPersonaAsync();
            return lista;
        }

        // GET api/<PersonasController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PersonasDTO>>> Get(int id)
        {
            var detalle =await _persona.ListarPersonaAsync(id);
            return detalle;
        }

        // POST api/<PersonasController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PersonasDTO persona)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creada = await _persona.CrearPersonaAsync(persona);

            // devuelve la ruta del nuevo recurso creado
            return CreatedAtAction(nameof(Get), new { id = creada.IdPersona }, creada);
        }

        // PUT api/<PersonasController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PersonasDTO persona)
        {
            var actualizada =await _persona.ActualizaPersonaAsync(persona);
            if (actualizada == null)
                return NotFound();

            return NoContent(); // 204
        }


        // DELETE api/<PersonasController>/5
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
