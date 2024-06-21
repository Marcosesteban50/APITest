using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIPruebas.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdCategoria { get; set; }
        [Required]

        public string? Descripcion { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
