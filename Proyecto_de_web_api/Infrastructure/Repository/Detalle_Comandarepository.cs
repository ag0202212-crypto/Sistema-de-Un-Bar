using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.DATABASE;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repository
{
    public class Detalle_Comandarepository:IDetalle_Comanda
    {
        private readonly DBConnectionfactory _connection;

        public Detalle_Comandarepository(DBConnectionfactory connection)
        {
            _connection = connection;
        }

        public async Task EditarDetalle_ComandaAsync(Detalle_Comanda detalle_Comanda)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("editar_detalle_comanda", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_detalle_comanda", detalle_Comanda.id_detalle_comanda));
                cmd.Parameters.Add(new SqlParameter("@cantidad_pedida", detalle_Comanda.cantidad_pedida));
                cmd.Parameters.Add(new SqlParameter("@precio_venta", detalle_Comanda.precio_de_venta));
                cmd.Parameters.Add(new SqlParameter("@nota_especial", detalle_Comanda.nota_especial));
                cmd.Parameters.Add(new SqlParameter("@id_comanda", detalle_Comanda.id_comanda));
                cmd.Parameters.Add(new SqlParameter("@id_producto", detalle_Comanda.id_producto));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EliminarDetalle_ComandaAsync(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("eliminar_detalle_comanda", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_detalle_comanda", id));


                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Detalle_Comanda>> ListarDetalle_ComandaAsync()
        {
            var lis = new List<Detalle_Comanda>();
            using var con = _connection.Createconection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("listar_detalle_comanda", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                var odeta_coma = new Detalle_Comanda
                {
                    id_detalle_comanda = Convert.ToInt32(dr["id_detalle_comanda"]),
                    cantidad_pedida = Convert.ToInt32(dr["cantidad_pedida"]),
                    precio_de_venta = Convert.ToDouble(dr["precio_venta"]),
                    nota_especial = dr["nota_especial"]?.ToString(),
                    id_comanda = Convert.ToInt32(dr["id_comanda"]),
                    id_producto = Convert.ToInt32(dr["id_producto"])
                };
                lis.Add(odeta_coma);
            }
            return lis;
        }

        public async Task NuevoDetalle_Comanda(Detalle_Comanda detalle_Comanda)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("nuevo_detalle_comanda", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@cantidad_pedida", detalle_Comanda.cantidad_pedida));
                cmd.Parameters.Add(new SqlParameter("@precio_venta", detalle_Comanda.precio_de_venta));
                cmd.Parameters.Add(new SqlParameter("@nota_especial", detalle_Comanda.nota_especial));
                cmd.Parameters.Add(new SqlParameter("@id_comanda", detalle_Comanda.id_comanda));
                cmd.Parameters.Add(new SqlParameter("@id_producto", detalle_Comanda.id_producto));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Detalle_Comanda>> ObtenerPorComandaIdAsync(int idComanda)
        {
            var lista = new List<Detalle_Comanda>();
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("obtener_detalle_comanda_por_comanda", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_comanda", idComanda));

                using var dr = await cmd.ExecuteReaderAsync();

                while (await dr.ReadAsync()) // <--- USAS WHILE
                {
                    lista.Add(new Detalle_Comanda
                    {
                        id_detalle_comanda = Convert.ToInt32(dr["id_detalle_comanda"]),
                        cantidad_pedida = Convert.ToInt32(dr["cantidad_pedida"]),
                        precio_de_venta = Convert.ToDouble(dr["precio_venta"]),
                        nota_especial = dr["nota_especial"].ToString(),
                        id_comanda = Convert.ToInt32(dr["id_comanda"]),
                        id_producto = Convert.ToInt32(dr["id_producto"])
                    });
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener detalles de  comanda: " + ex.Message);
            }
        }

        public async Task<Detalle_Comanda?> ObtenerPorIdAsync(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("obtener_detalle_comanda_por_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_detalle_comanda", id));

                using var dr = await cmd.ExecuteReaderAsync();

                if (await dr.ReadAsync())
                {
                    return new Detalle_Comanda
                    {
                        id_detalle_comanda = Convert.ToInt32(dr["id_detalle_comanda"]),
                        cantidad_pedida = Convert.ToInt32(dr["cantidad_pedida"]),
                        precio_de_venta = Convert.ToDouble(dr["precio_venta"]),
                        nota_especial = dr["nota_especial"].ToString(),
                        id_comanda = Convert.ToInt32(dr["id_comanda"]),
                        id_producto = Convert.ToInt32(dr["id_producto"])
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Detalle Comanda por id: " + ex.Message);
            }
        }
    }
}
