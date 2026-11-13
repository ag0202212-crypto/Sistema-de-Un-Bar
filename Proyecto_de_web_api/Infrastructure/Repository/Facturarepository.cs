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
    public class Facturarepository:IFactura
    {
        private readonly DBConnectionfactory _connection;

        public Facturarepository(DBConnectionfactory connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Factura>> BuscarFacturaAsync(string criterio)
        {
            var lis = new List<Factura>();
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("buscar_factura", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@criterio", criterio));

                using var dr = await cmd.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    var ofac = new Factura
                    {
                        id_factura = Convert.ToInt32(dr["id_factura"]),
                        nombre_factura = dr["numero_factura"].ToString()!,
                        fecha_y_hora_de_cierre = Convert.ToDateTime(dr["fecha_hora_cierre"]),
                        subtotal = Convert.ToDouble(dr["subtotal"]),
                        iva = Convert.ToDouble(dr["iva"]),
                        total = Convert.ToDouble(dr["total"]),
                        estado_de_la_factura = dr["estado_factura"].ToString()!,
                        id_comanda = Convert.ToInt32(dr["id_comanda"]),
                        id_cliente = dr["id_cliente"] == DBNull.Value ? null : Convert.ToInt32(dr["id_cliente"])
                    };
                    lis.Add(ofac);
                }
                return lis;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar facturas: " + ex.Message);
            }
        }

        public async Task EditarFacturaAsync(Factura factura)
        {

            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("editar_factura", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_factura", factura.id_factura));
                cmd.Parameters.Add(new SqlParameter("@numero_factura", factura.nombre_factura));
                cmd.Parameters.Add(new SqlParameter("@fecha_hora_cierre", factura.fecha_y_hora_de_cierre));
                cmd.Parameters.Add(new SqlParameter("@subtotal", factura.subtotal));
                cmd.Parameters.Add(new SqlParameter("@iva", factura.iva));
                cmd.Parameters.Add(new SqlParameter("@total", factura.total));
                cmd.Parameters.Add(new SqlParameter("@estado_factura", factura.estado_de_la_factura));

                cmd.Parameters.Add(new SqlParameter("@id_comanda", factura.id_comanda));
                cmd.Parameters.Add(new SqlParameter("@id_cliente", factura.id_cliente));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EliminarFacturaAsync(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("eliminar_factura", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id_factura", id));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Factura>> ListarFacturaAsync()
        {
            var lis = new List<Factura>();
            using var con = _connection.Createconection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("listar_factura", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                var ofac = new Factura
                {
                    id_factura = Convert.ToInt32(dr["id_factura"]),
                    nombre_factura = dr["numero_factura"].ToString(),
                    fecha_y_hora_de_cierre = Convert.ToDateTime(dr["fecha_hora_cierre"]),
                    subtotal = Convert.ToDouble(dr["subtotal"]),
                    iva = Convert.ToDouble(dr["iva"]),
                    total = Convert.ToDouble(dr["total"]),
                    estado_de_la_factura = dr["estado_factura"].ToString(),
                    id_comanda = Convert.ToInt32(dr["id_comanda"]),
                    id_cliente = dr["id_cliente"] == DBNull.Value ? null : Convert.ToInt32(dr["id_cliente"])
                };
                lis.Add(ofac);
            }
            return lis;
        }

        public async Task NuevoFactura(Factura factura)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("nuevo_factura", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@numero_factura", factura.nombre_factura));
                cmd.Parameters.Add(new SqlParameter("@fecha_hora_cierre", factura.fecha_y_hora_de_cierre));
                cmd.Parameters.Add(new SqlParameter("@subtotal", factura.subtotal));
                cmd.Parameters.Add(new SqlParameter("@iva", factura.iva));
                cmd.Parameters.Add(new SqlParameter("@total", factura.total));
                cmd.Parameters.Add(new SqlParameter("@estado_factura", factura.estado_de_la_factura));
                cmd.Parameters.Add(new SqlParameter("@id_comanda", factura.id_comanda));
                cmd.Parameters.Add(new SqlParameter("@id_cliente", factura.id_cliente));

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Factura?> ObtenerFactura(int id)
        {
            using var con = _connection.Createconection();
            await con.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("obtener_factura_por_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_factura", id));

                using var dr = await cmd.ExecuteReaderAsync();

                if (await dr.ReadAsync())
                {
                    return new Factura
                    {
                        id_factura = Convert.ToInt32(dr["id_factura"]),
                        nombre_factura = dr["numero_factura"].ToString(),
                        fecha_y_hora_de_cierre = Convert.ToDateTime(dr["fecha_hora_cierre"]),
                        subtotal = Convert.ToDouble(dr["subtotal"]),
                        iva = Convert.ToDouble(dr["iva"]),
                        total = Convert.ToDouble(dr["total"]),
                        estado_de_la_factura = dr["estado_factura"].ToString(),
                        id_comanda = Convert.ToInt32(dr["id_comanda"]),
                        id_cliente = Convert.ToInt32(dr["id_cliente"])
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
