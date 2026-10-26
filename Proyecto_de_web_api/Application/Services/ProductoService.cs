using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Application.DTos;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class ProductoService
    {
        private readonly IProducto _producto;

        public ProductoService(IProducto producto)
        {
            _producto = producto;
        }

        //listar
        public async Task<IEnumerable<ProductoDTos>> ListarProducto()
        {
            var li = await _producto.ListarProductosAsync();
            return li.Select(p => new ProductoDTos
            {
                id_producto = p.id_producto,
                nombre = p.nombre,
                precio_venta = p.precio_venta,
                precio_de_costo = p.precio_de_costo,
                stock_actual = p.stock_actual,
                stock_minimo = p.stock_minimo,
                activo = p.activo,
                id_categoria = p.id_categoria
            });
        }
        //obtener por id
        public async Task<ProductoDTos?> ObtenerProducto(int id)
        {
            // 1. Buscamos en la base de datos usando tu interfaz
            var p = await _producto.ObtenerProducto(id);

            // 2. Si no existe, devolvemos nulo
            if (p == null) return null;

            // 3. Si existe, lo convertimos a DTO
            return new ProductoDTos
            {
                id_producto = p.id_producto,
                nombre = p.nombre,
                precio_venta = p.precio_venta,
                precio_de_costo = p.precio_de_costo,
                stock_actual = p.stock_actual,
                stock_minimo = p.stock_minimo,
                activo = p.activo,
                id_categoria = p.id_categoria
            };
        }
        //agregar
        public async Task NuevoProducto(ProductoDTos productoDTos)
        {
            var pro = new Producto
            {

                nombre = productoDTos.nombre,
                precio_venta = productoDTos.precio_venta,
                precio_de_costo = productoDTos.precio_de_costo,
                stock_actual = productoDTos.stock_actual,
                stock_minimo = productoDTos.stock_minimo,
                id_categoria = productoDTos.id_categoria
            };
            await _producto.NuevoProductos(pro);

        }

        //editar

        public async Task EditarProducto(ProductoDTos productoD)
        {
            var oprod = new Producto
            {
                id_producto = productoD.id_producto,
                nombre = productoD.nombre,
                precio_venta = productoD.precio_venta,
                precio_de_costo = productoD.precio_de_costo,
                stock_actual = productoD.stock_actual,
                stock_minimo = productoD.stock_minimo,
                activo = productoD.activo,
                id_categoria = productoD.id_categoria
            };

            await _producto.EditarProductosAsync(oprod);
        }

        //eliminar 

        public async Task EliminarProducto(int id)
        {
            await _producto.EliminarProductosAsync(id);
        }
    }
}
