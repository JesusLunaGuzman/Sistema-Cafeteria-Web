using System;
using System.Collections.Generic;

namespace CafeteriaWeb.Models;

public partial class CategoriaProducto
{
    public int IdCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
