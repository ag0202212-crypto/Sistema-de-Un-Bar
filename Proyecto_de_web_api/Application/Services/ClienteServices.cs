using System;
using System.Collections.Generic;
using System.Text;
using Application.DTos;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class ClienteServices
    {
        private readonly ICliente _cliente;

        public ClienteServices(ICliente cliente)
        {
            _cliente = cliente;
        }

        //listar
        public async Task<IEnumerable<ClienteDTos>> Listarcliente()
        {
            var cli = await _cliente.ListarClienteAsync();
            return cli.Select(li => new ClienteDTos
            {
                id_cliente = li.id_cliente,
                nombre = li.nombre,
                cedula = li.cedula,
                telefono = li.telefono,
                activo = li.activo,
                id_direccion = li.id_direccion,

                // 📍 Campos de Dirección agregados
                departamento = li.departamento,
                municipio = li.municipio,
                detalle_direccion = li.detalle_direccion
            });
        }

        // 2. OBTENER POR ID
        public async Task<ClienteDTos?> ObtenerCliente(int id)
        {
            var li = await _cliente.ObtenerCliente(id);

            if (li == null) return null;

            return new ClienteDTos
            {
                id_cliente = li.id_cliente,
                nombre = li.nombre,
                cedula = li.cedula,
                telefono = li.telefono,
                activo = li.activo,
                id_direccion = li.id_direccion,

                // 📍 Campos de Dirección agregados
                departamento = li.departamento,
                municipio = li.municipio,
                detalle_direccion = li.detalle_direccion
            };
        }

        // 3. NUEVO CLIENTE
        public async Task NuevoCliente(ClienteDTos cliente)
        {
            var cliente1 = new Cliente
            {
                nombre = cliente.nombre,
                cedula = cliente.cedula,
                telefono = cliente.telefono,
                id_direccion = cliente.id_direccion,

                // 📍 Campos de Dirección agregados
                departamento = cliente.departamento,
                municipio = cliente.municipio,
                detalle_direccion = cliente.detalle_direccion
            };

            await _cliente.NuevoCliente(cliente1);
        }

        // 4. EDITAR CLIENTE
        public async Task EditarCliente(ClienteDTos clienteD)
        {
            var ocliente = new Cliente
            {
                id_cliente = clienteD.id_cliente,
                nombre = clienteD.nombre,
                cedula = clienteD.cedula,
                telefono = clienteD.telefono,
                activo = clienteD.activo,
                id_direccion = clienteD.id_direccion,

                // 📍 Campos de Dirección agregados
                departamento = clienteD.departamento,
                municipio = clienteD.municipio,
                detalle_direccion = clienteD.detalle_direccion
            };

            await _cliente.EditarClienteAsync(ocliente);
        }

        // 5. ELIMINAR CLIENTE
        public async Task EliminarCliente(int id)
        {
            await _cliente.EliminarClienteAsync(id);
        }
    }
}
