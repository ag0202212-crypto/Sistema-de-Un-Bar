using System;
using System.Collections.Generic;
using System.Text;
using Application.DTos;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class Detalle_ComandaService
    {
        private readonly IDetalle_Comanda _detalle_comanda;
        
        public Detalle_ComandaService(IDetalle_Comanda detalle_Comanda)
        {
            _detalle_comanda = detalle_Comanda;
        }
        //listar
        public async Task<IEnumerable<Detalle_ComandaDTos>> ListarDetalle_Comanda()
        {
            var li = await _detalle_comanda.ListarDetalle_ComandaAsync();
            return li.Select(m => new Detalle_ComandaDTos
            {
                id_detalle_comanda = m.id_detalle_comanda,
                cantidad_pedida = m.cantidad_pedida,
                precio_de_venta = m.precio_de_venta,
                nota_especial = m.nota_especial,
                id_comanda = m.id_comanda,
                id_producto = m.id_producto
            });
        }


        // 1. OBTENER UN SOLO DETALLE POR SU ID (Renglon individual)
        public async Task<Detalle_ComandaDTos?> ObtenerDetalle_Comanda(int id)
        {
            var p = await _detalle_comanda.ObtenerPorIdAsync(id);

            if (p == null) return null;

            return new Detalle_ComandaDTos
            {
                id_detalle_comanda = p.id_detalle_comanda,
                id_comanda = p.id_comanda,
                id_producto = p.id_producto,
                cantidad_pedida = p.cantidad_pedida,
                precio_de_venta = p.precio_de_venta,
                nota_especial = p.nota_especial
            };
        }

        // 2. OBTENER TODOS LOS DETALLES DE UNA MISMAS COMANDA (Devuelve Lista)
        public async Task<IEnumerable<Detalle_ComandaDTos>> ObtenerDetallePorComanda(int idComanda)
        {
            var lista = await _detalle_comanda.ObtenerPorComandaIdAsync(idComanda);

            return lista.Select(p => new Detalle_ComandaDTos
            {
                id_detalle_comanda = p.id_detalle_comanda,
                id_comanda = p.id_comanda,
                id_producto = p.id_producto,
                cantidad_pedida = p.cantidad_pedida,
                precio_de_venta = p.precio_de_venta,
                nota_especial = p.nota_especial
            });
        }

        //agregar
        public async Task NuevoDetalle_Comanda(Detalle_ComandaDTos detalle_ComandaDTos)
        {
            var detallecomanda = new Detalle_Comanda
            {
                cantidad_pedida = detalle_ComandaDTos.cantidad_pedida,
                precio_de_venta = detalle_ComandaDTos.precio_de_venta,
                nota_especial = detalle_ComandaDTos.nota_especial,
                id_comanda = detalle_ComandaDTos.id_comanda,
                id_producto = detalle_ComandaDTos.id_producto

            };
            await _detalle_comanda.NuevoDetalle_Comanda(detallecomanda);

        }

        //editar

        public async Task EditarDetalle_Comanda(Detalle_ComandaDTos detalle_Comanda)
        {
            var deta_comam = new Detalle_Comanda
            {
                id_detalle_comanda = detalle_Comanda.id_detalle_comanda,
                cantidad_pedida = detalle_Comanda.cantidad_pedida,
                precio_de_venta = detalle_Comanda.precio_de_venta,
                nota_especial = detalle_Comanda.nota_especial,
                id_comanda = detalle_Comanda.id_comanda,
                id_producto = detalle_Comanda.id_producto
            };

            await _detalle_comanda.EditarDetalle_ComandaAsync(deta_comam);
        }

        //eliminar 

        public async Task EliminarDetalle_Comanda(int id)
        {
            await _detalle_comanda.EliminarDetalle_ComandaAsync(id);
        }
    }
}
