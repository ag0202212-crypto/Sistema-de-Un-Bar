using System;
using System.Collections.Generic;
using System.Text;
using Application.DTos;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class ZonaService
    {
        private readonly IZona _zonaService;

        public ZonaService(IZona zonaService)
        {
            _zonaService = zonaService;
        }

        public async Task<IEnumerable<ZonaDTos>> ListarZona()
        {
            var l = await _zonaService.ListarZonaAsync();
            return l.Select(m => new ZonaDTos
            {
                id_zona = m.id_zona,
                nombre_zona = m.nombre_zona,
                activo = m.activo
            });
        }
        //obtener por id
        public async Task<ZonaDTos?> ObtenerZona(int id)
        {
            // 1. Buscamos en la base de datos usando tu interfaz
            var p = await _zonaService.ObtenerZona(id);

            // 2. Si no existe, devolvemos nulo
            if (p == null) return null;

            // 3. Si existe, lo convertimos a DTO
            return new ZonaDTos
            {
                id_zona = p.id_zona,
                nombre_zona = p.nombre_zona,
                activo = p.activo
            };
        }

        //agregar
        public async Task NuevoZona(ZonaDTos zonaDTos)
        {
            var zona = new Zona
            {

                nombre_zona = zonaDTos.nombre_zona,
            };
            await _zonaService.NuevoZona(zona);

        }

        //editar

        public async Task EditarZona(ZonaDTos zonaD)
        {
            var zo = new Zona
            {
                id_zona = zonaD.id_zona,
                nombre_zona = zonaD.nombre_zona,
                activo = zonaD.activo
            };

            await _zonaService.EditarZonaAsync(zo);
        }

        //eliminar 

        public async Task EliminarZona(int id)
        {
            await _zonaService.EliminarZonaAsync(id);
        }
    }
}
