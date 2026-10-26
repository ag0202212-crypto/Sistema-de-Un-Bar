using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICuenta_Contable
    {
        Task<IEnumerable<Cuenta_Contable>> ListarCuenta_ContableAsync();
        Task<Cuenta_Contable?> ObtenerCuenta_Contable(int id);
        Task NuevoCuenta_Contable(Cuenta_Contable cuenta_Contable);
        Task EditarCuenta_ContableAsync(Cuenta_Contable cuenta_Contable);
        Task EliminarCuenta_ContableAsync(int id);
    }
}
