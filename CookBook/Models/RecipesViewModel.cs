using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CookBook.Models
{
    public class RecipesViewModel
    {
        public Recipe Recipes { get; set; }
        public RecipeType RecipeTypes { get; set; }
    }
}