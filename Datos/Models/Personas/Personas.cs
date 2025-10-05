using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Datos.Models.Personas
{
    public class PersonasEntity
    {
        public PersonasEntity() { }
        public int IdPersona
        { get; set; }
        
        public string Nombre
        { get; set; }
       
        public DateTime FechaNacimiento
        { get; set; }
       
        public string Sexo
        { get; set; }


        public PersonasEntity(string nombre, DateTime fechaNacimiento, string sexo)
        {
            var errors = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(nombre))
                errors[nameof(Nombre)] = "El nombre es obligatorio";
            else if (!System.Text.RegularExpressions.Regex.IsMatch(nombre, @"^[a-zA-Z]+$"))
                errors[nameof(Nombre)] = "El nombre solo debe contener letras";

            if (string.IsNullOrWhiteSpace(sexo))
                errors[nameof(Sexo)] = "El sexo es obligatorio";

            if (errors.Any())
                throw new DomainValidationException(
                    message: "Se encontraron errores de validación al crear la persona.",
                    errors: errors,
                    statusCode: 400
                );

            Nombre = nombre;
            FechaNacimiento = fechaNacimiento;
            Sexo = sexo;
        }
    }
    public class DomainValidationException : Exception
    {
        public int StatusCode { get; }
        public Dictionary<string, string>? Errors { get; }

        public DomainValidationException(string message, Dictionary<string, string>? errors = null, int statusCode = 400)
            : base(message)
        {
            Errors = errors;
            StatusCode = statusCode;
        }
    }

}
