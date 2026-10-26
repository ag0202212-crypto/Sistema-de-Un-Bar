using System;
using System.Collections.Generic;
using System.Text;
using Application.DTos;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class Asiento_ContableService
    {
        private readonly IAsiento_Contable _contable;

        public Asiento_ContableService(IAsiento_Contable contable) 
        { 
         _contable = contable; 
        }

        //listar
        public async Task<IEnumerable<Asiento_ContableDTos>> ListarAsiento_Contable()
        {
            var li = await _contable.ListarAsiento_ContableAsync();
            return li.Select(p => new Asiento_ContableDTos
            {
                id_asiento_contable = p.id_asiento_contable,
                fecha_asiento = p.fecha_asiento,
                glosa = p.glosa,
                id_factura = p.id_factura
            });
        }

        //obtener por id
        public async Task<Asiento_ContableDTos?> ObtenerAsiento_Contable(int id)
        {
            // 1. Buscamos en la base de datos usando tu interfaz
            var p = await _contable.ObtenerAsiento_Contable(id);

            // 2. Si no existe, devolvemos nulo
            if (p == null) return null;

            // 3. Si existe, lo convertimos a DTO
            return new Asiento_ContableDTos
            {
                id_asiento_contable = p.id_asiento_contable,
                fecha_asiento = p.fecha_asiento,
                glosa = p.glosa,
                id_factura = p.id_factura
            };
        }

        //agregar
        public async Task NuevoAsiento_Contable(Asiento_ContableDTos ds)
        {
            var asiento_Contable = new Asiento_Contable
            {
                fecha_asiento = ds.fecha_asiento,
                glosa = ds.glosa,
                id_factura = ds.id_factura,

            };
            await _contable.NuevoAsiento_Contable(asiento_Contable);

        }

        //editar

        public async Task EditarAsiento_Contable(Asiento_ContableDTos tos)
        {
            var oasiento = new Asiento_Contable
            {
                id_asiento_contable = tos.id_asiento_contable,
                fecha_asiento = tos.fecha_asiento,
                glosa = tos.glosa,
                id_factura = tos.id_factura
            };

            await _contable.EditarAsiento_ContableAsync(oasiento);
        }

        //eliminar 

        public async Task EliminarAsiento_Contable(int id)
        {
            await _contable.EliminarAsiento_ContableAsync(id);
        }
    }
}
