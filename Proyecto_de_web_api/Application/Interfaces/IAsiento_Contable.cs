using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAsiento_Contable
    {
        Task<IEnumerable<Asiento_Contable>> ListarAsiento_ContableAsync();
        Task<Asiento_Contable?> ObtenerAsiento_Contable(int id);
        Task NuevoAsiento_Contable(Asiento_Contable asiento_Contable);
        Task EditarAsiento_ContableAsync(Asiento_Contable asiento_Contable);
        Task EliminarAsiento_ContableAsync(int id);
    }
}
