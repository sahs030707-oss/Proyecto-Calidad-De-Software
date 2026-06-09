using Capa_Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace Capa_Datos
{
    // Implementa ICategoriaDatos para permitir inyección y mocking en pruebas unitarias
    public class CategoriaDatos : ICategoriaDatos
    {
        private readonly string _connectionString;

        public CategoriaDatos(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Obtener todas las categorías
        public virtual List<Categorias> ObtenerCategorias()
        {
            var lista = new List<Categorias>();

            using (var conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("Sp_ObtenerCategorias", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Categorias
                            {
                                CategoriaID = Convert.ToInt32(reader["CategoriaID"]),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            });
                        }
                    }
                }
            }

            return lista;
        }

        //Obtener categoría por ID
        public virtual Categorias? ObtenerCategoriaPorId(int id)
        {
            Categorias? categoria = null;

            using (var conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();

                using (var cmd = new SqlCommand("Sp_ObtenerCategoriaPorId", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CategoriaID", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            categoria = new Categorias
                            {
                                CategoriaID = Convert.ToInt32(reader["CategoriaID"]),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            };
                        }
                    }
                }
            }

            return categoria;
        }

        // Insertar categoría
        public virtual void InsertarCategoria(Categorias categoria)
        {
            using (var conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("Sp_InsertarCategoria", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Eliminar categoría
        public virtual void EliminarCategoria(int categoriaId)
        {
            using (var conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("Sp_EliminarCategoria", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoriaID", categoriaId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
