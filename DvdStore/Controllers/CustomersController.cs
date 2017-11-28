using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DvdStore.Models;
using DvdStore.ViewModels;
using System.Data.Entity;

namespace DvdStore.Controllers
{
    public class CustomersController : Controller
    {

        private ApplicationDbContext _dbContext;

        public CustomersController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }
        // GET: Customers
        public ViewResult Index()
        {
            //Nema potrebe jer ce datatable poslati ajax zahtjev na api
            //List<Customer> customers = _dbContext.Customers.Include(c => c.MembershipType).ToList();

                return View(/*customers*/);
        }

        //Customer details
        public ActionResult Details(int id)
        {
            var customer = _dbContext.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);

        }

        //Create new customer
        [HttpGet]
        public ActionResult Create()
        {
            CustomerFormViewModel model = new CustomerFormViewModel()
            {
                MembershipTypes = _dbContext.MembershipTypes.ToList(),
                Customer = new Customer()
            };

            return View("CustomerForm", model);
        }

        [HttpGet]
        //Edit customer
        public ActionResult Edit(int id)
        {
            Customer customerToEdit = _dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customerToEdit == null)
                return HttpNotFound();

            CustomerFormViewModel model = new CustomerFormViewModel
            {
                MembershipTypes = _dbContext.MembershipTypes.ToList(),
                Customer = customerToEdit
            };

            return View("CustomerForm", model);
        }

        //Save new/edited customer to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                CustomerFormViewModel model = new CustomerFormViewModel
                {
                    MembershipTypes = _dbContext.MembershipTypes.ToList(),
                    Customer = customer
                };

                return View("CustomerForm", model);
            }
            if (customer.Id == 0)
            {
                _dbContext.Customers.Add(customer);
            }
            else
            {
                Customer editCustomer = _dbContext.Customers.Single(c => c.Id == customer.Id);

                editCustomer.Name = customer.Name;
                editCustomer.BirthDate = customer.BirthDate;
                editCustomer.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                editCustomer.MembershipTypeId = customer.MembershipTypeId;
            }



            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Customers");

        }
    }

}









