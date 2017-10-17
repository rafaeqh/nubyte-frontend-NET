using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationBlowout.Models;

namespace WebApplicationBlowout.Controllers
{
    public class OperadorController : Controller
    {
        private WebServiceBlowout.WebServicePruebaClient serviciosWeb = new WebServiceBlowout.WebServicePruebaClient();
        // GET: Operador
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CrearReserva(Reserva model) {

            if (ModelState.IsValid)
            {
                //var user = new ApplicationUser { UserName = model.nombre, Email = model.Email };

                var result = serviciosWeb.crearReserva(model.Nombre, model.Cedula, model.NumeroPersonas, model.Fecha.ToShortDateString(), Session["NIT"].ToString());
                if (result)
                {
                    
                    return RedirectToAction("ListaReservas", "Operador");
                }
                else
                {

                    ModelState.AddModelError("", "Error en la creación de la reserva");
                    return View(model);
                }




            }
            return View(model);



        }


        public ActionResult ListaReservas()
        {
            var lista = new List<Reserva>();
            String[] listaWS;

            listaWS = serviciosWeb.listaReservas(Session["NIT"].ToString());
            Char delimiter = ' ';

            for (int i = 0; i < listaWS.Length; i++)
            {

                var ReservaAux = new Reserva();
                String[] substrings = listaWS[i].Split(delimiter);
                ReservaAux.Cedula = substrings[0];
                ReservaAux.Nombre = substrings[1]+" "+substrings[2];
                ReservaAux.NumeroPersonas = substrings[3];
                ReservaAux.Fecha1 = substrings[4];

                lista.Add(ReservaAux);

            }

            return View(lista);

        }


        public ActionResult ListaPromociones()
        {
            var lista = new List<Promocion>();
            String[] listaWS;

            listaWS = serviciosWeb.listarPromos(Session["NIT"].ToString());
            Char delimiter = '/';

            for (int i = 0; i < listaWS.Length; i++)
            {

                var promocionAux = new Promocion();
                String[] substrings = listaWS[i].Split(delimiter);
                promocionAux.Precio = substrings[1];
                promocionAux.NombreProducto = substrings[0];
                promocionAux.FechaI = substrings[2];
                promocionAux.FechaF = substrings[3];

                lista.Add(promocionAux);

            }

            return View(lista);

        }


        public ActionResult Compra(Compra model)
        {

            String[] listaProd;

            listaProd = serviciosWeb.listaProductosEst("1");
            var lista = new List<ProductosCompra>();
            Char delimiter = '/';
            for (int i = 0; i < listaProd.Length; i++)
            {

                var Producto1 = new ProductosCompra();
                String[] substrings = listaProd[i].Split(delimiter);
                Producto1.Codigo = substrings[0];
                Producto1.Producto = substrings[1];


                lista.Add(Producto1);

            }

            SelectList listaPro = new SelectList(lista, "Codigo", "Producto");
            ViewBag.Productos = listaPro;

            if (ModelState.IsValid)
            {
                //var user = new ApplicationUser { UserName = model.nombre, Email = model.Email };

                var result = serviciosWeb.asignarPuntos(model.Codigo,"8");
                if (result != null)
                {
                    ModelState.AddModelError("", "Se registró correctamente la compra y carga de puntos de fidelidad al Cliente"); 
                    return View(model);
                }
                else
                {

                    ModelState.AddModelError("", "Error en el registro de la compra");
                    return View(model);
                }




            }
            return View(model);



        }
    }
}