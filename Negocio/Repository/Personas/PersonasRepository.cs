using Datos.Models.Personas;
using Datos.Utils;
using Negocio.Interfaces.Personas;
using System.Data;

namespace Negocio.Services.Personas
{
    public class PersonasRepository : IPersonasRepository
    {
        public async Task<PersonasEntity> ICrearPersonaAsync(PersonasEntity persona)
        {
            var parametros = new Dictionary<string, object>
            {   {"@operacion", "C"},
                {"@Nombre", persona.Nombre},
                {"@Fechanac", persona.FechaNacimiento},
                {"@Sexo", persona.Sexo}
            };

            await DbHelper.ExecuteNonQueryAsync("sp_AdminPersona", parametros);
            return persona; // o mapear si el SP devuelve el nuevo ID
        }
        public async Task<PersonasEntity> IActualizaPersonaAsync(PersonasEntity persona)
        {
            var parametros = new Dictionary<string, object>
            {   {"@operacion", "U"},
                {"@idpersona", persona.IdPersona},
                {"@Nombre", persona.Nombre},
                {"@Fechanac", persona.FechaNacimiento},
                {"@Sexo", persona.Sexo}
            };

            await DbHelper.ExecuteNonQueryAsync("sp_AdminPersona", parametros);
            return persona; // o mapear si el SP devuelve el nuevo ID
        }

        public async Task<List<PersonasEntity>> IListarPersonaAsync(int? id = null)
        {
            var parametros = new Dictionary<string, object>
            {   {"@operacion", "R"},
                {"@idpersona", id}
            };

            DataTable dt = await DbHelper.ExecuteQueryAsync("sp_AdminPersona", parametros);

            return dt.AsEnumerable().Select(row => new PersonasEntity
            {
                IdPersona = row.Field<int>("IdPersona"),
                Nombre = row.Field<string>("Nombre"),
                FechaNacimiento = row.Field<DateTime>("FechaNacimiento"),
                Sexo = row.Field<string>("Sexo")
            }).ToList();
        }

        public async Task<PersonasEntity> IEliminarPersonaAsync(int id)
        {
            var parametros = new Dictionary<string, object>
            {
                {"@operacion", "D"},
                {"@idpersona", id}
            };

            await DbHelper.ExecuteNonQueryAsync("sp_AdminPersona", parametros);

            // Devolver un "placeholder" o null depende de tu necesidad
            return new PersonasEntity
            {
                IdPersona = id,
                Nombre = string.Empty,
                FechaNacimiento = DateTime.MinValue,
                Sexo = string.Empty
            };
        }
    }
}
