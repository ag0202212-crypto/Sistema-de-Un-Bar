using System;
using System.Collections.Generic;
using System.Text;
using Application.DTos;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class FacturaService
    {
        private readonly IFactura _factura;

        public FacturaService(IFactura factura)
        {
            _factura = factura;
        }
        //listar
        public async Task<IEnumerable<FacturaDTos>> ListarFactura()
        {
            var l = await _factura.ListarFacturaAsync();
            return l.Select(m => new FacturaDTos
            {
                id_factura = m.id_factura,
                nombre_factura = m.nombre_factura,
                fecha_y_hora_de_cierre = m.fecha_y_hora_de_cierre,
                subtotal = m.subtotal,
                iva = m.iva,
                total = m.total,
                estado_de_la_factura = m.estado_de_la_factura,
                id_comanda = m.id_comanda,
                id_cliente = m.id_cliente
            });
        }
        //obtener por id
        public async Task<FacturaDTos?> ObtenerFactura(int id)
        {
            // 1. Buscamos en la base de datos usando tu interfaz
            var m = await _factura.ObtenerFactura(id);

            // 2. Si no existe, devolvemos nulo
            if (m == null) return null;

            // 3. Si existe, lo convertimos a DTO
            return new FacturaDTos
            {
                id_factura = m.id_factura,
                nombre_factura = m.nombre_factura,
                fecha_y_hora_de_cierre = m.fecha_y_hora_de_cierre,
                subtotal = m.subtotal,
                iva = m.iva,
                total = m.total,
                estado_de_la_factura = m.estado_de_la_factura,
                id_comanda = m.id_comanda,
                id_cliente = m.id_cliente
            };
        }
        //buscar
        public async Task<IEnumerable<FacturaDTos>> BuscarFactura(string criterio)
        {
            var lista = await _factura.BuscarFacturaAsync(criterio);

            return lista.Select(m => new FacturaDTos
            {
                id_factura = m.id_factura,
                nombre_factura = m.nombre_factura,
                fecha_y_hora_de_cierre = m.fecha_y_hora_de_cierre,
                subtotal = m.subtotal,
                iva = m.iva,
                total = m.total,
                estado_de_la_factura = m.estado_de_la_factura,
                id_comanda = m.id_comanda,
                id_cliente = m.id_cliente
            });
        }

        //agregar
        public async Task NuevoFactura(FacturaDTos facturaDTos)
        {
            var fac = new Factura
            {
                nombre_factura = facturaDTos.nombre_factura,
                fecha_y_hora_de_cierre = facturaDTos.fecha_y_hora_de_cierre,
                subtotal = facturaDTos.subtotal,
                iva = facturaDTos.iva,
                total = facturaDTos.total,
                estado_de_la_factura = facturaDTos.estado_de_la_factura,
                id_comanda = facturaDTos.id_comanda,
                id_cliente = facturaDTos.id_cliente
            };
            await _factura.NuevoFactura(fac);

        }

        //editar

        public async Task EditarFactura(FacturaDTos facturaD)
        {
            var factura = new Factura
            {
                id_factura = facturaD.id_factura,
                nombre_factura = facturaD.nombre_factura,
                fecha_y_hora_de_cierre = facturaD.fecha_y_hora_de_cierre,
                subtotal = facturaD.subtotal,
                iva = facturaD.iva,
                total = facturaD.total,
                estado_de_la_factura = facturaD.estado_de_la_factura,
                id_comanda = facturaD.id_comanda,
                id_cliente = facturaD.id_cliente
            };

            await _factura.EditarFacturaAsync(factura);
        }

        //eliminar 

        public async Task EliminarFactura(int id)
        {
            await _factura.EliminarFacturaAsync(id);
        }
    }
}
