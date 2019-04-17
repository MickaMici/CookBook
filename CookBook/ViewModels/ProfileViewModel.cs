using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CookBook.Models;


namespace CookBook.ViewModels
{
    public class ProfileViewModel
    {
        public List<Recipe> Recipes { get; set; }
        public ApplicationUser User { get; set; }
        public List<string> ImagesPaths { get; set; }
    }
}