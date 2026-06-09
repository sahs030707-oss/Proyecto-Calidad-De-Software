using Capa_Datos;
using Capa_Entidades;

namespace Capa_Negocio
{
    public class VentaServicio
    {

        // Depende de IVentaDatos para que las pruebas puedan mockear la capa de datos
        private readonly Capa_Datos.IVentaDatos _ventaDatos;

        public VentaServicio(Capa_Datos.IVentaDatos ventaDatos)
        {
            _ventaDatos = ventaDatos;
        }

        public void RegistrarVenta(Ventas venta, List<DetalleVenta> detalles)
        {
            if (detalles == null || !detalles.Any())
                throw new Exception("La venta debe contener al menos un producto.");

            foreach (var det in detalles)
                det.Subtotal = det.PrecioUnitario * det.Cantidad;

            venta.Total = detalles.Sum(d => d.Subtotal);
            venta.FechaVenta = DateTime.Now;

            _ventaDatos.RegistrarVenta(venta, detalles);
        }

        public List<Ventas> ObtenerVentas() => _ventaDatos.ObtenerVentas();

        public Ventas? ObtenerVentaPorId(int id) => _ventaDatos.ObtenerVentaPorId(id);
    }
}
