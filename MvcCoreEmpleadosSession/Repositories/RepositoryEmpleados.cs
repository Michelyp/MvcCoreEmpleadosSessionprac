﻿using Microsoft.EntityFrameworkCore;
using MvcCoreEmpleadosSession.Data;
using MvcCoreEmpleadosSession.Models;

namespace MvcCoreEmpleadosSession.Repositories
{
    public class RepositoryEmpleados
    {
        private EmpleadosContext context;
        public RepositoryEmpleados (EmpleadosContext context)
        {
            this.context = context;
        }
        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            var empleados = await this.context.Empleados.ToListAsync();
            return empleados;
        }
        public async Task<Empleado> FindEmpleadoAsync(int idEmpleado)
        {
            return await this.context.Empleados.FirstOrDefaultAsync(z => z.IdEmpleado == idEmpleado);
        }
        public async Task<List<Empleado>> GetEmpleadosSessionAsync(List<int> ids)
        {
            var consulta = from datos in this.context.Empleados where ids.Contains(datos.IdEmpleado) select datos;
            if(consulta.Count()==0)
            {
                return null;
            }
            else
            {
                return await  consulta.ToListAsync();
            }
        }

    }
}
