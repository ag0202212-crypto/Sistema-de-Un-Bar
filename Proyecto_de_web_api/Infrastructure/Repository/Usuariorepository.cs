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
    public class Usuariorepository : IUsuario
    {
        private readonly DBConnectionfactory _connection;
        public Usuariorepository(DBConnectionfactory connection)
        {
            _connection = connection;
        }

        public async Task EditarUsuarioAsync(Usuario usuario)
        {

            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("editar_usuario", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_usuario", usuario.id_usuario));
                cmd.Parameters.Add(new SqlParameter("@nombre", usuario.nombre));
                cmd.Parameters.Add(new SqlParameter("@cedula", usuario.cedula));
                cmd.Parameters.Add(new SqlParameter("@rol", usuario.rol));
                cmd.Parameters.Add(new SqlParameter("@contraseña", usuario.contraseña));
                cmd.Parameters.Add(new SqlParameter("@activo", usuario.activo));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EliminarUsuarioAsync(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("eliminar_usuario", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_usuario", id));


                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Usuario>> ListarUsuarioAsync()
        {
            var lis = new List<Usuario>();
            using var con = _connection.Createconection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("listar_usuario", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                var ous = new Usuario
                {
                    id_usuario = Convert.ToInt32(dr["id_usuario"]),
                    nombre = dr["nombre"].ToString(),
                    cedula = dr["cedula"].ToString(),
                    rol = dr["rol"].ToString(),
                    activo = Convert.ToBoolean(dr["activo"])
                };
                lis.Add(ous);
            }
            return lis;
        }

        public async Task NuevoUsuario(Usuario usuario)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("nuevo_usuario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nombre", usuario.nombre));
                cmd.Parameters.Add(new SqlParameter("@cedula", usuario.cedula));
                cmd.Parameters.Add(new SqlParameter("@rol", usuario.rol));
                cmd.Parameters.Add(new SqlParameter("@contraseña", usuario.contraseña));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Usuario?> ObtenerUsuario(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("obtener_usuario_por_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_usuario", id));

                using var dr = await cmd.ExecuteReaderAsync();

                if (await dr.ReadAsync())
                {
                    return new Usuario
                    {
                        id_usuario = Convert.ToInt32(dr["id_usuario"]),
                        nombre = dr["nombre"].ToString(),
                        cedula = dr["cedula"].ToString(),
                        rol = dr["rol"].ToString(),
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
