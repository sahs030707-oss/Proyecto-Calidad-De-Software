using Capa_Datos;
using Capa_Entidades;

namespace Capa_Negocio
{
    public class InventarioServicio
    {
        // Depende de IInventarioDatos para permitir el mockeo en pruebas unitarias
        private readonly Capa_Datos.IInventarioDatos _inventarioDatos;

        public InventarioServicio(Capa_Datos.IInventarioDatos inventarioDatos)
        {
            _inventarioDatos = inventarioDatos;
        }

        public IEnumerable<Inventario> GetInventario()
        {
            return _inventarioDatos.ObtenerInventario();
        }

        public void AddStock(int productoId, int cantidad)
        {
            _inventarioDatos.AumentarStock(productoId, cantidad);
        }
    }
}

