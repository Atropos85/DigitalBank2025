using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Diagnostics.Metrics;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Datos.Utils
{
    public static class DbHelper
    {
        private static readonly string _connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DigitalBank;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public static async Task<int> ExecuteNonQueryAsync(string spName, Dictionary<string, object>? parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(spName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (var param in parameters)
                        cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }

                await conn.OpenAsync();
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async Task<DataTable> ExecuteQueryAsync(string spName, Dictionary<string, object>? parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(spName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (var param in parameters)
                        cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    await Task.Run(() => adapter.Fill(dt));
                    return dt;
                }
            }
        }
    }
}
