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
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _dbContext;

        public CustomersController()
        {
            _dbContext = new ApplicationDbContext();
        }

        //GET /api/customers
        public IHttpActionResult GetCustomers(string query = null)
        {
            IQueryable<Customer> customers = _dbContext.Customers.Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
                customers = customers.Where(c => c.Name.Contains(query));


            return Ok(customers.ToList().Select(Mapper.Map<Customer, CustomerDto>));
        }

        //GET /api/customers/Id
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = _dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            CustomerDto customerDto = Mapper.Map<Customer, CustomerDto>(customer);

            return Ok(customerDto);
        }

        //POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Customer customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        //PUT /api/customers/Id
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Customer customerFromDb = _dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customerFromDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //customerDto.Id = customerFromDb.Id;

            Mapper.Map(customerDto, customerFromDb);

            _dbContext.SaveChanges();
        }

        //DELETE /api/customer/Id
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            Customer customerFromDb = _dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customerFromDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _dbContext.Customers.Remove(customerFromDb);
            _dbContext.SaveChanges();
        }
    }
}
