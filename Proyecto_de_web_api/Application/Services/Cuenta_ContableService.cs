using System;
using System.Collections.Generic;
using System.Text;
using Application.DTos;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class Cuenta_ContableService
    {
        private readonly ICuenta_Contable _cuenta_Contable;

        public Cuenta_ContableService(ICuenta_Contable cuenta_Contable)
        {
            _cuenta_Contable = cuenta_Contable;
        }

        //listar
        public async Task<IEnumerable<Cuenta_ContableDTos>> ListarCuenta_Contable()
        {
            var l = await _cuenta_Contable.ListarCuenta_ContableAsync();
            return l.Select(m => new Cuenta_ContableDTos
            {
                id_cuenta_contable = m.id_cuenta_contable,
                cod_cuenta = m.cod_cuenta,
                nombre_cuenta = m.nombre_cuenta,
                activo = m.activo,
                id_tipo_cuenta = m.id_tipo_cuenta
            });
        }

        //obtener por id
        public async Task<Cuenta_ContableDTos?> ObtenerCuenta_Contable(int id)
        {
            // 1. Buscamos en la base de datos usando tu interfaz
            var m = await _cuenta_Contable.ObtenerCuenta_Contable(id);

            // 2. Si no existe, devolvemos nulo
            if (m == null) return null;

            // 3. Si existe, lo convertimos a DTO
            return new Cuenta_ContableDTos
            {
                id_cuenta_contable = m.id_cuenta_contable,
                cod_cuenta = m.cod_cuenta,
                nombre_cuenta = m.nombre_cuenta,
                activo = m.activo,
                id_tipo_cuenta = m.id_tipo_cuenta
            };
        }
        //agregar
        public async Task NuevoCuenta_Contable(Cuenta_ContableDTos cuenta_ContableD)
        {
            var cuenta_con = new Cuenta_Contable
            {
                cod_cuenta = cuenta_ContableD.cod_cuenta,
                nombre_cuenta = cuenta_ContableD.nombre_cuenta,
                id_tipo_cuenta = cuenta_ContableD.id_tipo_cuenta
            };
            await _cuenta_Contable.NuevoCuenta_Contable(cuenta_con);

        }

        //editar

        public async Task EditarCuenta_Contable(Cuenta_ContableDTos ContableDTos)
        {
            var cuenta = new Cuenta_Contable
            {
                id_cuenta_contable = ContableDTos.id_cuenta_contable,
                cod_cuenta = ContableDTos.cod_cuenta,
                nombre_cuenta = ContableDTos.nombre_cuenta,
                activo = ContableDTos.activo,
                id_tipo_cuenta = ContableDTos.id_tipo_cuenta
            };

            await _cuenta_Contable.EditarCuenta_ContableAsync(cuenta);
        }

        //eliminar 

        public async Task EliminarCuenta_contable(int id)
        {
            await _cuenta_Contable.EliminarCuenta_ContableAsync(id);
        }
    }
}
