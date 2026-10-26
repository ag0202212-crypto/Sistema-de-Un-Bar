using System;
using System.Collections.Generic;
using System.Text;
using Application.DTos;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class DireccionService
    {
        private readonly IDireccion _direccion;
        public DireccionService(IDireccion direccion)
        {
            _direccion = direccion;
        }

        //listar
        public async Task<IEnumerable<DireccionDTos>> ListarDireccion()
        {
            var l = await _direccion.ListarDireccionAsync();
            return l.Select(p => new DireccionDTos
            {
                id_direccion = p.id_direccion,
                departamento = p.departamento,
                Municipio = p.Municipio,
                detalle_de_direccion = p.detalle_de_direccion,
                activo = p.activo
            });
        }
        //obtener por id
        public async Task<DireccionDTos?> ObtenerDireccion(int id)
        {
            // 1. Buscamos en la base de datos usando tu interfaz
            var p = await _direccion.ObtenerDireccion(id);

            // 2. Si no existe, devolvemos nulo
            if (p == null) return null;

            // 3. Si existe, lo convertimos a DTO
            return new DireccionDTos
            {
                id_direccion = p.id_direccion,
                departamento = p.departamento,
                Municipio = p.Municipio,
                detalle_de_direccion = p.detalle_de_direccion,
                activo = p.activo
            };
        }

        //agregar
        public async Task NuevoDireccion(DireccionDTos direccionDTos)
        {
            var direccion = new Direccion
            {
                departamento = direccionDTos.departamento,
                Municipio = direccionDTos.Municipio,
                detalle_de_direccion = direccionDTos.detalle_de_direccion,
            };
            await _direccion.NuevoDireccion(direccion);

        }

        //editar

        public async Task EditarDireccion(DireccionDTos direccionD)
        {
            var direcc = new Direccion
            {
                id_direccion = direccionD.id_direccion,
                departamento = direccionD.departamento,
                Municipio = direccionD.Municipio,
                detalle_de_direccion = direccionD.detalle_de_direccion,
                activo = direccionD.activo
            };

            await _direccion.EditarDireccionAsync(direcc);
        }

        //eliminar 

        public async Task EliminarDireccion(int id)
        {
            await _direccion.EliminarDireccionAsync(id);
        }
    }
}
