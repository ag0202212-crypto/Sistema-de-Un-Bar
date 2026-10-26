using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuario
    {
        Task<IEnumerable<Usuario>> ListarUsuarioAsync();
        Task<Usuario?> ObtenerUsuario(int id);
        Task NuevoUsuario(Usuario usuario);
        Task EditarUsuarioAsync(Usuario usuario);
        Task EliminarUsuarioAsync(int id);
    }
}
