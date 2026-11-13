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
    public class Clienterepository:ICliente
    {
        private readonly DBConnectionfactory _dbConnectionfactory;

        public Clienterepository(DBConnectionfactory dbConnectionfactory)
        {
            _dbConnectionfactory = dbConnectionfactory;
        }

        public async Task EditarClienteAsync(Cliente cliente)
        {
            using var con = _dbConnectionfactory.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("editar_cliente", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Datos Personales
                cmd.Parameters.Add(new SqlParameter("@id_cliente", cliente.id_cliente));
                cmd.Parameters.Add(new SqlParameter("@nombre", (object?)cliente.nombre ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@cedula", (object?)cliente.cedula ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@telefono", (object?)cliente.telefono ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@activo", cliente.activo));

                // Datos de Dirección
                cmd.Parameters.Add(new SqlParameter("@id_direccion", cliente.id_direccion));
                cmd.Parameters.Add(new SqlParameter("@departamento", (object?)cliente.departamento ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@municipio", (object?)cliente.municipio ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@detalle_direccion", (object?)cliente.detalle_direccion ?? DBNull.Value));
                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EliminarClienteAsync(int id)
        {
            using var con = _dbConnectionfactory.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("eliminar_cliente", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_cliente", id));


                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Cliente>> ListarClienteAsync()
        {
            var lis = new List<Cliente>();
            using var con = _dbConnectionfactory.Createconection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("listar_cliente", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                var oclien = new Cliente
                {
                    id_cliente = Convert.ToInt32(dr["id_cliente"]),
                    nombre = dr["nombre"].ToString(),
                    cedula = dr["cedula"].ToString(),
                    telefono = dr["telefono"].ToString(),
                    activo = Convert.ToBoolean(dr["activo"]),
                    id_direccion = Convert.ToInt32(dr["id_direccion"]),
                    departamento = dr["departamento"]?.ToString(),
                    municipio = dr["municipio"]?.ToString(),
                    detalle_direccion = dr["detalle_direccion"]?.ToString()
                };
                lis.Add(oclien);
            }
            return lis;
        }

        public async Task NuevoCliente(Cliente cliente)
        {
            using var con = _dbConnectionfactory.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("nuevo_cliente", con);
                cmd.CommandType = CommandType.StoredProcedure;
                // Datos Personales
                cmd.Parameters.Add(new SqlParameter("@nombre", (object?)cliente.nombre ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@cedula", (object?)cliente.cedula ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@telefono", (object?)cliente.telefono ?? DBNull.Value));

                // Datos de Dirección (El SP se encarga de crear el ID)
                cmd.Parameters.Add(new SqlParameter("@departamento", (object?)cliente.departamento ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@municipio", (object?)cliente.municipio ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@detalle_direccion", (object?)cliente.detalle_direccion ?? DBNull.Value));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cliente?> ObtenerCliente(int id)
        {
            using var con = _dbConnectionfactory.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("obtener_cliente_por_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_cliente", id));

                using var dr = await cmd.ExecuteReaderAsync();

                if (await dr.ReadAsync())
                {
                    return new Cliente
                    {
                        id_cliente = Convert.ToInt32(dr["id_cliente"]),
                        nombre = dr["nombre"].ToString(),
                        cedula = dr["cedula"].ToString(),
                        telefono = dr["telefono"].ToString(),
                        activo = Convert.ToBoolean(dr["activo"]),
                        id_direccion = Convert.ToInt32(dr["id_direccion"]),
                        departamento = dr["departamento"]?.ToString(),
                        municipio = dr["municipio"]?.ToString(),
                        detalle_direccion = dr["detalle_direccion"]?.ToString()

                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener cliente por id: " + ex.Message);
            }
        }
    }
}
