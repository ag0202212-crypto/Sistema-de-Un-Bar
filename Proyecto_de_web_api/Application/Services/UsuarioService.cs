using System;
using System.Collections.Generic;
using System.Text;
using Application.DTos;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuario _usuario;

        public UsuarioService(IUsuario usuario)
        {
            _usuario = usuario;
        }
        public async Task<IEnumerable<UsuarioDTos>> ListarUsuario()
        {
            var n = await _usuario.ListarUsuarioAsync();
            return n.Select(m => new UsuarioDTos
            {
                id_usuario = m.id_usuario,
                nombre = m.nombre,
                cedula = m.cedula,
                rol = m.rol,
                contraseña = m.contraseña,
                activo = m.activo
            });
        }
        //obtener por id
        public async Task<UsuarioDTos?> ObtenerUsuario(int id)
        {
            // 1. Buscamos en la base de datos usando tu interfaz
            var m = await _usuario.ObtenerUsuario(id);

            // 2. Si no existe, devolvemos nulo
            if (m == null) return null;

            // 3. Si existe, lo convertimos a DTO
            return new UsuarioDTos
            {
                id_usuario = m.id_usuario,
                nombre = m.nombre,
                cedula = m.cedula,
                rol = m.rol,
                contraseña = m.contraseña,
                activo = m.activo
            };
        }

        //agregar
        public async Task NuevoUsuario(UsuarioDTos usuarioDTos)
        {
            var usua = new Usuario
            {
                id_usuario = usuarioDTos.id_usuario,
                nombre = usuarioDTos.nombre,
                cedula = usuarioDTos.cedula,
                rol = usuarioDTos.rol,
                contraseña = usuarioDTos.contraseña,

            };
            await _usuario.NuevoUsuario(usua);

        }

        //editar

        public async Task EditarUsuario(UsuarioDTos usuarioD)
        {
            var us = new Usuario
            {
                id_usuario = usuarioD.id_usuario,
                nombre = usuarioD.nombre,
                cedula = usuarioD.cedula,
                rol = usuarioD.rol,
                contraseña = usuarioD.contraseña,
                activo = usuarioD.activo
            };

            await _usuario.EditarUsuarioAsync(us);
        }

        //eliminar 

        public async Task EliminarUsuario(int id)
        {
            await _usuario.EliminarUsuarioAsync(id);
        }
    }
}
