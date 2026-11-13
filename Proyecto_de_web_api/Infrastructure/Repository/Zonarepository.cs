using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.DATABASE;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repository
{
    public class Zonarepository : IZona
    {
        private readonly DBConnectionfactory _dbconnectionfactory;

        public Zonarepository(DBConnectionfactory dbconnectionfactory)
        {
            _dbconnectionfactory = dbconnectionfactory;
        }

        public async Task EditarZonaAsync(Zona zona)
        {
            using var con = _dbconnectionfactory.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("editar_zona", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_zona", zona.id_zona));
                cmd.Parameters.Add(new SqlParameter("@nombre_zona", zona.nombre_zona));
                cmd.Parameters.Add(new SqlParameter("@activo", zona.activo));


                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EliminarZonaAsync(int id)
        {
            using var con = _dbconnectionfactory.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("eliminar_zona", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_zona", id));


                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Zona>> ListarZonaAsync()
        {
            var lis = new List<Zona>();
            using var con = _dbconnectionfactory.Createconection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("listar_zona", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                var ozo = new Zona
                {
                    id_zona = Convert.ToInt32(dr["id_zona"]),
                    nombre_zona = dr["nombre_zona"].ToString(),
                    activo = Convert.ToBoolean(dr["activo"])
                };
                lis.Add(ozo);
            }
            return lis;
        }

        public async Task NuevoZona(Zona zona)
        {
            using var con = _dbconnectionfactory.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("nuevo_zona", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@nombre_zona", zona.nombre_zona));


                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Zona?> ObtenerZona(int id)
        {

            using var con = _dbconnectionfactory.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("obtener_zona_por_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_zona", id));

                using var dr = await cmd.ExecuteReaderAsync();

                if (await dr.ReadAsync())
                {
                    return new Zona
                    {
                        id_zona = Convert.ToInt32(dr["id_zona"]),
                        nombre_zona = dr["nombre_zona"].ToString(),
                        activo = Convert.ToBoolean(dr["activo"])
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Zona por id: " + ex.Message);
            }
        }
    }
}
