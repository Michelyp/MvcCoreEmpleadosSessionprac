using Microsoft.AspNetCore.Mvc;
using MvcCoreEmpleadosSession.Extensions;
using MvcCoreEmpleadosSession.Models;
using MvcCoreEmpleadosSession.Repositories;

namespace MvcCoreEmpleadosSession.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repo;
        public EmpleadosController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> SessionSalarios(int? salario)
        {
            if(salario != null)
            {
                int sumaSalarial = 0;
                //preguntamos si tiene la suma en la session
                if(HttpContext.Session.GetString("SUMASALARIAL")!=null)
                {
                    //recuperamos la suma salarial
                    sumaSalarial = int.Parse(HttpContext.Session.GetString("SUMASALARIAL"));
                }
                //realizamos la suma salarial
                sumaSalarial += salario.Value;
                //almacenamos la nueva suma salarial en la session
                HttpContext.Session.SetString("SUMASALARIAL", sumaSalarial.ToString());
                ViewData["MENSAJE"]= "Salario almacenado: "+salario.Value;
            }
            List<Empleado> empleados = await this.repo.GetEmpleadosAsync();
            
            return View(empleados);
        }
        //public async Task<IActionResult> SessionEmpleados(int idEmpleado?)
        //{
        //    if(idEmpleado != null)
        //    {
        //        Empleado empleado = await this.repo.FindEmpleadoAsync(idEmpleado);
        //        //En la session almacenaremos un conjunto de empleados
        //        List<Empleado> empleadosList;
        //        if (HttpContext.Session.GetObject<List<Empleado>>("EMPLEADOS") != null)
        //        {
        //            //sI YA TENEMOS LO RECUPERAMOS DE SESSION
        //            empleadosList = HttpContext.Session.GetObject<List<Empleado>>("EMPLEADOS");
        //        }else
        //        {
        //            empleadosList = new List<Empleado>();
        //        }
        //        empleadosList.Add(empleado);
        //        HttpContext.Session.SetObject("Empleados", empleadosList);
        //        ViewData["MENSAJE"] = "Empleado " + empleado.Apellido
        //            + " almacenado correctamente";
        //    }
        //    List<Empleado> empleados = await this.repo.GetEmpleadosAsync();
        //    return View(empleados);
        //}
        //public IActionResult EmpleadosAlmacenados()

        //{

        //    return View();

        //}
        public async Task<IActionResult> SessionEmpleadosOk(int? idempleado)
        {
            if(idempleado!=null) {
                //Almacenamos lo minimo del objeto, una coleccion int
                List<int> idsEmpleados;
                if(HttpContext.Session.GetString("IDSEMPLEADOS") == null)
                {
                    idsEmpleados = new List<int>();
                }
                else
                {
                    idsEmpleados = HttpContext.Session.GetObject<List<int>>("IDSEMPLEADOS");
                }
                idsEmpleados.Add(idempleado.Value);
                HttpContext.Session.SetObject("IDSEMPLEADOS", idsEmpleados);
                ViewData["MENSAJE"] = "Empleados almacenados: "
                   + idsEmpleados.Count;
            }
            List<Empleado> empleados = await this.repo.GetEmpleadosAsync();
            return View(empleados);
        }

        public async Task<IActionResult> EmpleadosAlmacenadosOk()
        {
            List<int> idsEmpleados = HttpContext.Session.GetObject<List<int>>("IDSEMPLEADOS");
            if(idsEmpleados == null)
            {
                ViewData["MENSAJE"] = "No existen empleados almacenados en Session";
                return View();
            }else
            {
                List<Empleado> empleados = await

                   this.repo.GetEmpleadosSessionAsync(idsEmpleados);

                return View(empleados);
            }
        }
        public IActionResult SumaSalarios()

        {

            return View();

        }
    }
}
