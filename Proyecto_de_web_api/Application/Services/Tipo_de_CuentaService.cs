using System;
using System.Collections.Generic;
using System.Text;
using Application.DTos;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class Tipo_de_CuentaService
    {
        private readonly ITipo_de_Cuenta _tipo_De_Cuenta;

        public Tipo_de_CuentaService(ITipo_de_Cuenta tipo_de_Cuenta)
        {
            _tipo_De_Cuenta = tipo_de_Cuenta;
        }
        //listar
        public async Task<IEnumerable<Tipo_de_CuentaDTos>> ListarTipo_de_Cuenta()
        {
            var m = await _tipo_De_Cuenta.ListarTipo_de_CuentaAsync();
            return m.Select(n => new Tipo_de_CuentaDTos
            {
                id_tipo_cuenta = n.id_tipo_cuenta,
                nombre_tipo_cuenta = n.nombre_tipo_cuenta,
                activo = n.activo
            });
        }
        //obtener por id
        public async Task<Tipo_de_CuentaDTos?> ObtenerTipo_de_Cuenta(int id)
        {
            // 1. Buscamos en la base de datos usando tu interfaz
            var n = await _tipo_De_Cuenta.ObtenerTipo_de_Cuenta(id);

            // 2. Si no existe, devolvemos nulo
            if (n == null) return null;

            // 3. Si existe, lo convertimos a DTO
            return new Tipo_de_CuentaDTos
            {
                id_tipo_cuenta = n.id_tipo_cuenta,
                nombre_tipo_cuenta = n.nombre_tipo_cuenta,
                activo = n.activo
            };
        }
        //agregar
        public async Task NuevoTipo_de_Cuenta(Tipo_de_CuentaDTos tipo_De_CuentaDTos)
        {
            var tipocuenta = new Tipo_de_Cuenta
            {
                nombre_tipo_cuenta = tipo_De_CuentaDTos.nombre_tipo_cuenta,

            };
            await _tipo_De_Cuenta.NuevoTipo_de_Cuenta(tipocuenta);

        }

        //editar

        public async Task EditarTipo_de_Cuenta(Tipo_de_CuentaDTos tipo_De_CuentaD)
        {
            var cuentatipo = new Tipo_de_Cuenta
            {
                id_tipo_cuenta = tipo_De_CuentaD.id_tipo_cuenta,
                nombre_tipo_cuenta = tipo_De_CuentaD.nombre_tipo_cuenta,
                activo = tipo_De_CuentaD.activo
            };

            await _tipo_De_Cuenta.EditarTipo_de_CuentaAsync(cuentatipo);
        }

        //eliminar 

        public async Task EliminarTipo_de_Cuenta(int id)
        {
            await _tipo_De_Cuenta.EliminarTipo_de_CuentaAsync(id);
        }
    }
}
