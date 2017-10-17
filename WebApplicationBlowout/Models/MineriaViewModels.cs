using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationBlowout.Models
{
    public class ReglaAsociacion
    {

        [Display(Name = "Codigo premisa")]
        public string NumeroPremisa { get; set; }

        [Display(Name = "Premisa")]
        public string Premisa { get; set; }

        [Display(Name = "Conclusión")]
        public string Conclusion { get; set; }

        [Display(Name = "Confianza")]
        public string Confianza { get; set; }

    }

    public class Reserva
    {

        [Required]
        [Display(Name = "Nombre Usuario")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Cedula")]
        public string Cedula { get; set; }
        [Required]
        [Display(Name = "Numero de personas")]
        public string NumeroPersonas { get; set; }
        [Required]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        
        [Display(Name = "Fecha")]
        public string Fecha1 { get; set; }

    }


    public class Promocion
    {
        [Required]
        [Display(Name = "Nombre Promoción")]
        public string NombreProducto { get; set; }
        [Required]
        [Display(Name = "Precio")]
        public string Precio { get; set; }
        [Required]
        [Display(Name = "Fecha Inicio")]
        [DataType(DataType.Date)]
        public string FechaI { get; set; }
        [Required]
        [Display(Name = "Fecha Culminación")]
        [DataType(DataType.Date)]
        public string FechaF { get; set; }
    }



    public class segmentacion
    {
        
        [Display(Name = "Cluster")]
        public string Cluster { get; set; }
        
        [Display(Name = "Cantidad clientes")]
        public string Clientes { get; set; }
        
        [Display(Name = "Total compras")]
        public string Compras { get; set; }
        
        [Display(Name = "Cantidad de facturas")]
        public string Facturas { get; set; }

         [Display(Name = "Promedio compras")]
        public string PromCompras { get; set; }

         [Display(Name = "Promedio edad")]
        public string PromEdad { get; set; }





    }


    public class Compra {

        [Required]
        [Display(Name = "Código Cliente")]
        public string Codigo { get; set; }


    }

    public class ProductosCompra
    {

        
        [Display(Name = "Codigo")]
        public string Codigo { get; set; }
        [Display(Name = "Producto")]
        public string Producto { get; set; }


    }

}

    