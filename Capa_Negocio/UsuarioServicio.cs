using System.Security.Cryptography;
using System.Text;
using Capa_Datos;
using Capa_Entidades;
using Konscious.Security.Cryptography;


namespace Capa_Negocio
{
    public class UsuarioServicio
    {

        // Depende de IUsuarioDatos para permitir el mockeo en pruebas unitarias
        private readonly Capa_Datos.IUsuarioDatos _usuarioDatos;

        public UsuarioServicio(Capa_Datos.IUsuarioDatos usuarioDatos)
        {
            _usuarioDatos = usuarioDatos ?? throw new ArgumentNullException(nameof(usuarioDatos));
        }

        // Obtener todos los usuarios
        public IEnumerable<Usuario> GetUsuarios()
        {
            return _usuarioDatos.ObtenerUsuarios();
        }

        // Obtener usuario por ID
        public Usuario? GetUsuarioById(int id)
        {
            return _usuarioDatos.ObtenerUsuarioPorId(id);
        }

        // Agregar usuario
        public void AddUsuario(Usuario user, string Contraseña)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(Contraseña)) throw new ArgumentException("Clave no puede ser vacía");

            // Hashear la contraseña
            byte[] salt = GenerarSalt();
            user.Contraseña = HashearClave(Contraseña, salt);

            _usuarioDatos.Insertar(user);
        }

        // Actualizar usuario
        public void UpdateUsuario(Usuario user, string? Contraseña = null)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            if (!string.IsNullOrEmpty(Contraseña))
            {
                byte[] salt = GenerarSalt();
                user.Contraseña = HashearClave(Contraseña, salt);
            }

            _usuarioDatos.Actualizar(user);
        }

        // Eliminar usuario
        public void DeleteUsuario(int id)
        {
            _usuarioDatos.Eliminar(id);
        }

        // Métodos de seguridad
        public byte[] GenerarSalt(int tamaño = 16)
        {
            byte[] salt = new byte[tamaño];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }

        public string HashearClave(string clave, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(clave))
            {
                Salt = salt,
                DegreeOfParallelism = 2,
                Iterations = 4,
                MemorySize = 1024
            };
            var hashBytes = argon2.GetBytes(16);
            return Convert.ToBase64String(salt) + "." + Convert.ToBase64String(hashBytes);
        }
    }
}
