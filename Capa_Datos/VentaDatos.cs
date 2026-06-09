using Capa_Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace Capa_Datos
{

    // Implementa IVentaDatos para permitir inyección y mocking en pruebas unitarias
    public class VentaDatos : IVentaDatos
    {
        private readonly string _connectionString;

        public VentaDatos(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void RegistrarVenta(Ventas venta, List<DetalleVenta> detalles)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();
            using var transaccion = conexion.BeginTransaction();

            try
            {
                // Insertar venta y obtener el ID
                using var cmdVenta = new SqlCommand("Sp_InsertarVenta", conexion, transaccion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmdVenta.Parameters.AddWithValue("@FechaVenta", venta.FechaVenta);
                cmdVenta.Parameters.AddWithValue("@Total", venta.Total);

                var paramVentaID = new SqlParameter("@VentaID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmdVenta.Parameters.Add(paramVentaID);

                cmdVenta.ExecuteNonQuery();
                int ventaId = (int)paramVentaID.Value;

                // Insertar detalles
                foreach (var det in detalles)
                {
                    using var cmdDetalle = new SqlCommand("Sp_InsertarDetalleVenta", conexion, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmdDetalle.Parameters.AddWithValue("@VentaID", ventaId);
                    cmdDetalle.Parameters.AddWithValue("@ProductoID", det.ProductoID);
                    cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", det.PrecioUnitario);
                    cmdDetalle.Parameters.AddWithValue("@Cantidad", det.Cantidad);
                    cmdDetalle.Parameters.AddWithValue("@Subtotal", det.Subtotal);

                    cmdDetalle.ExecuteNonQuery();
                }

                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }

        public List<Ventas> ObtenerVentas()
        {
            var lista = new List<Ventas>();

            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();
            using var cmd = new SqlCommand("Sp_ObtenerVentas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Ventas
                {
                    VentaID = Convert.ToInt32(reader["VentaID"]),
                    FechaVenta = Convert.ToDateTime(reader["FechaVenta"]),
                    Total = Convert.ToDecimal(reader["Total"])
                });
            }

            return lista;
        }

        public Ventas? ObtenerVentaPorId(int id)
        {
            Ventas? venta = null;

            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();
            using var cmd = new SqlCommand("Sp_ObtenerVentaPorId", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@VentaID", id);

            using var reader = cmd.ExecuteReader();

            // Leer la venta principal
            if (reader.Read())
            {
                venta = new Ventas
                {
                    VentaID = Convert.ToInt32(reader["VentaID"]),
                    FechaVenta = Convert.ToDateTime(reader["FechaVenta"]),
                    Total = Convert.ToDecimal(reader["Total"]),
                    Detalles = new List<DetalleVenta>()
                };
            }

            // Pasar al siguiente result set (detalles)
            if (venta != null && reader.NextResult())
            {
                while (reader.Read())
                {
                    venta.Detalles.Add(new DetalleVenta
                    {
                        DetalleVentaID = Convert.ToInt32(reader["DetalleVentaID"]),
                        VentaID = Convert.ToInt32(reader["VentaID"]),
                        ProductoID = Convert.ToInt32(reader["ProductoID"]),
                        Nombre = Convert.ToString(reader["NombreProducto"]) ?? string.Empty,
                        PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                        Cantidad = Convert.ToInt32(reader["Cantidad"]),
                        Subtotal = Convert.ToDecimal(reader["Subtotal"])
                    });
                }
            }

            return venta;
        }
    }
}