using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IDetalle_Comanda
    {
        Task<IEnumerable<Detalle_Comanda>> ListarDetalle_ComandaAsync();
        
        Task<Detalle_Comanda?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Detalle_Comanda>> ObtenerPorComandaIdAsync(int idComanda);
        Task NuevoDetalle_Comanda(Detalle_Comanda detalle_Comanda);
        Task EditarDetalle_ComandaAsync(Detalle_Comanda detalle_Comanda);
        Task EliminarDetalle_ComandaAsync(int id);
    }
}
