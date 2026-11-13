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
    public class Kardexrepository:IKardex
    {
        private readonly DBConnectionfactory _connection;

        public Kardexrepository(DBConnectionfactory connection)
        {
            _connection = connection;
        }

        public async Task EditarKardexAsync(Kardex kardex)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("editar_kardex", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_kardex", kardex.id_kardex));
                cmd.Parameters.Add(new SqlParameter("@tipo_movimiento", kardex.tipo_de_movimiento));
                cmd.Parameters.Add(new SqlParameter("@cantidad", kardex.cantidad));
                cmd.Parameters.Add(new SqlParameter("@fecha", kardex.fecha));
                cmd.Parameters.Add(new SqlParameter("@id_producto", kardex.id_producto));


                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EliminarKardexAsync(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("eliminar_kardex", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_kardex", id));



                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Kardex>> ListarKardexAsync()
        {
            var lis = new List<Kardex>();
            using var con = _connection.Createconection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("listar_kardex", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                var okar = new Kardex
                {
                    id_kardex = Convert.ToInt32(dr["id_kardex"]),
                    tipo_de_movimiento = dr["tipo_movimiento"].ToString(),
                    cantidad = Convert.ToInt32(dr["cantidad"]),
                    fecha = Convert.ToDateTime(dr["fecha"]),
                    id_producto = Convert.ToInt32(dr["id_producto"])
                };
                lis.Add(okar);
            }
            return lis;
        }

        public async Task NuevoKardex(Kardex kardex)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("nuevo_kardex", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@tipo_movimiento", kardex.tipo_de_movimiento));
                cmd.Parameters.Add(new SqlParameter("@cantidad", kardex.cantidad));
                cmd.Parameters.Add(new SqlParameter("@fecha", kardex.fecha));
                cmd.Parameters.Add(new SqlParameter("@id_producto", kardex.id_producto));


                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Kardex?> ObtenerKardex(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("obtener_kardex_por_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_kardex", id));

                using var dr = await cmd.ExecuteReaderAsync();

                if (await dr.ReadAsync())
                {
                    return new Kardex
                    {
                        id_kardex = Convert.ToInt32(dr["id_kardex"]),
                        tipo_de_movimiento = dr["tipo_movimiento"].ToString(),
                        cantidad = Convert.ToInt32(dr["cantidad"]),
                        fecha = Convert.ToDateTime(dr["fecha"]),
                        id_producto = Convert.ToInt32(dr["id_producto"])
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Kardex por id: " + ex.Message);
            }
        }
    }
}
