using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CookBook.Models;
namespace CookBook.ViewModels
{

    public class GetRecipesByTypeViewModel
    {
        public IEnumerable<Recipe> Recipes { get; set; }
    }
}