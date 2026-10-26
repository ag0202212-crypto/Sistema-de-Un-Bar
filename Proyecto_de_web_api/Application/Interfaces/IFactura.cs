using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IFactura
    {
        Task<IEnumerable<Factura>> ListarFacturaAsync();

        Task<Factura?> ObtenerFactura(int id);
        Task<IEnumerable<Factura>> BuscarFacturaAsync(string criterio);
        Task NuevoFactura(Factura factura);
        Task EditarFacturaAsync(Factura factura);
        Task EliminarFacturaAsync(int id);
    }
}
