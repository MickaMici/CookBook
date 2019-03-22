using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CookBook.ViewModels;
using CookBook.Models;

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
            var recipes = _context.Recipes;
            return View(recipes);
        }


        public ActionResult New()
        {
            return View();
        }


    }
}