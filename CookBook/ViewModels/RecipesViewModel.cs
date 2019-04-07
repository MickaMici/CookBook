using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CookBook.Models;
using System.Text;

namespace CookBook.ViewModels
{
    public class RecipesViewModel
    {
        public Recipe Recipe { get; set; }
        public List<RecipeType> RecipeTypes { get; set; }
        public List<IngredientMeasure> IngredientMeasures { get; set; }
        public IEnumerable<HttpPostedFileBase> ImageFiles { get; set; }

    }
}