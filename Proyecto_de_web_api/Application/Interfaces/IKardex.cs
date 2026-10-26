using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IKardex
    {
        Task<IEnumerable<Kardex>> ListarKardexAsync();
        Task<Kardex?> ObtenerKardex(int id);
        Task NuevoKardex(Kardex kardex);
        Task EditarKardexAsync(Kardex kardex);
        Task EliminarKardexAsync(int id);
    }
}
