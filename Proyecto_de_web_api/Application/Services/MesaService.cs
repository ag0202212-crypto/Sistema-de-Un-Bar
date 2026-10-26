using System;
using System.Collections.Generic;
using System.Text;
using Application.DTos;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class MesaService
    {
        private readonly IMesa _mesa;

        public MesaService(IMesa mesa)
        {
            _mesa = mesa;
        }
        //listar
        public async Task<IEnumerable<MesaDTos>> ListarMesa()
        {
            var l = await _mesa.ListarMesaAsync();
            return l.Select(n => new MesaDTos
            {
                id_mesa = n.id_mesa,
                numero_de_mesa = n.numero_de_mesa,
                capacidad_de_persona = n.capacidad_de_persona,
                estado = n.estado,
                activo = n.activo,
                id_zona = n.id_zona,
            });
        }
        //obtener por id
        public async Task<MesaDTos?> ObtenerMesa(int id)
        {
            // 1. Buscamos en la base de datos usando tu interfaz
            var n = await _mesa.ObtenerMesa(id);

            // 2. Si no existe, devolvemos nulo
            if (n == null) return null;

            // 3. Si existe, lo convertimos a DTO
            return new MesaDTos
            {
                id_mesa = n.id_mesa,
                numero_de_mesa = n.numero_de_mesa,
                capacidad_de_persona = n.capacidad_de_persona,
                estado = n.estado,
                activo = n.activo,
                id_zona = n.id_zona,
            };
        }
        //agregar
        public async Task NuevoMesa(MesaDTos mesaDTos)
        {
            var mesa = new Mesa
            {
                numero_de_mesa = mesaDTos.numero_de_mesa,
                capacidad_de_persona = mesaDTos.capacidad_de_persona,
                estado = mesaDTos.estado,
                id_zona = mesaDTos.id_zona

            };
            await _mesa.NuevoMesa(mesa);

        }

        //editar

        public async Task EditarMesa(MesaDTos mesaD)
        {
            var me = new Mesa
            {
                id_mesa = mesaD.id_mesa,
                numero_de_mesa = mesaD.numero_de_mesa,
                capacidad_de_persona = mesaD.capacidad_de_persona,
                estado = mesaD.estado,
                activo = mesaD.activo,
                id_zona = mesaD.id_zona
            };

            await _mesa.EditarMesaAsync(me);
        }

        //eliminar 

        public async Task EliminarMesa(int id)
        {
            await _mesa.EliminarMesaAsync(id);
        }
    }
}
