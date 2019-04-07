using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CookBook.Models
{
    public class Image
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Path { get; set; }
    }
}