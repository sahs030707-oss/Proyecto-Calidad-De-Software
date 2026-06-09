using Capa_Datos;
using Capa_Entidades;

namespace Capa_Negocio
{
    public class CategoriaServicio
    {
        // Depende de ICategoriaDatos para permitir el mockeo en pruebas unitarias
        private readonly Capa_Datos.ICategoriaDatos _categoriaDatos;

        public CategoriaServicio(Capa_Datos.ICategoriaDatos categoriaDatos)
        {
            _categoriaDatos = categoriaDatos;
        }
        public List<Categorias> ObtenerCategorias() => _categoriaDatos.ObtenerCategorias();

        public Categorias? ObtenerCategoriaPorId(int id) => _categoriaDatos.ObtenerCategoriaPorId(id);

        public void InsertarCategoria(Categorias categoria) => _categoriaDatos.InsertarCategoria(categoria);

        public void EliminarCategoria(int categoriaId) => _categoriaDatos.EliminarCategoria(categoriaId);
    }
}