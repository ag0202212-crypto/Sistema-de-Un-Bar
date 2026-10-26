using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IZona
    {
        Task<IEnumerable<Zona>> ListarZonaAsync();
        Task<Zona?> ObtenerZona(int id);
        Task NuevoZona(Zona zona);
        Task EditarZonaAsync(Zona zona);
        Task EliminarZonaAsync(int id);
    }
}
