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
        public List<Recipe> Recipes { get; set; }
        public List<RecipeType> RecipeTypes { get; set; }
        public List<HttpPostedFileBase> ImageFiles { get; set; }
        public List<string> SplitedIngredients { get; set; }
        public List<string> ImagesPaths { get; set; }
        public List<bool> ImagesChanged { get; set; }
        public List<bool> ImagesRemoved { get; set; }

    }
}