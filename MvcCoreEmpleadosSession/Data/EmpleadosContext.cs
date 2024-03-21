using Microsoft.EntityFrameworkCore;
using MvcCoreEmpleadosSession.Models;
using System.Collections.Generic;

namespace MvcCoreEmpleadosSession.Data
{
    public class EmpleadosContext : DbContext

    {

        public EmpleadosContext(DbContextOptions<EmpleadosContext> options)
        : base(options)

        { }
        public DbSet<Empleado> Empleados { get; set; }

    }
}
