using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationBlowout.Models;

namespace WebApplicationBlowout.Controllers
{
    public class AdminController : Controller
    {
        private WebServiceBlowout.WebServicePruebaClient serviciosWeb = new WebServiceBlowout.WebServicePruebaClient();
        // GET: Admin
        //comentario nuevo
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = new ApplicationUser { UserName = model.nombre, Email = model.Email };

                Boolean result = serviciosWeb.crearCuenta(model.cedula, model.nombre, model.Email, model.Password, "2", Session["NIT"].ToString());
                if (result)
                {

                    return RedirectToAction("ListaOperadores", "Admin");
                }
                else
                {

                    ModelState.AddModelError("", "Error en la creación del usuario");
                    return View(model);
                }




            }
            return View(model);
        }


        public ActionResult ListaOperadores()
        {
            var lista = new List<OperadorViewModel>();
            String[] listaWS;

            listaWS = serviciosWeb.listaFuncionarios();
            Char delimiter = ' ';

            for (int i = 0; i < listaWS.Length; i++)
            {

                var operador = new OperadorViewModel();
                String[] substrings = listaWS[i].Split(delimiter);
                operador.nombre = substrings[0] + " " + substrings[1];
                operador.Email = substrings[2];
                operador.cedula = substrings[3];

                lista.Add(operador);

            }

            return View(lista);

        }

        public ActionResult ReglasDeAsociacion()
        {
            var lista = new List<ReglaAsociacion>();
            String[] listaWS;

            listaWS = serviciosWeb.reglasAsociacion();

            Char delimiter = '/';
            for (int i = 0; i < listaWS.Length; i++)
            {

                var regla = new ReglaAsociacion();
                String[] substrings = listaWS[i].Split(delimiter);
                regla.NumeroPremisa = (i + 1).ToString();
                regla.Premisa = substrings[0];
                regla.Conclusion = substrings[1];
                regla.Confianza = substrings[2];

                lista.Add(regla);

            }
            Session["Producto1"] = lista[0].Premisa.ToString();
            Session["Producto2"] = lista[0].Conclusion.ToString();
            return View(lista);

        }


        public ActionResult CerrarSesion()
        {

            Session["login"] = "0";

            return RedirectToAction("Login", "Account");

        }

        public ActionResult Segmentacion()
        {
            var lista = new List<segmentacion>();
            String[] listaWS;

            /*listaWS = serviciosWeb.pedirInfoSegmentacion();
           

            Char delimiter =' ';
            for (int i = 0; i < listaWS.Length; i++)
            {

                var segmen = new segmentacion();
                String[] substrings = listaWS[i].Split(delimiter);
                segmen.Cluster = substrings[1];
                segmen.Clientes = substrings[3];
                segmen.Compras = substrings[4];
                segmen.Facturas = substrings[5];
                segmen.PromCompras = substrings[7];
                segmen.PromEdad = substrings[6];
              

                lista.Add(segmen);

            }
            */
            return View(lista);

        }

        public ActionResult CrearPromocion(Promocion promo) {

            
              String[] listaProd;

            listaProd = serviciosWeb.listaProductosEst("1");
             var lista = new List<ProductosCompra>();
            Char delimiter ='/';
            for (int i = 0; i < listaProd.Length; i++)
            {

                var Producto1 = new ProductosCompra();
                String[] substrings = listaProd[i].Split(delimiter);
                Producto1.Codigo = substrings[0];
                Producto1.Producto = substrings[1];
              

                lista.Add(Producto1);

            }
            
       


            SelectList listaPro = new SelectList(lista, "Codigo", "Producto");
            ViewBag.CrearPromocion = listaPro;

       
            if (ModelState.IsValid)
            {

                Boolean result = serviciosWeb.crearPromo(promo.NombreProducto, promo.Precio, promo.FechaI, promo.FechaF, Session["NIT"].ToString());
                if (result)
                {

                    return RedirectToAction("ListaPromociones", "Admin");
                }
                else
                {

                    ModelState.AddModelError("", "Error en la creación de la Promoción");
                    return View(promo);
                }

            }
            else {

                return View(promo);

            }
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
                promocionAux.NombreProducto = substrings[0];
                promocionAux.Precio = substrings[1];
                promocionAux.FechaI = substrings[2];
                promocionAux.FechaF = substrings[3];

                lista.Add(promocionAux);

            }

            return View(lista);

        }

        public ActionResult CrearPromocionRegla(Promocion promo)
        {

            if (ModelState.IsValid)
            {

                Boolean result = serviciosWeb.crearPromo(promo.NombreProducto, promo.Precio, promo.FechaI, promo.FechaF, Session["NIT"].ToString());
                if (result)
                {

                    return RedirectToAction("ListaPromociones", "Admin");
                }
                else
                {

                    ModelState.AddModelError("", "Error en la creación de la Promoción");
                    return View(promo);
                }

            }
            else
            {

                return View(promo);

            }
        }

        public ActionResult ListaProductos()
        {
            var lista = new List<ProductosCompra>();
            var Producto1 = new ProductosCompra();
            Producto1.Codigo = "CP_04";
            Producto1.Producto = Session["Producto1"].ToString();
            var Producto2 = new ProductosCompra();
            Producto2.Codigo = "CP_08";
            Producto2.Producto = Session["Producto2"].ToString();
            lista.Add(Producto1);
            lista.Add(Producto2);

            return View(lista);

        }

        public ActionResult SelectListaProductos()
        {
            var lista = new List<ProductosCompra>();
            var Producto1 = new ProductosCompra();
            Producto1.Codigo = "CP_04";
            Producto1.Producto = "Vino";
            var Producto2 = new ProductosCompra();
            Producto2.Codigo = "CP_08";
            Producto2.Producto = "Cerveza";
            lista.Add(Producto1);
            lista.Add(Producto2);

            SelectList listaPro = new SelectList(lista, "Codigo", "Producto");
            ViewBag.CrearPromocion = listaPro;

            return View();

        }

    }

}