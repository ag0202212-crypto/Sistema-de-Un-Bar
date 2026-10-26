using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICategoria
    {
        Task<IEnumerable<Categoria>> ListarCategoriaAsync();
        Task<Categoria?> ObtenerCategoria(int id);
        Task NuevoCategoria(Categoria categoria);
        Task EditarCategoriaAsync(Categoria categoria);
        Task EliminarCategoriaAsync(int id);
    }
}
