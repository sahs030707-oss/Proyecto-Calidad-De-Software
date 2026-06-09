using Capa_Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace Capa_Datos
{
    // Implementa IInventarioDatos para permitir inyección y mocking en pruebas unitarias
    public class InventarioDatos : IInventarioDatos
    {
        private readonly string _connectionString;

        public InventarioDatos(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Obtener inventario
        public IEnumerable<Inventario> ObtenerInventario()
        {
            var lista = new List<Inventario>();
            using var con = new SqlConnection(_connectionString);
            con.Open();
            using var cmd = new SqlCommand("Sp_ObtenerInventario", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Inventario
                {
                    InventarioID = Convert.ToInt32(reader["InventarioID"]),
                    ProductoID = Convert.ToInt32(reader["ProductoID"]),
                    NombreProducto = reader["NombreProducto"].ToString(),
                    Categoria = reader["Categoria"].ToString(),
                    PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                    Estado = Convert.ToBoolean(reader["Estado"]),
                    StockActual = Convert.ToInt32(reader["StockActual"]),
                    Fecha = Convert.ToDateTime(reader["Fecha"])
                });
            }
            return lista;
        }

        // Aumentar stock
        public void AumentarStock(int productoId, int cantidad)
        {
            using var con = new SqlConnection(_connectionString);
            con.Open();
            using var cmd = new SqlCommand("Sp_AumentarStock", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@ProductoID", productoId);
            cmd.Parameters.AddWithValue("@Cantidad", cantidad);
            cmd.ExecuteNonQuery();
        }
    }
}