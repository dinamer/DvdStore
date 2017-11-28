using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DvdStore.Models;
using DvdStore.Dtos;
using AutoMapper;
using System.Data.Entity;


namespace DvdStore.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _dbContext;

        public MoviesController()
        {
            _dbContext = new ApplicationDbContext();
        }


        //GET /api/movies
        public IHttpActionResult GetMovies(string query = null)
        {
            IQueryable<Movie> movies = _dbContext.Movies.Include(m => m.Genre).Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
            {
                movies = movies.Where(m => m.Name.Contains(query));
            }

            return Ok(movies.ToList().Select(Mapper.Map<Movie, MovieDto>));
        }

        //GET /api/movies/Id
        public IHttpActionResult GetMovie(int id)
        {
            Movie movie = _dbContext.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //POST /api/movies

        [HttpPost]
        [Authorize(Roles = RoleName.MovieManager)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Movie movie = Mapper.Map<MovieDto, Movie>(movieDto);
            movie.DateAdded = DateTime.Now;

            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        //PUT /api/movies/Id
        [HttpPut]
        [Authorize(Roles = RoleName.MovieManager)]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Movie movieFromDb = _dbContext.Movies.SingleOrDefault(m => m.Id == id);

            if (movieFromDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //movieDto.Id = movieFromDb.Id;

            Mapper.Map(movieDto, movieFromDb);

            _dbContext.SaveChanges();
        }

        //DELETE /api/movies/Id
        [HttpDelete]
        [Authorize(Roles = RoleName.MovieManager)]
        public void DeleteMovie(int id)
        {
            Movie movieToDelete = _dbContext.Movies.SingleOrDefault(m => m.Id == id);

            if (movieToDelete == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _dbContext.Movies.Remove(movieToDelete);
            _dbContext.SaveChanges();

        }
    }
}
