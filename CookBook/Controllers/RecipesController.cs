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

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RecipesViewModel viewModel)
        {

            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            foreach (var file in viewModel.ImageFiles)
            {
                if (file == null || file.ContentLength == 0)
                {
                    ModelState.AddModelError("ImageFiles", "This field is required");
                }
                else if (!validImageTypes.Contains(file.ContentType))
                {
                    ModelState.AddModelError("ImageFiles", "Please choose either a GIF, JPG or PNG image.");
                }
            }



            if (ModelState.IsValid)
            {
                _context.Recipes.Add(viewModel.Recipe);
                _context.SaveChanges();

                var newRecipeId = viewModel.Recipe.Id;


                var uploadDir = "~/Images/" + newRecipeId.ToString();
                var fileIndex = 0;
                Directory.CreateDirectory(Server.MapPath(uploadDir));
                foreach (var file in viewModel.ImageFiles)
                {
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), fileIndex.ToString());
                    var imageUrl = Path.Combine(uploadDir, fileIndex.ToString());
                    file.SaveAs(imagePath);
                    var image = new Image
                    {
                        RecipeId = newRecipeId,
                        Path=imageUrl
                    };

                    _context.Images.Add(image);
                    _context.SaveChanges();                

                    fileIndex++;
                }

                return RedirectToAction("Details", "Recipes", new { id = newRecipeId });
            }

            return View();
            

        }

        


       

    }
}