using Capa_Datos;
using Capa_Entidades;


namespace Capa_Negocio
{
    public class ProductoServicio
    {
        // Depende de la interfaz de la capa de datos para permitir el mockeo en pruebas unitarias
        private readonly Capa_Datos.IProductoDatos _productoDatos;

        public ProductoServicio(Capa_Datos.IProductoDatos productoDatos)
        {
            _productoDatos = productoDatos ?? throw new ArgumentNullException(nameof(productoDatos));
        }

        public IEnumerable<Producto> GetProductos()
        {
            return _productoDatos.GetProductos();
        }

        public Producto? GetProductoById(int id)
        {
            return _productoDatos.GetProductoById(id);
        }

        public void AddProducto(Producto producto)
        {
            if (producto == null) throw new ArgumentNullException(nameof(producto));
            _productoDatos.Insertar(producto);
        }

        public void UpdateProducto(Producto producto)
        {
            if (producto == null) throw new ArgumentNullException(nameof(producto));
            _productoDatos.Actualizar(producto);
        }

        public void DeleteProducto(int id)
        {
            _productoDatos.Eliminar(id);
        }
    }
}
