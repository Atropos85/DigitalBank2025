namespace Datos.Models.Personas
{
    public class PersonasEntity
    {
        public int IdPersona
        { get; set; }
        required
        public string Nombre
        { get; set; }
        required
        public DateTime FechaNacimiento
        { get; set; }
        required
        public string Sexo
        { get; set; }
    }
}
