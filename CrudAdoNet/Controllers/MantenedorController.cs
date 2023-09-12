using Microsoft.AspNetCore.Mvc;
using CrudAdoNet.Models;
using crudAdoNet.Datos;

namespace CrudAdoNet.Controllers
{
    public class MantenedorController : Controller
    {
        ContactoDatos _contactoDatos = new ContactoDatos();
        public IActionResult Listar()
        {
            var lista = _contactoDatos.Listar();
            //mostrar una lista de contactos
            return View(lista);
        }
        [HttpGet]
        public IActionResult Guardar() 
        { 
            //para mostrar el formulario
            return View();
        }
        [HttpPost]
        public IActionResult Guardar(ContactoModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            //para obtener los datos del formulario y enviarlo en la base de datos
            bool respuesta = _contactoDatos.GuardarContacto(model);
            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }
            [HttpGet]
            public IActionResult Editar(int IdContacto)
            {
                ContactoModel _contacto = _contactoDatos.ObtenerContacto(IdContacto);    

                return View(_contacto);
            }
            [HttpPost]
            public IActionResult Editar(ContactoModel model)
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                //para obtener los datos del formulario y enviarlo en la base de datos
                bool respuesta = _contactoDatos.EditarContacto(model);
                if (respuesta)
                {
                    return RedirectToAction("Listar");
                }
                else
                {
                    return View();
                }
            }
        [HttpGet]
        public IActionResult Eliminar(int IdContacto) 
        {
            var _contacto = _contactoDatos.ObtenerContacto(IdContacto);
            return View(_contacto);
        }
        [HttpPost]
        public IActionResult Eliminar(ContactoModel model)
        {
            var respuesta = _contactoDatos.EliminarContacto(model.IdContacto);
            if(respuesta) 
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }
    }
}
