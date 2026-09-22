using System;
using System.Collections.Generic;

namespace CafeteriaWeb.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Email { get; set; }

    public string? Telefono { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
