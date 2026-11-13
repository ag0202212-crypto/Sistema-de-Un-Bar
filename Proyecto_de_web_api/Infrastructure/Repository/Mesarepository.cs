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
    public class Mesarepository : IMesa
    {
        private readonly DBConnectionfactory _connection;

        public Mesarepository(DBConnectionfactory connection)
        {
            _connection = connection;
        }

        public  async Task EditarMesaAsync(Mesa mesa)
        {

            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("editar_mesa", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_mesa", mesa.id_mesa));
                cmd.Parameters.Add(new SqlParameter("@numero_mesa", mesa.numero_de_mesa));
                cmd.Parameters.Add(new SqlParameter("@capacidad_persona", mesa.capacidad_de_persona));
                cmd.Parameters.Add(new SqlParameter("@estado", mesa.estado));
                cmd.Parameters.Add(new SqlParameter("@activo", mesa.activo));
                cmd.Parameters.Add(new SqlParameter("@id_zona", mesa.id_zona));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EliminarMesaAsync(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("eliminar_mesa", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_mesa", id));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public  async Task<IEnumerable<Mesa>> ListarMesaAsync()
        {

            var lis = new List<Mesa>();
            using var con = _connection.Createconection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("listar_mesa", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                var ome = new Mesa
                {
                    id_mesa = Convert.ToInt32(dr["id_mesa"]),
                    numero_de_mesa = Convert.ToInt32(dr["numero_mesa"]),
                    capacidad_de_persona = Convert.ToInt32(dr["capacidad_persona"]),
                    estado = dr["estado"].ToString(),
                    activo = Convert.ToBoolean(dr["activo"]),
                    id_zona = Convert.ToInt32(dr["id_zona"])
                };
                lis.Add(ome);
            }
            return lis;
        }

        public async Task NuevoMesa(Mesa mesa)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("nuevo_mesa", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@numero_mesa", mesa.numero_de_mesa));
                cmd.Parameters.Add(new SqlParameter("@capacidad_persona", mesa.capacidad_de_persona));
                cmd.Parameters.Add(new SqlParameter("@estado", mesa.estado));
                cmd.Parameters.Add(new SqlParameter("@id_zona", mesa.id_zona));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Mesa?> ObtenerMesa(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("obtener_mesa_por_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_mesa", id));

                using var dr = await cmd.ExecuteReaderAsync();

                if (await dr.ReadAsync())
                {
                    return new Mesa
                    {
                        id_mesa = Convert.ToInt32(dr["id_mesa"]),
                        numero_de_mesa = Convert.ToInt32(dr["numero_mesa"]),
                        capacidad_de_persona = Convert.ToInt32(dr["capacidad_persona"]),
                        estado = dr["estado"].ToString(),
                        activo = Convert.ToBoolean(dr["activo"]),
                        id_zona = Convert.ToInt32(dr["id_zona"])
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener mesa por id: " + ex.Message);
            }
        }
    }
}
