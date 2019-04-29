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
           
            var recipes = _context.Recipes.ToList();
            List<string> imagePaths = new List<string>();
            foreach (var item in recipes)
            {
                imagePaths.Add((from Image in _context.Images where Image.RecipeId == item.Id && Image.Path != "" select Image.Path).First());
            }

            var model = new ProfileViewModel()
            {
                Recipes = recipes,
                ImagesPaths = imagePaths
            };
            if (User.IsInRole("Admin"))
            {
                return View("IndexForAdmin",model);
            }
            else
            {
                return View("IndexForUser",model);

            }
        }


        [HttpGet]
        public ActionResult Details(int id)
        {
           
            
            var recipeTypes = _context.RecipeTypes.ToList();

            var viewModel = new RecipesViewModel
            {
                Recipe = _context.Recipes.Include(r => r.RecipeType).Include(r=>r.User).SingleOrDefault(r => r.Id == id),
                RecipeTypes = recipeTypes,

                
            };
            
            var ingredients = viewModel.Recipe.Ingredients;
            viewModel.SplitedIngredients = ingredients.Split('|').ToList();

            var imagesPaths = (from Image in _context.Images
                               where Image.RecipeId == id
                               select Image.Path).ToList();
            viewModel.ImagesPaths = imagesPaths;
            return View(viewModel);
        }


        //akcija za update recepta
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var recipeTypes = _context.RecipeTypes.ToList();

            var viewModel = new RecipesViewModel
            {
                Recipe = _context.Recipes.Include(r => r.RecipeType).Include(r => r.User).SingleOrDefault(r => r.Id == id),
                RecipeTypes = recipeTypes
            };
            var ingredients = viewModel.Recipe.Ingredients;
            viewModel.SplitedIngredients = ingredients.Split('|').ToList();
            var imagesPaths = (from Image in _context.Images
                               where Image.RecipeId == id
                               select Image.Path).ToList();
            viewModel.ImagesPaths = imagesPaths;
            return View("New", viewModel);
        }

        [Authorize]
        public ActionResult New()
        {
            var recipeTypes = _context.RecipeTypes.ToList();
            var viewModel = new RecipesViewModel
            {
                Recipe = new Recipe(),
                RecipeTypes = recipeTypes
            };
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecipesViewModel viewModel)
        {
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            for (int i = 0; i < viewModel.ImageFiles.Count(); i++)
            {
                if(viewModel.ImageFiles[i] != null && !viewModel.ImagesChanged[i] )
                {
                    ModelState.AddModelError("ImageFiles", "File not expected!");
                }
                else if (viewModel.ImageFiles[i] == null && viewModel.ImagesChanged[i] && !viewModel.ImagesRemoved[i])
                {
                    ModelState.AddModelError("ImageFiles", "This field is required");
                }
                //provera da slika nije prazna da ima podataka
                else if(viewModel.ImageFiles[i] != null && viewModel.ImageFiles[i].ContentLength == 0)
                {
                    ModelState.AddModelError("ImageFiles", "This field is required");
                }
                else if (viewModel.ImageFiles[i] != null && !validImageTypes.Contains(viewModel.ImageFiles[i].ContentType))
                {
                    ModelState.AddModelError("ImageFiles", "Please choose either a GIF, JPG or PNG image.");
                }
            }
            
            if (ModelState.IsValid)
            {
                if (viewModel.Recipe.Id == 0) // Dodavanje novog recepta
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
                        if(file != null)
                        {
                            var imagePath = Path.Combine(Server.MapPath(uploadDir), fileIndex.ToString() + Path.GetExtension(file.FileName));                            
                            file.SaveAs(imagePath);

                            var imageUrl = Path.Combine(uploadDir, fileIndex.ToString() + Path.GetExtension(file.FileName));
                            var image = new Image
                            {
                                RecipeId = newRecipeId,
                                Path = imageUrl
                            };

                            _context.Images.Add(image);
                            _context.SaveChanges();                            
                        }
                        else
                        {
                            var image = new Image
                            {
                                RecipeId = newRecipeId,
                                Path = ""
                            };

                            _context.Images.Add(image);
                            _context.SaveChanges();
                        }

                        fileIndex++;
                    }

                    return RedirectToAction("Details", "Recipes", new { id = newRecipeId });
                }
                else // Menjanje postojeceg recepta
                {
                    var recipeInDb = _context.Recipes.Single(r => r.Id == viewModel.Recipe.Id);
                    recipeInDb.Name = viewModel.Recipe.Name;
                    recipeInDb.RecipeType = viewModel.Recipe.RecipeType;
                    recipeInDb.Ingredients = viewModel.Recipe.Ingredients;
                    recipeInDb.Procedure = viewModel.Recipe.Procedure;

                    var images = (from Image in _context.Images
                                  where Image.RecipeId == viewModel.Recipe.Id
                                  select Image).ToList();
                    var uploadDir = "~/Images/" + viewModel.Recipe.Id.ToString();
                    for (int i = 0; i < viewModel.ImageFiles.Count(); i++)
                    {
                        if (viewModel.ImageFiles[i] == null)
                        {
                            if(viewModel.ImagesRemoved[i] && images[i].Path != "")
                            {
                                System.IO.File.Delete(Server.MapPath(images[i].Path));
                                images[i].Path = "";
                            }                            
                        }
                        else
                        {
                            if (viewModel.ImagesChanged[i])
                            {
                                if(System.IO.File.Exists(Server.MapPath(images[i].Path)))
                                {
                                    System.IO.File.Delete(Server.MapPath(images[i].Path));
                                    images[i].Path = "";
                                }
                                var imagePath = Path.Combine(Server.MapPath(uploadDir), i.ToString() + Path.GetExtension(viewModel.ImageFiles[i].FileName));
                                var imageUrl = Path.Combine(uploadDir, i.ToString() + Path.GetExtension(viewModel.ImageFiles[i].FileName));

                                viewModel.ImageFiles[i].SaveAs(imagePath);
                                images[i].Path = imageUrl;
                            }
                        }
                    }                    
                    
                    _context.SaveChanges();
                    return RedirectToAction("Details", "Recipes", new { id = viewModel.Recipe.Id });
                }
            }
            else
            {
                var recipeTypes = _context.RecipeTypes.ToList();
                var viewModel1 = new RecipesViewModel
                {
                    RecipeTypes = recipeTypes
                };
                return View("New", viewModel1);

            }
            
        }
      

        //akcija koja se poziva u parcijalnom view-u (_RecipeTypesPartial)
        // iz baze pokupi sve recepte koji imaju konkretan id tj. tip recepta
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

        //akcija koja pravi parcijalni view
        
        public ActionResult RecipeTypes()
        {
            var model = _context.RecipeTypes.ToList();
            return PartialView("_RecipeTypesPartial", model);
        }


        //akcija za brisanje recepta iz baze koja se poziva klikom na dugme u Profile View-u 
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var recipe = _context.Recipes.SingleOrDefault(r => r.Id == id);

           
            if(recipe == null)
            {
                HttpNotFound();
            }
            _context.Recipes.Remove(recipe);
            _context.SaveChanges();

            //brisanje slika iz baze podataka
            var images = (from Image in _context.Images
                          where Image.RecipeId == id
                          select Image).ToList();

            foreach (var item in images)
            {
                _context.Images.Remove(item);
                _context.SaveChanges();
            }

            //brisanje slika iz file system-a
            string mappedPath = Server.MapPath(@"~/Images/" + id);
            Directory.Delete(mappedPath, true);
            return Json(new { result = "Redirect", url = Url.Action("Index", "Recipes") }, JsonRequestBehavior.AllowGet);
        }
    }
}