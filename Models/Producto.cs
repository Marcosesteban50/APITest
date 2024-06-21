using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIPruebas.Models
{
    public partial class Producto
    {
        public int IdProducto { get; set; }
        [Required]
        public string? CodigoBarra { get; set; }
        public string? Descripcion { get; set; }
        public string? Marca { get; set; }
        [Required]

        public int? IdCategoria { get; set; }

        public decimal? Precio { get; set; }

        public virtual Categoria? oCategoria { get; set; }
    }
}
