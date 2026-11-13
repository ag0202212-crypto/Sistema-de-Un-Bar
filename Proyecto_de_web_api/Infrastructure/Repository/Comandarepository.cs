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
    public class Comandarepository:IComanda
    {
        private readonly DBConnectionfactory _connection;

        public Comandarepository(DBConnectionfactory connection)
        {
            _connection = connection;
        }

        public async Task EditarComandaAsync(Comanda comanda)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("editar_comanda", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_comanda", comanda.id_comanda));
                cmd.Parameters.Add(new SqlParameter("@fecha_hora_apertura", comanda.fecha_y_hora_de_apertura));
                cmd.Parameters.Add(new SqlParameter("@estado", comanda.estado));
                cmd.Parameters.Add(new SqlParameter("@id_mesa", comanda.id_mesa));
                cmd.Parameters.Add(new SqlParameter("@id_usuario", comanda.id_usuario));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EliminarComandaAsync(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("eliminar_comanda", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_comanda", id));


                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Comanda>> ListarComandaAsync()
        {
            var lis = new List<Comanda>();
            using var con = _connection.Createconection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("listar_comanda", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                var ocoma = new Comanda
                {
                    id_comanda = Convert.ToInt32(dr["id_comanda"]),
                    fecha_y_hora_de_apertura = Convert.ToDateTime(dr["fecha_hora_apertura"]),
                    estado = dr["estado"].ToString(),
                    id_mesa = Convert.ToInt32(dr["id_mesa"]),
                    id_usuario = Convert.ToInt32(dr["id_usuario"])
                };
                lis.Add(ocoma);
            }
            return lis;
        }

        public async Task NuevoComanda(Comanda comanda)
        {

            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("nuevo_comanda", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@fecha_hora_apertura", comanda.fecha_y_hora_de_apertura));
                cmd.Parameters.Add(new SqlParameter("@estado", comanda.estado));
                cmd.Parameters.Add(new SqlParameter("@id_mesa", comanda.id_mesa));
                cmd.Parameters.Add(new SqlParameter("@id_usuario", comanda.id_usuario));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Comanda?> ObtenerComanda(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("obtener_comanda_por_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_comanda", id));

                using var dr = await cmd.ExecuteReaderAsync();

                if (await dr.ReadAsync())
                {
                    return new Comanda
                    {
                        id_comanda = Convert.ToInt32(dr["id_comanda"]),
                        fecha_y_hora_de_apertura = Convert.ToDateTime(dr["fecha_hora_apertura"]),
                        estado = dr["estado"].ToString(),
                        id_mesa = Convert.ToInt32(dr["id_mesa"]),
                        id_usuario = Convert.ToInt32(dr["id_usuario"])
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Comanda por id: " + ex.Message);
            }
        }
    }
}
