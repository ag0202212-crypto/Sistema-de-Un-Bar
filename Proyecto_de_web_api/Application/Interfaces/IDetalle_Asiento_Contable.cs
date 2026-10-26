using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IDetalle_Asiento_Contable
    {
        Task<IEnumerable<Detalle_Asiento_Contable>> ListarDetalle_Asiento_ContableAsync();
        Task<Detalle_Asiento_Contable?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Detalle_Asiento_Contable>> ObtenerPorAsientoIdAsync(int idAsiento);
        Task NuevoDetalle_Asiento_Contable(Detalle_Asiento_Contable detalle_Asiento_Contable);
        Task EditarDetalle_Asiento_ContableAsync(Detalle_Asiento_Contable detalle_Asiento_Contable);
        Task EliminarDetalle_Asiento_ContableAsync(int id);
    }
}
