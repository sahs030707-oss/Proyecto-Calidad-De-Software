using Capa_Datos;
using Capa_Entidades;

namespace Capa_Negocio
{
    public class MovimientosInventarioServicio
    {
        // Depende de IMovimientosInventarioDatos para permitir el mockeo en pruebas unitarias
        private readonly Capa_Datos.IMovimientosInventarioDatos _movimientosDatos;

        public MovimientosInventarioServicio(Capa_Datos.IMovimientosInventarioDatos movimientosDatos)
        {
            _movimientosDatos = movimientosDatos;
        }

        public void InsertarMovimiento(MovimientosInventario movimiento)
        {
            if (movimiento == null)
                throw new ArgumentNullException(nameof(movimiento));

            _movimientosDatos.InsertarMovimiento(movimiento);
        }
    }
}