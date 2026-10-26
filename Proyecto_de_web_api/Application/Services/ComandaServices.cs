using System;
using System.Collections.Generic;
using System.Text;
using Application.DTos;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class ComandaServices
    {
        private readonly IComanda _comanda;

        public ComandaServices(IComanda comanda)
        {
            _comanda = comanda;
        }
        //listar
        public async Task<IEnumerable<ComandaDTos>> Listarcomanda()
        {
            var l = await _comanda.ListarComandaAsync();
            return l.Select(x => new ComandaDTos
            {
                id_comanda = x.id_comanda,
                fecha_y_hora_de_apertura = x.fecha_y_hora_de_apertura,
                estado = x.estado,
                id_mesa = x.id_mesa,
                id_usuario = x.id_usuario
            });
        }
        //obtener por id
        public async Task<ComandaDTos?> ObtenerComanda(int id)
        {
            // 1. Buscamos en la base de datos usando tu interfaz
            var x = await _comanda.ObtenerComanda(id);

            // 2. Si no existe, devolvemos nulo
            if (x == null) return null;

            // 3. Si existe, lo convertimos a DTO
            return new ComandaDTos
            {
                id_comanda = x.id_comanda,
                fecha_y_hora_de_apertura = x.fecha_y_hora_de_apertura,
                estado = x.estado,
                id_mesa = x.id_mesa,
                id_usuario = x.id_usuario
            };
        }
        //agregar
        public async Task NuevoComanda(ComandaDTos comandaDTos)
        {
            var coman = new Comanda
            {
                fecha_y_hora_de_apertura = comandaDTos.fecha_y_hora_de_apertura,
                estado = comandaDTos.estado,
                id_mesa = comandaDTos.id_mesa,
                id_usuario = comandaDTos.id_usuario
            };
            await _comanda.NuevoComanda(coman);

        }

        //editar

        public async Task EditarComanda(ComandaDTos comandaDTo)
        {
            var coman = new Comanda
            {
                id_comanda = comandaDTo.id_comanda,
                fecha_y_hora_de_apertura = comandaDTo.fecha_y_hora_de_apertura,
                estado = comandaDTo.estado,
                id_mesa = comandaDTo.id_mesa,
                id_usuario = comandaDTo.id_usuario
            };

            await _comanda.EditarComandaAsync(coman);
        }

        //eliminar 

        public async Task EliminarComanda(int id)
        {
            await _comanda.EliminarComandaAsync(id);
        }
    }
}
