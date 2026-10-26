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
    public class Asiento_Contablerepository : IAsiento_Contable
    {
        private readonly DBConnectionfactory _connection;

        public Asiento_Contablerepository(DBConnectionfactory connection)
        {
            _connection = connection;
        }

        public async Task EditarAsiento_ContableAsync(Asiento_Contable asiento_Contable)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();
            try
            {
                using var cmd = new SqlCommand("editar_asiento_contable", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_asiento_contable", asiento_Contable.id_asiento_contable));
                cmd.Parameters.Add(new SqlParameter("@fecha_asiento", asiento_Contable.fecha_asiento));
                cmd.Parameters.Add(new SqlParameter("@glosa", asiento_Contable.glosa));
                cmd.Parameters.Add(new SqlParameter("@id_factura", asiento_Contable.id_factura));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EliminarAsiento_ContableAsync(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();
            try
            {
                using var cmd = new SqlCommand("eliminar_asiento_contable", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_asiento_contable", id));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Asiento_Contable>> ListarAsiento_ContableAsync()
        {
            var lis = new List<Asiento_Contable>();
            using var con = _connection.Createconection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("listar_asiento_contable", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                var oasiento_contable = new Asiento_Contable
                {
                    id_asiento_contable = Convert.ToInt32(dr["id_asiento_contable"]),
                    fecha_asiento = Convert.ToDateTime(dr["fecha_asiento"]),
                    glosa = dr["glosa"].ToString(),
                    id_factura = Convert.ToInt32(dr["id_factura"])
                };
                lis.Add(oasiento_contable);
            }
            return lis;
        }

        public async Task NuevoAsiento_Contable(Asiento_Contable asiento_Contable)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("nuevo_asiento_contable", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@fecha_asiento", asiento_Contable.fecha_asiento));
                cmd.Parameters.Add(new SqlParameter("@glosa", asiento_Contable.glosa));
                cmd.Parameters.Add(new SqlParameter("@id_factura", asiento_Contable.id_factura));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Asiento_Contable?> ObtenerAsiento_Contable(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("obtener_asiento_contable_por_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_asiento_contable", id));

                using var dr = await cmd.ExecuteReaderAsync();

                if (await dr.ReadAsync())
                {
                    return new Asiento_Contable
                    {
                        id_asiento_contable = Convert.ToInt32(dr["id_asiento_contable"]),
                        fecha_asiento = Convert.ToDateTime(dr["fecha_asiento"]),
                        glosa = dr["glosa"].ToString(),
                        id_factura = Convert.ToInt32(dr["id_factura"])
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener asiento contable por id: " + ex.Message);
            }
        }
    }
}
