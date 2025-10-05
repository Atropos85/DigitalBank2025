using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Presentacion.Utils
{
    public class HTTPErrorHandler
    {
        public class ErrorResponse
        {
            public string Message { get; set; }
            public Dictionary<string, string> Errors { get; set; } = new();
        }
        public static async Task HandleApiErrorAsync(HttpResponseMessage response, Controller controller)
        {
            string errorContent = await response.Content.ReadAsStringAsync();

            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(
                errorContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            controller.TempData["Estado"] = "error";
            controller.TempData["Mensaje"] = errorResponse?.Message ?? "Ocurrió un error inesperado.";

            if (errorResponse?.Errors != null)
            {
                foreach (var error in errorResponse.Errors)
                {
                    controller.ModelState.AddModelError(error.Key, error.Value);
                }
            }
        }
    }
}
