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
    public class Cuenta_Contablerepository:ICuenta_Contable
    {
        private readonly DBConnectionfactory _connection;

        public Cuenta_Contablerepository(DBConnectionfactory connection)
        {
            _connection = connection;
        }

        public async Task EditarCuenta_ContableAsync(Cuenta_Contable cuenta_Contable)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("editar_cuenta_contable", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_cuenta_contable", cuenta_Contable.id_cuenta_contable));
                cmd.Parameters.Add(new SqlParameter("@cod_cuenta", cuenta_Contable.cod_cuenta));
                cmd.Parameters.Add(new SqlParameter("@nombre_cuenta", cuenta_Contable.nombre_cuenta));
                cmd.Parameters.Add(new SqlParameter("@activo", cuenta_Contable.activo));
                cmd.Parameters.Add(new SqlParameter("@id_tipo_cuenta", cuenta_Contable.id_tipo_cuenta));


                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EliminarCuenta_ContableAsync(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("eliminar_cuenta_contable", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_cuenta_contable", id));



                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Cuenta_Contable>> ListarCuenta_ContableAsync()
        {
            var lis = new List<Cuenta_Contable>();
            using var con = _connection.Createconection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("listar_cuenta_contable", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                var ocuenta = new Cuenta_Contable
                {
                    id_cuenta_contable = Convert.ToInt32(dr["id_cuenta_contable"]),
                    cod_cuenta = dr["cod_cuenta"].ToString(),
                    nombre_cuenta = dr["nombre_cuenta"].ToString(),
                    activo = Convert.ToBoolean(dr["activo"]),
                    id_tipo_cuenta = Convert.ToInt32(dr["id_tipo_cuenta"])

                };
                lis.Add(ocuenta);
            }
            return lis;
        }

        public async Task NuevoCuenta_Contable(Cuenta_Contable cuenta_Contable)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("nuevo_cuenta_contable", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@cod_cuenta", cuenta_Contable.cod_cuenta));
                cmd.Parameters.Add(new SqlParameter("@nombre_cuenta", cuenta_Contable.nombre_cuenta));
                cmd.Parameters.Add(new SqlParameter("@id_tipo_cuenta", cuenta_Contable.id_tipo_cuenta));


                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cuenta_Contable?> ObtenerCuenta_Contable(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("obtener_cuenta_contable_por_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_cuenta_contable", id));

                using var dr = await cmd.ExecuteReaderAsync();

                if (await dr.ReadAsync())
                {
                    return new Cuenta_Contable
                    {
                        id_cuenta_contable = Convert.ToInt32(dr["id_cuenta_contable"]),
                        cod_cuenta = dr["cod_cuenta"].ToString(),
                        nombre_cuenta = dr["nombre_cuenta"].ToString(),
                        activo = Convert.ToBoolean(dr["activo"]),
                        id_tipo_cuenta = Convert.ToInt32(dr["id_tipo_cuenta"])
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Cuenta Contable por id: " + ex.Message);
            }
        }
    }
}
