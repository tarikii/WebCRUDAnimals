using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAnimals.Models
{
    public class Animal
    {
        public int IdAnimal { get; set; }

        [Required]
        [Display(Name = "Nombre del Animal")]
        public string NombreAnimal { get; set; }

        [Required]
        [Display(Name = "Raza del Animal")]
        public string Raza { get; set; }

        [Required]
        [Display(Name = "Tipo de Animal")]
        public int RIdTipoAnimal { get; set; }

        [Required]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        public Animal(int id, string name)
        {
            IdAnimal = id;
            NombreAnimal = name;
        }

        public Animal()
        {

        }
    }
}