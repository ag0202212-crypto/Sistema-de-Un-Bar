using System;
using System.Collections.Generic;
using System.Text;
using Application.DTos;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class CategoriaServices
    {
        private readonly ICategoria _categoria;

        public CategoriaServices(ICategoria categoria)
        {
            _categoria = categoria;
        }

        //listar
        public async Task<IEnumerable<CategoriaDTos>> Listarcategoria()
        {
            var categorias = await _categoria.ListarCategoriaAsync();
            return categorias.Select(p => new CategoriaDTos
            {
                id_categoria = p.id_categoria,
                nombrecat = p.nombrecat,
                activo = p.activo
            });
        }

        //obtener por id
        public async Task<CategoriaDTos?> ObtenerCategoria(int id)
        {
            // 1. Buscamos en la base de datos usando tu interfaz
            var p = await _categoria.ObtenerCategoria(id);

            // 2. Si no existe, devolvemos nulo
            if (p == null) return null;

            // 3. Si existe, lo convertimos a DTO
            return new CategoriaDTos
            {
                id_categoria = p.id_categoria,
                nombrecat = p.nombrecat,
                activo = p.activo
            };
        }

        //agregar
        public async Task NuevoCategoria(CategoriaDTos ds)
        {
            var categoria = new Categoria
            {
                nombrecat = ds.nombrecat,

            };
            await _categoria.NuevoCategoria(categoria);

        }

        //editar

        public async Task EditarCategoria(CategoriaDTos Dtos)
        {
            var ocategoria = new Categoria
            {
                id_categoria = Dtos.id_categoria,
                nombrecat = Dtos.nombrecat,
                activo = Dtos.activo
            };

            await _categoria.EditarCategoriaAsync(ocategoria);
        }

        //eliminar 

        public async Task EliminarCategoria(int id)
        {
            await _categoria.EliminarCategoriaAsync(id);
        }
    }
}
