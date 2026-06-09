using System.Collections.Generic;
using Capa_Entidades;

namespace Capa_Datos
{
    // Interfaz añadida para permitir el mockeo de la capa de datos en pruebas unitarias.
    public interface IProductoDatos
    {
        IEnumerable<Producto> GetProductos();
        Producto? GetProductoById(int id);
        void Insertar(Producto producto);
        void Actualizar(Producto producto);
        void Eliminar(int id);
    }
}
