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

        [Required]
        [Display (Name= "Naziv recepta")]
        public string Name { get; set; }

        public RecipeType RecipeType { get; set; }
        public int RecipeTypeId { get; set; }
        
        
        public double? AverageDifficulty { get; set; }
        public double? AverageRating { get; set; }

    }
}