using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CookBook.ViewModels;
using CookBook.Models;
using System.Text;
using System.IO;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CookBook.Controllers
{
    public class RecipesController : Controller
    {
        private ApplicationDbContext _context;
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public RecipesController()
        {
            _context = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this._context));

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


        [HttpGet]
        public ActionResult Details(int id)
        {
           
            
            var recipeTypes = _context.RecipeTypes.ToList();
            var ingredientMeasures = _context.IngredientMeasures.ToList();

            var viewModel = new RecipesViewModel
            {
                Recipe = _context.Recipes.Include(r => r.RecipeType).Include(r=>r.User).SingleOrDefault(r => r.Id == id),
                RecipeTypes = recipeTypes,
                IngredientMeasures = ingredientMeasures

                
            };

           
            
            var ingredients = viewModel.Recipe.Ingredients;
            viewModel.SplitedIngredients = ingredients.Split('|').ToList();

            var imagesPaths = (from Image in _context.Images
                               where Image.RecipeId == id
                               select Image.Path).ToList();
            viewModel.ImagesPaths = imagesPaths;
            return View(viewModel);
        }


        [Authorize]
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

                var user = UserManager.FindById(User.Identity.GetUserId());
                viewModel.Recipe.User = user;
                viewModel.Recipe.Date = DateTime.Now;
                _context.Recipes.Add(viewModel.Recipe);
                _context.SaveChanges();

                var newRecipeId = viewModel.Recipe.Id;


                var uploadDir = "~/Images/" + newRecipeId.ToString();
                var fileIndex = 0;
                Directory.CreateDirectory(Server.MapPath(uploadDir));
                foreach (var file in viewModel.ImageFiles)
                {
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), fileIndex.ToString() + Path.GetExtension(file.FileName));
                    var imageUrl = Path.Combine(uploadDir, fileIndex.ToString() + Path.GetExtension(file.FileName));
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
            else
            {
                var recipeTypes = _context.RecipeTypes.ToList();
                var ingredientMeasures = _context.IngredientMeasures.ToList();
                var viewModel1 = new RecipesViewModel
                {
                    RecipeTypes = recipeTypes,
                    IngredientMeasures = ingredientMeasures

                };
                return View("New",viewModel1);

            }           

        }

        public ActionResult GetRecipesByType(int id)
        {
            var recipes = (from Recipe in _context.Recipes
                               where Recipe.RecipeTypeId == id
                               select Recipe).ToList();
            
            var viewModel = new GetRecipesByTypeViewModel
            {
                Recipes = recipes
            };
            return View(viewModel);
        }

        [ChildActionOnly]
        public ActionResult RecipeTypes()
        {
            var model = _context.RecipeTypes.ToList();
            return PartialView("_RecipeTypesPartial", model);
        }

        


       

    }
}