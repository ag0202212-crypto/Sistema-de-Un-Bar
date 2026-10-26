using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMesa
    {
        Task<IEnumerable<Mesa>> ListarMesaAsync();
        Task<Mesa?> ObtenerMesa(int id);
        Task NuevoMesa(Mesa mesa);
        Task EditarMesaAsync(Mesa mesa);
        Task EliminarMesaAsync(int id);
    }
}
