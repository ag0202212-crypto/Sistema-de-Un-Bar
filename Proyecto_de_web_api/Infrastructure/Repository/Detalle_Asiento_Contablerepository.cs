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
    public class Detalle_Asiento_Contablerepository:IDetalle_Asiento_Contable
    {
        private readonly DBConnectionfactory _connection;

        public Detalle_Asiento_Contablerepository(DBConnectionfactory connection)
        {
            _connection = connection;
        }

        public async Task EditarDetalle_Asiento_ContableAsync(Detalle_Asiento_Contable detalle_Asiento_Contable)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("editar_detalle_asiento_contable", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_detalle_asiento_contable", detalle_Asiento_Contable.id_detalle_asiento_contable));
                cmd.Parameters.Add(new SqlParameter("@monto_debe", detalle_Asiento_Contable.monto_debe));
                cmd.Parameters.Add(new SqlParameter("@monto_haber", detalle_Asiento_Contable.monto_haber));
                cmd.Parameters.Add(new SqlParameter("@id_asiento_contable", detalle_Asiento_Contable.id_asiento_contable));
                cmd.Parameters.Add(new SqlParameter("@id_cuenta_contable", detalle_Asiento_Contable.id_cuenta_contable));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EliminarDetalle_Asiento_ContableAsync(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("eliminar_detalle_asiento_contable", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_detalle_asiento_contable", id));


                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Detalle_Asiento_Contable>> ListarDetalle_Asiento_ContableAsync()
        {
            var lis = new List<Detalle_Asiento_Contable>();
            using var con = _connection.Createconection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("listar_detalle_asiento_contable", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                var odetalle = new Detalle_Asiento_Contable
                {
                    id_detalle_asiento_contable = Convert.ToInt32(dr["id_detalle_asiento_contable"]),
                    monto_debe = Convert.ToDouble(dr["monto_debe"]),
                    monto_haber = Convert.ToDouble(dr["monto_haber"]),
                    id_asiento_contable = Convert.ToInt32(dr["id_asiento_contable"]),
                    id_cuenta_contable = Convert.ToInt32(dr["id_cuenta_contable"])
                };
                lis.Add(odetalle);
            }
            return lis;
        }

        public async Task NuevoDetalle_Asiento_Contable(Detalle_Asiento_Contable detalle_Asiento_Contable)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("nuevo_detalle_asiento_contable", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@monto_debe", detalle_Asiento_Contable.monto_debe));
                cmd.Parameters.Add(new SqlParameter("@monto_haber", detalle_Asiento_Contable.monto_haber));
                cmd.Parameters.Add(new SqlParameter("@id_asiento_contable", detalle_Asiento_Contable.id_asiento_contable));
                cmd.Parameters.Add(new SqlParameter("@id_cuenta_contable", detalle_Asiento_Contable.id_cuenta_contable));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Detalle_Asiento_Contable>> ObtenerPorAsientoIdAsync(int idAsiento)
        {
            var lista = new List<Detalle_Asiento_Contable>();
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("obtener_detalle_asiento_por_asiento", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_asiento_contable", idAsiento));

                using var dr = await cmd.ExecuteReaderAsync();

                while (await dr.ReadAsync()) // <--- USAS WHILE
                {
                    lista.Add(new Detalle_Asiento_Contable
                    {
                        id_detalle_asiento_contable = Convert.ToInt32(dr["id_detalle_asiento_contable"]),
                        monto_debe = Convert.ToDouble(dr["monto_debe"]),
                        monto_haber = Convert.ToDouble(dr["monto_haber"]),
                        id_asiento_contable = Convert.ToInt32(dr["id_asiento_contable"]),
                        id_cuenta_contable = Convert.ToInt32(dr["id_cuenta_contable"])
                    });
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener detalle asiento contable: " + ex.Message);
            }
        }

        public async Task<Detalle_Asiento_Contable?> ObtenerPorIdAsync(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("obtener_detalle_asiento_contable_por_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_detalle_asiento_contable", id));

                using var dr = await cmd.ExecuteReaderAsync();

                if (await dr.ReadAsync())
                {
                    return new Detalle_Asiento_Contable
                    {
                        id_detalle_asiento_contable = Convert.ToInt32(dr["id_detalle_asiento_contable"]),
                        monto_debe = Convert.ToDouble(dr["monto_debe"]),
                        monto_haber = Convert.ToDouble(dr["monto_haber"]),
                        id_asiento_contable = Convert.ToInt32(dr["id_asiento_contable"]),
                        id_cuenta_contable = Convert.ToInt32(dr["id_cuenta_contable"])
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener detalle asiento contable por id: " + ex.Message);
            }
        }
    }
}
