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
    public class Tipo_de_Cuentarepository : ITipo_de_Cuenta
    {
        private readonly DBConnectionfactory _connection;

        public Tipo_de_Cuentarepository(DBConnectionfactory connection)
        {
            _connection = connection;
        }

        public async Task EditarTipo_de_CuentaAsync(Tipo_de_Cuenta tipo_De_Cuenta)
        {

            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("editar_tipo_cuenta", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_tipo_cuenta", tipo_De_Cuenta.id_tipo_cuenta));
                cmd.Parameters.Add(new SqlParameter("@nombre_tipo_cuenta", tipo_De_Cuenta.nombre_tipo_cuenta));
                cmd.Parameters.Add(new SqlParameter("@activo", tipo_De_Cuenta.activo));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EliminarTipo_de_CuentaAsync(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("eliminar_tipo_cuenta", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_tipo_cuenta", id));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Tipo_de_Cuenta>> ListarTipo_de_CuentaAsync()
        {

            var lis = new List<Tipo_de_Cuenta>();
            using var con = _connection.Createconection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("listar_tipo_cuenta", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                var otipo = new Tipo_de_Cuenta
                {
                    id_tipo_cuenta = Convert.ToInt32(dr["id_tipo_cuenta"]),
                    nombre_tipo_cuenta = dr["nombre_tipo_cuenta"].ToString(),
                    activo = Convert.ToBoolean(dr["activo"])
                };
                lis.Add(otipo);
            }
            return lis;
        }

        public async Task NuevoTipo_de_Cuenta(Tipo_de_Cuenta tipo_De_Cuenta)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("nuevo_tipo_cuenta", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@nombre_tipo_cuenta", tipo_De_Cuenta.nombre_tipo_cuenta));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Tipo_de_Cuenta?> ObtenerTipo_de_Cuenta(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("obtener_tipo_cuenta_por_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_tipo_cuenta", id));

                using var dr = await cmd.ExecuteReaderAsync();

                if (await dr.ReadAsync())
                {
                    return new Tipo_de_Cuenta
                    {
                        id_tipo_cuenta = Convert.ToInt32(dr["id_tipo_cuenta"]),
                        nombre_tipo_cuenta = dr["nombre_tipo_cuenta"].ToString(),
                        activo = Convert.ToBoolean(dr["activo"])
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener tipo de cuenta por id: " + ex.Message);
            }
        }
    }
}
