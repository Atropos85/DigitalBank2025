using Microsoft.AspNetCore.Mvc;
using Negocio.DTO.Personas;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentacion.Controllers.Personas
{
    public class PersonasController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public PersonasController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("PersonasApi");

            // Configurar opciones JSON para compatibilidad con la API
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        // 🔹 Listar personas
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/personas");

            if (!response.IsSuccessStatusCode)
                return View("Error", $"Error al obtener personas: {response.StatusCode}");

            var json = await response.Content.ReadAsStringAsync();
            var personas = JsonSerializer.Deserialize<List<PersonasDTO>>(json, _jsonOptions);

            return View(personas);
        }

        // 🔹 Vista para crear
        [HttpGet]
        public async Task<IActionResult> CreatePersona(int? id)
        {
            PersonasDTO model;

            if (id.HasValue)
            {
                // Obtener persona por Id desde la API
                var response = await _httpClient.GetAsync($"api/personas/{id}");
                if (!response.IsSuccessStatusCode)
                    return View("Error", $"Error al obtener persona: {response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();
                var personas = JsonSerializer.Deserialize<List<PersonasDTO>>(json, _jsonOptions);
                var persona = personas?.FirstOrDefault() ?? new PersonasDTO();
                return View(persona);
            }
            else
            {
                // Nuevo registro
                model = new PersonasDTO();
                return View(model);
            }            
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersona(PersonasDTO persona)
        {
            var json = JsonSerializer.Serialize(persona);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response;

            if (persona.IdPersona == 0)
            {   // Crear
                response = await _httpClient.PostAsync("api/personas", content);
            }
            else
            {   // Actualizar
                response = await _httpClient.PutAsync($"api/personas/{persona.IdPersona}", content);
            }

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Error al guardar la persona";
                return View(persona);
            }

            return RedirectToAction(nameof(Index));
        }

        // 🔹 Eliminar persona
        [HttpPost]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/personas/{id}");
            if (!response.IsSuccessStatusCode)
                ViewBag.Error = "Error al eliminar la persona";

            return RedirectToAction(nameof(Index));
        }
    }
}
