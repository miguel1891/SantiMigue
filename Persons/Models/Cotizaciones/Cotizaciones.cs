using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Persons.Models
{
    public class Cotizaciones
    {
        [Key]
        public int IDCotizacion { get; set; }

        public int IDMoneda { get; set; } 
        public MonedasData Moneda { get; set; }
        //los 2 campos de arriba referencian a la tabla Monedas; y hacen que se pueda elegir el dato desde un combo en 
        //las vistas de Create y Edit

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public Double Cotizacion { get; set; } 

    }
    //la clave se define en el archivo ApplicationDbContext.cs
    //la clave en esta tabla es el par IDMoneda-Fecha
}
