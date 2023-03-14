using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using WebApplication1.Servicios;
using WebApplication1.Sesion;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    public class ClienteController : Controller
    {
        List<Cliente> list = new List<Cliente>();
        List<SelectListItem> paises = new List<SelectListItem>();

        private readonly ServicioCliente _servicoCliente;
        private readonly ServicioSingleton _servicioSingleton;
        private readonly ServicioTrasient _servicioTrasient;
        private readonly ServicioScope _servicioScope;


        public ClienteController(ServicioCliente servicioCliente, ServicioSingleton servicioSingleton, ServicioTrasient servicioTrasient, ServicioScope servicioScope)
        {
            _servicoCliente = servicioCliente;
            _servicioSingleton = servicioSingleton;
            _servicioTrasient = servicioTrasient;
            _servicioScope = servicioScope;
            Console.WriteLine("ClientesController Constructor");
        }

        public IActionResult Index()
        {
            return View(listaClientes());
        }
        public List<Cliente> listaClientes()
        {
            List<Cliente> list = SessionHelper.GetObjectFromJson<List<Cliente>>(HttpContext.Session, "clientes");
            if (list == null)
            {
                list = new List<Cliente>();
            }
            return list;
        }
        [Route("Cliente/Api/{identificacion}")]
        public async Task<IActionResult> Get(string identificacion) 
        {

            try
            {
                List<Cliente> listClient = new List<Cliente>();
                Cliente cliente = new Cliente();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5162/api/ClienteApi/listarID/" + identificacion))
                    {

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            cliente = JsonConvert.DeserializeObject<Cliente>(apiResponse);
                        }
                        else
                        {
                            ViewBag.StatusCode = response.StatusCode;
                        }
                    }
                }
                listClient.Add(cliente);
                foreach (Cliente c in listClient)
                {
                    Console.WriteLine(c.Nombres);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return Ok("xd");
        }

        //[Route("Cliente/Delete/{identificacion}")]
        [HttpGet("Cliente/Delete/{identificacion}")]
        public IActionResult Delete(String identificacion)
        {
            if (ModelState.IsValid)
            {
                List<Cliente> list = SessionHelper.GetObjectFromJson<List<Cliente>>(HttpContext.Session, "clientes");
                Console.WriteLine(list.Count);
                if (list == null)
                    list = new List<Cliente>();
                list.RemoveAll(x => x.Identificacion == identificacion);
                Console.WriteLine(list.Count);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "clientes", list);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EliminarPage(Cliente cliente)
        {
            if (list == null)
                list = new List<Cliente>();

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Identificacion == cliente.Identificacion)
                {
                    list.Remove(list[i]);

                }
            }

            return RedirectToAction("ClientesAc");
        }
       

        [HttpPost]
        public IActionResult Store(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   _servicoCliente.GuardarCliente(cliente);
                    List<Cliente> list = SessionHelper.GetObjectFromJson<List<Cliente>>(HttpContext.Session, "clientes");
                    if (list == null)
                        list = new List<Cliente>();
                    list.Add(cliente);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "clientes", list);
                        return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Pais = SelectPais();
                    TempData["errorCedula"] = ex.Message;
                    return View("Create", cliente);
                }
                //TempData["errorCedula"] = ValidadorCliente.ValidaCliente(cliente);
                //return View("Create", cliente);

            }
            else
            {
                ViewBag.Pais = SelectPais();
                return View("Create", cliente);
            }
        }
        //[Authorize(Roles = "Admin")]
        public IActionResult Create()
        {

            ViewBag.Pais = SelectPais();
            return View();
        }
        [HttpGet("Cliente/BuscarPorCedulaConcatenado")]
        public IActionResult BuscarPorCedulaConcatenado(string identificacion, string edad)
        {
            List<Cliente> list = SessionHelper.GetObjectFromJson<List<Cliente>>(HttpContext.Session, "clientes");
            Cliente? cliente = list.Find(x => x.Identificacion == identificacion);
            Console.WriteLine(edad);
            return View(cliente);
        }
        [HttpGet("Cliente/BuscarPorCedulaParams/{identificacion}/{edad}")]
        public IActionResult BuscarPorCedulaParams(string identificacion, string edad)
        {
            List<Cliente> list = SessionHelper.GetObjectFromJson<List<Cliente>>(HttpContext.Session, "clientes");
            Cliente? cliente = list.Find(x => x.Identificacion == identificacion);
            Console.WriteLine(edad);
            return View(cliente);
        }
        [HttpGet]
        public IActionResult BuscarPorCedula()
        {
            return View();
        }
        [HttpPost]
        [Route(("Cliente/Edit/{cedula}"))]
        public IActionResult BuscarPorCedulaPost(string identificacion)
        {
            Console.WriteLine(identificacion);
            return Ok(identificacion);
            //List<Cliente> list = SessionHelper.GetObjectFromJson<List<Cliente>>(HttpContext.Session, "clientes");
            //Cliente? cliente = list.Find(x => x.Identificacion == identificacion);
            //return View(cliente);
        }
        [Route("Cliente/Edit/{identificacion}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string identificacion)
        {
            List<Cliente> list = SessionHelper.GetObjectFromJson<List<Cliente>>(HttpContext.Session, "clientes");
            Cliente? cliente = list.Find(x => x.Identificacion == identificacion);
            list.RemoveAll(x => x.Identificacion == identificacion);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "clientes", list);
            if (cliente == null)
            {
                return View("Error");
            }
            ViewBag.Pais = SelectPais();
            return View(cliente);
        }


        [HttpPost]
        public IActionResult Update(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                List<Cliente> list = SessionHelper.GetObjectFromJson<List<Cliente>>(HttpContext.Session, "clientes");
                if (list == null)
                    list = new List<Cliente>();
                list.Add(cliente);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "clientes", list);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Pais = SelectPais();
                return View("Edit", cliente);

            }
        }

        public List<SelectListItem> SelectPais()
        {
            paises = new List<SelectListItem>();
            paises.Add(new SelectListItem { Text = "Ecuador", Value = "Ecuador" });
            paises.Add(new SelectListItem { Text = "Colombia", Value = "Colombia" });
            paises.Add(new SelectListItem { Text = "EEUU", Value = "EEUU", Selected = true });
            paises.Add(new SelectListItem { Text = "France", Value = "France" });
            return paises;
        }
        [AcceptVerbs("GET", "POST")]
        public void VerifyCedula(Cliente cliente)
        {

        }
        //public ViewResult Edit()
        //{
        //    ViewBag.Paises = new SelectList(repository.Ciudades)
        //        .Select(c => c.Pais).Distinc());
        //    return View("Create", repository.Cities.First());
        //}
    }
}