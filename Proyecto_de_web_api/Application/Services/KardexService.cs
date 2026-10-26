using System;
using System.Collections.Generic;
using System.Text;
using Application.DTos;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class KardexService
    {
        private readonly IKardex _kardex;

        public KardexService(IKardex kardex)
        {
            _kardex = kardex;
        }
        //listar
        public async Task<IEnumerable<KardexDTos>> ListarKardex()
        {
            var m = await _kardex.ListarKardexAsync();
            return m.Select(l => new KardexDTos
            {
                id_kardex = l.id_kardex,
                tipo_de_movimiento = l.tipo_de_movimiento,
                cantidad = l.cantidad,
                fecha = l.fecha,
                id_producto = l.id_producto,
            });
        }
        //obtener por id
        public async Task<KardexDTos?> ObtenerKardex(int id)
        {
            // 1. Buscamos en la base de datos usando tu interfaz
            var l = await _kardex.ObtenerKardex(id);

            // 2. Si no existe, devolvemos nulo
            if (l == null) return null;

            // 3. Si existe, lo convertimos a DTO
            return new KardexDTos
            {
                id_kardex = l.id_kardex,
                tipo_de_movimiento = l.tipo_de_movimiento,
                cantidad = l.cantidad,
                fecha = l.fecha,
                id_producto = l.id_producto,
            };
        }

        //agregar
        public async Task NuevoKardex(KardexDTos kardexDTos)
        {
            var karde = new Kardex
            {
                tipo_de_movimiento = kardexDTos.tipo_de_movimiento,
                cantidad = kardexDTos.cantidad,
                fecha = kardexDTos.fecha,
                id_producto = kardexDTos.id_producto
            };
            await _kardex.NuevoKardex(karde);

        }

        //editar

        public async Task EditarKardex(KardexDTos kardexD)
        {
            var kard = new Kardex
            {
                id_kardex = kardexD.id_kardex,
                tipo_de_movimiento = kardexD.tipo_de_movimiento,
                cantidad = kardexD.cantidad,
                fecha = kardexD.fecha,
                id_producto = kardexD.id_producto
            };

            await _kardex.EditarKardexAsync(kard);
        }

        //eliminar 

        public async Task EliminarKardex(int id)
        {
            await _kardex.EliminarKardexAsync(id);
        }
    }
}
