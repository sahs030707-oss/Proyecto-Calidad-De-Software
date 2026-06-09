using Capa_Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Capa_Datos
{
    // Implementa IMovimientosInventarioDatos para permitir inyección y mocking en pruebas unitarias
    public class MovimientosInventarioDatos : IMovimientosInventarioDatos
    {
        private readonly string _connectionString;

        public MovimientosInventarioDatos(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void InsertarMovimiento(MovimientosInventario movimiento)
        {
            using var con = new SqlConnection(_connectionString);

            con.Open();

            using var cmd = new SqlCommand("Sp_InsertarMovimientoInventario", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ProductoID", movimiento.ProductoID);
            cmd.Parameters.AddWithValue("@Cantidad", movimiento.Cantidad);
            cmd.Parameters.AddWithValue("@Motivo", movimiento.Motivo);

            cmd.ExecuteNonQuery();
        }
    }
}