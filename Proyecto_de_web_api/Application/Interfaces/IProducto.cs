using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProducto
    {
        Task<IEnumerable<Producto>> ListarProductosAsync();
        Task<Producto?> ObtenerProducto(int id);
        Task NuevoProductos(Producto productos);
        Task EditarProductosAsync(Producto productos);
        Task EliminarProductosAsync(int id);
    }
}
