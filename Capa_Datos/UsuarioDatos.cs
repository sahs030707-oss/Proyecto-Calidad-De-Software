using Capa_Entidades;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;


namespace Capa_Datos
{
    // Implementa IUsuarioDatos para permitir inyección y mocking en pruebas unitarias
    public class UsuarioDatos : IUsuarioDatos
    {
        
        private readonly string _cadenaConexion;

        public UsuarioDatos(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("DefaultConnection");
        }

        // Obtener todos los usuarios
        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            var usuarios = new List<Usuario>();
            using SqlConnection con = new(_cadenaConexion);
            con.Open();
            using SqlCommand cmd = new("Sp_ObtenerUsuario", con) { CommandType = CommandType.StoredProcedure };
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                usuarios.Add(new Usuario
                {
                    UsuarioID = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Correo = reader.GetString(2),
                    RolID = reader.GetInt32(3),
                    Rol = new Roles
                    {
                        NombreRol = reader.GetString(4)
                    }

                });
            }
            return usuarios;
        }

        // Obtener usuario por Id
        public Usuario? ObtenerUsuarioPorId(int id)
        {
            Usuario? usuario = null;
            using SqlConnection con = new(_cadenaConexion);
            con.Open();
            using SqlCommand cmd = new("Sp_ObtenerUsuarioPorId", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@UsuarioID", id);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                usuario = new Usuario
                {
                    UsuarioID = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Correo = reader.GetString(2),
                    RolID = reader.GetInt32(3),
                    Rol = new Roles
                    {
                        NombreRol = reader.GetString(4)
                    }
                };
            }
            return usuario;
        }

        // Insertar usuario
        public void Insertar(Usuario usuario)
        {
            using SqlConnection con = new(_cadenaConexion);
            con.Open();
            using SqlCommand cmd = new("Sp_InsertarUsuario", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
            cmd.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
            cmd.Parameters.AddWithValue("@RolID", usuario.RolID);
            cmd.ExecuteNonQuery();
        }

        // Actualizar usuario
        public void Actualizar(Usuario usuario)
        {
            using SqlConnection con = new(_cadenaConexion);
            con.Open();
            using SqlCommand cmd = new("Sp_ActualizarUsuario", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@UsuarioID", usuario.UsuarioID);
            cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
            cmd.Parameters.AddWithValue("@Contraseña", usuario.Contraseña); 
            cmd.Parameters.AddWithValue("@RolID", usuario.RolID);
            cmd.ExecuteNonQuery();
        }

        // Eliminar usuario
        public void Eliminar(int id)
        {
            using SqlConnection con = new(_cadenaConexion);
            con.Open();
            using SqlCommand cmd = new("Sp_EliminarUsuario", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@UsuarioID", id); 
            cmd.ExecuteNonQuery();
        }
    }
}
