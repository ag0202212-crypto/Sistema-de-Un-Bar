using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IDireccion
    {
        Task<IEnumerable<Direccion>> ListarDireccionAsync();
        Task<Direccion?> ObtenerDireccion(int id);
        Task NuevoDireccion(Direccion direccion);
        Task EditarDireccionAsync(Direccion direccion);
        Task EliminarDireccionAsync(int id);
    }
}
