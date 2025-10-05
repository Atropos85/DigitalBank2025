namespace Negocio.Enumerable.Personas
{
    public class Enumerables
    {
        public static class SexoEnum
        {
            public static readonly List<SexoItem> Sexos = new List<SexoItem>
        {
            new SexoItem { Valor = "M", Descripcion = "Masculino" },
            new SexoItem { Valor = "F", Descripcion = "Femenino" }
        };
        }
        public class SexoItem
        {
            public string Valor { get; set; } = null!;
            public string Descripcion { get; set; } = null!;
        }
    }
}
