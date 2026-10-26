using System;
using System.Collections.Generic;
using System.Text;
using Application.DTos;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class Detalle_Asiento_ContableServices
    {
        private readonly IDetalle_Asiento_Contable _detalle_asiento_contable;

        public Detalle_Asiento_ContableServices(IDetalle_Asiento_Contable detalle_Asiento_Contable)
        {
            _detalle_asiento_contable = detalle_Asiento_Contable;
        }

        //listar
        public async Task<IEnumerable<Detalle_Asiento_ContableDTos>> ListarDetalle_Asiento_Contable()
        {
            var m = await _detalle_asiento_contable.ListarDetalle_Asiento_ContableAsync();
            return m.Select(p => new Detalle_Asiento_ContableDTos
            {
                id_detalle_asiento_contable = p.id_detalle_asiento_contable,
                monto_debe = p.monto_debe,
                monto_haber = p.monto_haber,
                id_asiento_contable = p.id_asiento_contable,
                id_cuenta_contable = p.id_cuenta_contable
            });
        }

        // 1. OBTENER UN SOLO REGISTRO DE DETALLE POR SU ID
        public async Task<Detalle_Asiento_ContableDTos?> ObtenerDetalle_Asiento_Contable(int id)
        {
            var p = await _detalle_asiento_contable.ObtenerPorIdAsync(id);

            if (p == null) return null;

            return new Detalle_Asiento_ContableDTos
            {
                id_detalle_asiento_contable = p.id_detalle_asiento_contable,
                id_asiento_contable = p.id_asiento_contable,
                id_cuenta_contable = p.id_cuenta_contable,
                monto_debe = p.monto_debe,
                monto_haber = p.monto_haber
            };
        }

        // 2. OBTENER TODOS LOS DETALLES DE UN MISMO ASIENTO CONTABLE (Devuelve Lista)
        public async Task<IEnumerable<Detalle_Asiento_ContableDTos>> ObtenerDetallePorAsiento(int idAsiento)
        {
            var lista = await _detalle_asiento_contable.ObtenerPorAsientoIdAsync(idAsiento);

            return lista.Select(p => new Detalle_Asiento_ContableDTos
            {
                id_detalle_asiento_contable = p.id_detalle_asiento_contable,
                id_asiento_contable = p.id_asiento_contable,
                id_cuenta_contable = p.id_cuenta_contable,
                monto_debe = p.monto_debe,
                monto_haber = p.monto_haber
            });
        }

        //agregar
        public async Task NuevoDetalle_Asiento_Contable(Detalle_Asiento_ContableDTos detalle_Asiento_Contable)
        {
            var detallecontable = new Detalle_Asiento_Contable
            {
                monto_debe = detalle_Asiento_Contable.monto_debe,
                monto_haber = detalle_Asiento_Contable.monto_haber,
                id_asiento_contable = detalle_Asiento_Contable.id_asiento_contable,
                id_cuenta_contable = detalle_Asiento_Contable.id_cuenta_contable

            };
            await _detalle_asiento_contable.NuevoDetalle_Asiento_Contable(detallecontable);

        }

        //editar

        public async Task EditarDetalle_Asiento_Contable(Detalle_Asiento_ContableDTos detalle_Asiento_ContableD)
        {
            var detalleasientocontable = new Detalle_Asiento_Contable
            {
                id_detalle_asiento_contable = detalle_Asiento_ContableD.id_detalle_asiento_contable,
                monto_debe = detalle_Asiento_ContableD.monto_debe,
                monto_haber = detalle_Asiento_ContableD.monto_haber,
                id_asiento_contable = detalle_Asiento_ContableD.id_asiento_contable,
                id_cuenta_contable = detalle_Asiento_ContableD.id_cuenta_contable
            };

            await _detalle_asiento_contable.EditarDetalle_Asiento_ContableAsync(detalleasientocontable);
        }

        //eliminar 

        public async Task EliminarDetalle_Asiento_Contable(int id)
        {
            await _detalle_asiento_contable.EliminarDetalle_Asiento_ContableAsync(id);
        }
    }
}
