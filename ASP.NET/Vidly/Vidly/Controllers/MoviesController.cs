using Microsoft.AspNetCore.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        //public IActionResult Index()
        //{
        //    return View();
        //}
        //This will get call when we do movie/index

        //We want to create movie/Random
        public IActionResult Random()
        {
            var movie = new Movie() {Name = "Shreak!"};

            return View(movie);
        }
    }
}
