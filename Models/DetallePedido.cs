using System;
using System.Collections.Generic;

namespace CafeteriaWeb.Models;

public partial class DetallePedido
{
    public int IdDetalle { get; set; }

    public int IdPedido { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public decimal SubTotal { get; set; }

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
