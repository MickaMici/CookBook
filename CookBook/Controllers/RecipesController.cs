using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CookBook.ViewModels;
using CookBook.Models;
using System.Text;
using System.IO;

namespace CookBook.Controllers
{
    public class RecipesController : Controller
    {
        private ApplicationDbContext _context;

        public RecipesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            //ako zelimo da osim recepata klase pozivamo i neku drugu 
            //moramo koristiti metodu include
            //npr var recipes= _context.recipes.include(r => r.recipesType (druga tabela znaci)
            var recipes = _context.Recipes;
            return View(recipes);
        }

        public ActionResult Details(int id)
        {
            var recipe = _context.Recipes.SingleOrDefault(r => r.Id == id);
            return View(recipe);
        }

        public ActionResult New()
        {
            var recipeTypes = _context.RecipeTypes.ToList();
            var ingredientMeasures = _context.IngredientMeasures.ToList();
            var viewModel = new RecipesViewModel
            {
                RecipeTypes = recipeTypes,
                IngredientMeasures = ingredientMeasures

            };
            return View(viewModel);
        }


        public ActionResult CreateIngredient(RecipesViewModel model)
        {

            return PartialView(model);
        }


        [HttpPost]
        public ActionResult Create(RecipesViewModel viewModel)
        {
            return View();
        }


       

    }
}