using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITipo_de_Cuenta
    {
        Task<IEnumerable<Tipo_de_Cuenta>> ListarTipo_de_CuentaAsync();
        Task<Tipo_de_Cuenta?> ObtenerTipo_de_Cuenta(int id);
        Task NuevoTipo_de_Cuenta(Tipo_de_Cuenta tipo_De_Cuenta);
        Task EditarTipo_de_CuentaAsync(Tipo_de_Cuenta tipo_De_Cuenta);
        Task EliminarTipo_de_CuentaAsync(int id);
    }
}
