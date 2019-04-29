using CookBook.Models;
using CookBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CookBook.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

            public ActionResult Index()
        {
            var recipes = _context.Recipes.Take(10).ToList();
            List<string> imagePaths = new List<string>();
            foreach (var item in recipes)
            {
                imagePaths.Add((from Image in _context.Images where Image.RecipeId == item.Id && Image.Path != "" select Image.Path).First());
            }
            var model = new RecipesViewModel
            {
                Recipes = recipes,
                ImagesPaths=imagePaths
            };
            
            return View(model);
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}