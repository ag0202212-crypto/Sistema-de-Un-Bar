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
    public class Direccionrepository:IDireccion
    {
        private readonly DBConnectionfactory _connection;

        public Direccionrepository(DBConnectionfactory connection)
        {
            _connection = connection;
        }

        public async Task EditarDireccionAsync(Direccion direccion)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("editar_direccion", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_direccion", direccion.id_direccion));
                cmd.Parameters.Add(new SqlParameter("@departamento", direccion.departamento));
                cmd.Parameters.Add(new SqlParameter("@municipio", direccion.Municipio));
                cmd.Parameters.Add(new SqlParameter("@detalle_direccion", direccion.detalle_de_direccion));
                cmd.Parameters.Add(new SqlParameter("@activo", direccion.activo));


                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EliminarDireccionAsync(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("eliminar_direccion", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_direccion", id));


                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Direccion>> ListarDireccionAsync()
        {
            var lis = new List<Direccion>();
            using var con = _connection.Createconection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("listar_direccion", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                var oclien = new Direccion
                {
                    id_direccion = Convert.ToInt32(dr["id_direccion"]),
                    departamento = dr["departamento"].ToString(),
                    Municipio = dr["municipio"].ToString(),
                    detalle_de_direccion = dr["detalle_direccion"].ToString(),
                    activo = Convert.ToBoolean(dr["activo"])
                };
                lis.Add(oclien);
            }
            return lis;
        }

        public async Task NuevoDireccion(Direccion direccion)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("nuevo_direccion", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@departamento", direccion.departamento));
                cmd.Parameters.Add(new SqlParameter("@municipio", direccion.Municipio));
                cmd.Parameters.Add(new SqlParameter("@detalle_direccion", direccion.detalle_de_direccion));


                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Direccion?> ObtenerDireccion(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("obtener_direccion_por_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_direccion", id));

                using var dr = await cmd.ExecuteReaderAsync();

                if (await dr.ReadAsync())
                {
                    return new Direccion
                    {
                        id_direccion = Convert.ToInt32(dr["id_direccion"]),
                        departamento = dr["departamento"].ToString(),
                        Municipio = dr["municipio"].ToString(),
                        detalle_de_direccion = dr["detalle_direccion"].ToString(),
                        activo = Convert.ToBoolean(dr["activo"])
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Direccion por id: " + ex.Message);
            }
        }
    }
}
