using System.Collections.Generic;
using Capa_Entidades;

namespace Capa_Datos
{
    // Interfaz añadida para permitir el mockeo de InventarioDatos en pruebas unitarias.
    public interface IInventarioDatos
    {
        IEnumerable<Inventario> ObtenerInventario();
        void AumentarStock(int productoId, int cantidad);
    }
}
