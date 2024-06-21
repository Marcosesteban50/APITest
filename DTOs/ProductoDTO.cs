using APIPruebas.Models;

namespace APIPruebas.DTOs
{
    public class ProductoDTO
    {

        public int IdProducto { get; set; }
        public string? CodigoBarra { get; set; }
        public string? Descripcion { get; set; }
        public string? Marca { get; set; }
        public int? IdCategoria { get; set; }
        public decimal? Precio { get; set; }

        public virtual Categoria? oCategoria { get; set; }
    }
}
