using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICliente
    {
        Task<IEnumerable<Cliente>> ListarClienteAsync();
        Task<Cliente?> ObtenerCliente(int id);
        Task NuevoCliente(Cliente cliente);
        Task EditarClienteAsync(Cliente cliente);
        Task EliminarClienteAsync(int id);
    }
}
