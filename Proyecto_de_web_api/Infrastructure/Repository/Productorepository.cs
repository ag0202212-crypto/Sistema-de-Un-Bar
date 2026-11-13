using System;
using System.Collections.Generic;
using System.Data;
using System.Net.NetworkInformation;
using System.Text;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.DATABASE;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repository
{
    public class Productorepository : IProducto
    {
        private readonly DBConnectionfactory _connection;
        public Productorepository(DBConnectionfactory connection)
        {
            _connection = connection;
        }

        public async Task EditarProductosAsync(Producto productos)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("editar_productos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_producto", productos.id_producto));
                cmd.Parameters.Add(new SqlParameter("@nombre", productos.nombre));
                cmd.Parameters.Add(new SqlParameter("@precio_venta", productos.precio_venta));
                cmd.Parameters.Add(new SqlParameter("@precio_costo", productos.precio_de_costo));
                cmd.Parameters.Add(new SqlParameter("@stock_actual", productos.stock_actual));
                cmd.Parameters.Add(new SqlParameter("@stock_minimo", productos.stock_minimo));
                cmd.Parameters.Add(new SqlParameter("@activo", productos.activo));
                cmd.Parameters.Add(new SqlParameter("@id_categoria", productos.id_categoria));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public  async Task EliminarProductosAsync(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("eliminar_productos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_producto", id));


                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Producto>> ListarProductosAsync()
        {
            var lis = new List<Producto>();
            using var con = _connection.Createconection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("listar_productos", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                var oprod = new Producto
                {
                    id_producto = Convert.ToInt32(dr["id_producto"]),
                    nombre = dr["nombre"].ToString(),
                    precio_venta = Convert.ToDouble(dr["precio_venta"]),
                    precio_de_costo = Convert.ToDouble(dr["precio_costo"]),
                    stock_actual = Convert.ToInt32(dr["stock_actual"]),
                    stock_minimo = Convert.ToInt32(dr["stock_minimo"]),
                    activo = Convert.ToBoolean(dr["activo"]),
                    id_categoria = Convert.ToInt32(dr["id_categoria"])
                };
                lis.Add(oprod);
            }
            return lis;
        }

        public async Task NuevoProductos(Producto productos)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("nuevo_productos", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nombre", productos.nombre));
                cmd.Parameters.Add(new SqlParameter("@precio_venta", productos.precio_venta));
                cmd.Parameters.Add(new SqlParameter("@precio_costo", productos.precio_de_costo));
                cmd.Parameters.Add(new SqlParameter("@stock_actual", productos.stock_actual));
                cmd.Parameters.Add(new SqlParameter("@stock_minimo", productos.stock_minimo));
                cmd.Parameters.Add(new SqlParameter("@id_categoria", productos.id_categoria));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Producto?> ObtenerProducto(int id)
        {

            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("obtener_productos_por_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_producto", id));

                using var dr = await cmd.ExecuteReaderAsync();

                if (await dr.ReadAsync())
                {
                    return new Producto
                    {
                        id_producto = Convert.ToInt32(dr["id_producto"]),
                        nombre = dr["nombre"].ToString(),
                        precio_venta = Convert.ToDouble(dr["precio_venta"]),
                        precio_de_costo = Convert.ToDouble(dr["precio_costo"]),
                        stock_actual = Convert.ToInt32(dr["stock_actual"]),
                        stock_minimo = Convert.ToInt32(dr["stock_minimo"]),
                        activo = Convert.ToBoolean(dr["activo"]),
                        id_categoria = Convert.ToInt32(dr["id_categoria"])
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Categoria por id: " + ex.Message);
            }
        }
    }
}
