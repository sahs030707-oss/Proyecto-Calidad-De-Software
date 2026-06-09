using System.Collections.Generic;
using Capa_Entidades;

namespace Capa_Datos
{
    // Interfaz añadida para permitir el mockeo de VentaDatos en pruebas unitarias.
    public interface IVentaDatos
    {
        void RegistrarVenta(Ventas venta, List<DetalleVenta> detalles);
        List<Ventas> ObtenerVentas();
        Ventas? ObtenerVentaPorId(int id);
    }
}
