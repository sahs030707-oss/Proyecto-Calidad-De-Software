
namespace Capa_Entidades
{
    public class Inventario
    {
        public int InventarioID { get; set; }

        public int ProductoID { get; set; }

        public string? NombreProducto { get; set; }

        public string? Categoria { get; set; }

        public decimal PrecioUnitario { get; set; }

        public bool Estado { get; set; }

        public int StockActual { get; set; }

        public DateTime Fecha { get; set; }
    }
}