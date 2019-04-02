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


        //public JsonResult ImageUpload(ProductViewModel model)
        //{

        //    MVCTutorialEntities db = new MVCTutorialEntities();
        //    int imageId = 0;

        //    var file = model.ImageFile;

        //    byte[] imagebyte = null;

        //    if (file != null)
        //    {

        //        file.SaveAs(Server.MapPath("/UploadedImage/" + file.FileName));

        //        BinaryReader reader = new BinaryReader(file.InputStream);

        //        imagebyte = reader.ReadBytes(file.ContentLength);

        //        ImageStore img = new ImageStore();

        //        img.ImageName = file.FileName;
        //        img.ImageByte = imagebyte;
        //        img.ImagePath = "/UploadedImage/" + file.FileName;
        //        img.IsDeleted = false;
        //        db.ImageStores.Add(img);
        //        db.SaveChanges();

        //        imageId = img.ImageId;

        //    }

        //    return Json(imageId, JsonRequestBehavior.AllowGet);

        //}

        public ActionResult Upload()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var path = Path.Combine(Server.MapPath("~/MyImages"));
                        string pathString = System.IO.Path.Combine(path.ToString());
                        var fileName1 = Path.GetFileName(file.FileName);
                        bool isExists = System.IO.Directory.Exists(pathString);
                        if (!isExists) System.IO.Directory.CreateDirectory(pathString);
                        var uploadpath = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(uploadpath);
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }
            if (isSavedSuccessfully)
            {
                return Json(new
                {
                    Message = fName
                });
            }
            else
            {
                return Json(new
                {
                    Message = "Error in saving file"
                });
            }

        }
    }
}