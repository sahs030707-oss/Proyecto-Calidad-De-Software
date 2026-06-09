using System.Collections.Generic;
using Capa_Entidades;

namespace Capa_Datos
{
    // Interfaz añadida para permitir el mockeo de CategoriaDatos en pruebas unitarias.
    public interface ICategoriaDatos
    {
        List<Categorias> ObtenerCategorias();
        Categorias? ObtenerCategoriaPorId(int id);
        void InsertarCategoria(Categorias categoria);
        void EliminarCategoria(int categoriaId);
    }
}
