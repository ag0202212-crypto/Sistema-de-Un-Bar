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
    public class Categoriarepository : ICategoria
    {
        private readonly DBConnectionfactory _dBConnectionfactory;

        public Categoriarepository(DBConnectionfactory dBConnectionfactory)
        {
            _dBConnectionfactory = dBConnectionfactory;
        }

        public async Task EditarCategoriaAsync(Categoria categoria)
        {
            using var con = _dBConnectionfactory.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("editar_categoria", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_categoria", categoria.id_categoria));
                cmd.Parameters.Add(new SqlParameter("@nombrecat", categoria.nombrecat));
                cmd.Parameters.Add(new SqlParameter("@activo", categoria.activo));


                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EliminarCategoriaAsync(int id)
        {
            using var con = _dBConnectionfactory.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("eliminar_categoria", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_categoria", id));



                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Categoria>> ListarCategoriaAsync()
        {
            var lis = new List<Categoria>();
            using var con = _dBConnectionfactory.Createconection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("listar_categoria", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                var ocateg = new Categoria
                {
                    id_categoria = Convert.ToInt32(dr["id_categoria"]),
                    nombrecat = dr["nombrecat"].ToString(),
                    activo = Convert.ToBoolean(dr["activo"])
                };
                lis.Add(ocateg);
            }
            return lis;

        }

        public async Task NuevoCategoria(Categoria categoria)
        {
            using var con = _dBConnectionfactory.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("nuevo_categoria", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@nombrecat", categoria.nombrecat));


                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Categoria?> ObtenerCategoria(int id)
        {
            using var con = _dBConnectionfactory.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("obtener_categoria_por_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_categoria", id));

                using var dr = await cmd.ExecuteReaderAsync();

                if (await dr.ReadAsync())
                {
                    return new Categoria
                    {
                        id_categoria = Convert.ToInt32(dr["id_categoria"]),
                        nombrecat = dr["nombrecat"].ToString(),
                        activo = Convert.ToBoolean(dr["activo"])
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
