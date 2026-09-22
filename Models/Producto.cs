using System;
using System.Collections.Generic;

namespace CafeteriaWeb.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public bool Estado { get; set; }

    public int IdCategoria { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual CategoriaProducto IdCategoriaNavigation { get; set; } = null!;
}
