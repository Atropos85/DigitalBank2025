namespace Negocio.DTO.Personas
{
    public class PersonasDTO
    {
        public int IdPersona
        { get; set; }
        public string Nombre
        { get; set; }
        public DateTime FechaNacimiento 
        { get; set; } = DateTime.Today;
        public string Sexo
        { get; set; }
    }

    public class PersonasListaDTO
    {
        public int IdPersona
        { get; set; }
        public string Nombre
        { get; set; }
        public DateTime FechaNacimiento
        { get; set; } 
        public string Sexo
        { get; set; }
        public string SexoDesc
        { get; set; }
    }
}
