using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Registro_de_mascotas.Models
{
    [Table("t_mascota")]
    public class Mascota
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
         public string? Name {get;set;}
        public string? Raza {get;set;}
        public string? Color {get;set;}
        public DateTime FechaNacimiento {get;set;}
    }
}