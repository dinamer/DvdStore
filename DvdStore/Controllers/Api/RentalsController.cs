using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DvdStore.Dtos;
using DvdStore.Models;

namespace DvdStore.Controllers.Api
{
    public class RentalsController : ApiController
    {
        private ApplicationDbContext _dbContext;

        public RentalsController()
        {
            _dbContext = new ApplicationDbContext();
        }

        //Create new rental
        [HttpPost]
        public IHttpActionResult Create(RentalsDto createRental)
        {
            if (createRental.MovieIds.Count() == 0)
                return BadRequest("No movies selected.");

            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == createRental.CustomerId);
            if (customer == null)
                return BadRequest("Customer invalid.");

            var movies = _dbContext.Movies.Where(m => createRental.MovieIds.Contains(m.Id)).ToList();
            if (movies.Count() != createRental.MovieIds.Count())
                return BadRequest("One or more movies are invalid.");

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("No movies available");

                movie.NumberAvailable--;

                Rental rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };


                _dbContext.Rentals.Add(rental);
            }

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
