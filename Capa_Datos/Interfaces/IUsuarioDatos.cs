using System.Collections.Generic;
using Capa_Entidades;

namespace Capa_Datos
{
    // Interfaz añadida para permitir el mockeo de UsuarioDatos en pruebas unitarias.
    public interface IUsuarioDatos
    {
        IEnumerable<Usuario> ObtenerUsuarios();
        Usuario? ObtenerUsuarioPorId(int id);
        void Insertar(Usuario usuario);
        void Actualizar(Usuario usuario);
        void Eliminar(int id);
    }
}
