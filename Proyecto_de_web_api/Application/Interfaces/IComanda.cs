using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IComanda
    {
        Task<IEnumerable<Comanda>> ListarComandaAsync();
        Task<Comanda?> ObtenerComanda(int id);
        Task NuevoComanda(Comanda comanda);
        Task EditarComandaAsync(Comanda comanda);
        Task EliminarComandaAsync(int id);
    }
}
