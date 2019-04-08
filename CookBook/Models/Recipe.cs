using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Morate uneti ime recepta")]
        [StringLength(30)]
        [Display (Name= "Naziv recepta")]
        public string Name { get; set; }

        public RecipeType RecipeType { get; set; }

        [Required(ErrorMessage ="Morate izabrati kategoriju")]
        [Display(Name = "Kategorija")]
        public int RecipeTypeId { get; set; }

        [Required(ErrorMessage ="Morate uneti sastojke")]
        [StringLength(1000)]
        public string Ingredients { get; set; }


        public double? AverageDifficulty { get; set; }
        public double? AverageRating { get; set; }

        [Required(ErrorMessage ="Morate upisati postupak")]
        [StringLength(5000)]
        public string Procedure { get; set; }
    }
}