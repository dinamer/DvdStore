using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DvdStore.Models;
using DvdStore.ViewModels;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;


namespace DvdStore.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _dbContext;

        public MoviesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }

        // GET: Movies
        public ViewResult Index()
        {
            if (User.IsInRole("MovieManager"))
                return View("Index");

            else
                return View("ReadOnlyIndex");
            //IList<Movie> movies = _dbContext.Movies.Include(m => m.Genre).ToList();
            //return View(movies);
        
        }

        public ViewResult Details(int id)
        {

            var movie = _dbContext.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);


            return View(movie);

        }

        //Add new movie
        [Authorize(Roles = RoleName.MovieManager)]
        [HttpGet]
        public ActionResult Create()
        {
            MovieFormViewModel model = new MovieFormViewModel
            {
                Genres = _dbContext.Genres.ToList(),                
            };

            return View("MovieForm", model);
        }

        //Edit a movie
        [Authorize(Roles = RoleName.MovieManager)]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Movie movieToEdit = _dbContext.Movies.SingleOrDefault(m => m.Id == id);

            if (movieToEdit == null)
                return HttpNotFound();

            MovieFormViewModel model = new MovieFormViewModel(movieToEdit)
            {
                Genres = _dbContext.Genres.ToList(),
            };

                return View("MovieForm", model);
            
        }

        //Save new/edited movie to the database
        [HttpPost]
        [Authorize(Roles = RoleName.MovieManager)]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                MovieFormViewModel model = new MovieFormViewModel(movie)
                {
                    Genres = _dbContext.Genres.ToList(),
                };

            return View("MovieForm", model);
        }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _dbContext.Movies.Add(movie);
            }
            else
            {
                Movie movieToEdit = _dbContext.Movies.SingleOrDefault(m => m.Id == movie.Id);

                movieToEdit.Name = movie.Name;
                movieToEdit.ReleaseDate = movie.ReleaseDate;
                movieToEdit.GenreId = movie.GenreId;
                movieToEdit.NumberInStock = movie.NumberInStock;
            }

            _dbContext.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}