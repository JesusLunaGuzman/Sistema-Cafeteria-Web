using System;
using System.Collections.Generic;

namespace CafeteriaWeb.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public DateTime Fecha { get; set; }

    public decimal Total { get; set; }

    public string Estado { get; set; } = null!;

    public int IdUsuario { get; set; }

    public int? IdCliente { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
