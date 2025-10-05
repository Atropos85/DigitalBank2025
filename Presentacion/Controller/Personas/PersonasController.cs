using Datos.Models.Personas;
using Microsoft.AspNetCore.Mvc;
using Negocio.DTO.Personas;
using Presentacion.Utils;   
using System.Text;
using System.Text.Json;
using static Negocio.Enumerable.Personas.Enumerables;

namespace Presentacion.Controllers.Personas
{
    public class PersonasController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public PersonasController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("PersonasApi");

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/personas");

            if (!response.IsSuccessStatusCode)
            {
                await HTTPErrorHandler.HandleApiErrorAsync(response, this);
                return View();
            }

            var json = await response.Content.ReadAsStringAsync();
            var personas = JsonSerializer.Deserialize<List<PersonasListaDTO>>(json, _jsonOptions);

            return View(personas);
        }

        [HttpGet]
        public async Task<IActionResult> CreatePersona(int? id)
        {
            PersonasDTO model;
            ViewBag.Sexos = SexoEnum.Sexos;

            if (id.HasValue)
            {
                var response = await _httpClient.GetAsync($"api/personas/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    await HTTPErrorHandler.HandleApiErrorAsync(response, this);
                    model = new PersonasDTO();
                    return View(model);
                }

                var json = await response.Content.ReadAsStringAsync();
                var personas = JsonSerializer.Deserialize<List<PersonasDTO>>(json, _jsonOptions);
                var persona = personas?.FirstOrDefault() ?? new PersonasDTO();
                return View(persona);
            }
            else
            {
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
            string mensaje;

            if (persona.IdPersona == 0)
            {    
                response = await _httpClient.PostAsync("api/personas", content);
                mensaje = "Persona guardada exitosamente";
            }
            else
            {
                response = await _httpClient.PutAsync($"api/personas/{persona.IdPersona}", content);
                mensaje = "Persona actualizada exitosamente";
            }

            if (!response.IsSuccessStatusCode)
            {
                await HTTPErrorHandler.HandleApiErrorAsync(response, this);
                ViewBag.Sexos = SexoEnum.Sexos; 
                return View(persona);
            }
            TempData["Estado"] = "success";
            TempData["Mensaje"] = mensaje;
            return RedirectToAction(nameof(Index));            
        }

        [HttpPost]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/personas/{id}");
            if (!response.IsSuccessStatusCode) if (!response.IsSuccessStatusCode)
            {
                await HTTPErrorHandler.HandleApiErrorAsync(response, this);
                return View();
            }
            TempData["Estado"] = "success";
            TempData["Mensaje"] = "Persona eliminada correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
