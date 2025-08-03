using CustomerRelationManagement.Models;
using CustomerRelationManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;
using System.Linq;

namespace CustomerRelationManagement.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerService _customerService;
        

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public JsonResult GetProjectsByBuilderId(int builderId)
        {
            var projects = _customerService.GetProjectsByBuilderId(builderId)
                .Select(p => new SelectListItem
                {
                    Value = p.ProjectId.ToString(),
                    Text = p.ProjectName
                }).ToList();

            return Json(projects);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.BudgetOptions = _customerService.GetBudgetOptions()
           .Select(b => new SelectListItem
           {
               Value = b.BudgetId.ToString(),
               Text = b.BudgetName
           }).ToList();


            ViewBag.Locations = _customerService.GetLocations()
                .Select(l => new SelectListItem { Value = l.LocationId.ToString(), Text = l.LocationName }).ToList();
           

            ViewBag.PreferredLocations = _customerService.GetPreferredLocations()
                .Select(p => new SelectListItem { Value = p.PreferredLocationId.ToString(), Text = p.PreferredLocationName }).ToList();

            ViewBag.Builders = _customerService.GetBuilders()
           .Select(b => new SelectListItem
            {
            Value = b.BuilderId.ToString(),
            Text = b.BuilderName
            }).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {

                _customerService.AddCustomer(customer);
                TempData["SuccessMessage"] = "Information Submitted Successfully";
                return RedirectToAction("Index");
            }

            ViewBag.Locations = _customerService.GetLocations()
                .Select(l => new SelectListItem { Value = l.LocationId.ToString(), Text = l.LocationName }).ToList();

          
            ViewBag.PreferredLocations = _customerService.GetPreferredLocations()
                .Select(p => new SelectListItem { Value = p.PreferredLocationId.ToString(), Text = p.PreferredLocationName }).ToList();

            ViewBag.Builders = _customerService.GetBuilders()
           .Select(b => new SelectListItem
           {
               Value = b.BuilderId.ToString(),
               Text = b.BuilderName
           }).ToList();


            ViewBag.BudgetOptions = _customerService.GetBudgetOptions()
          .Select(b => new SelectListItem
          {
              Value = b.BudgetId.ToString(),
              Text = b.BudgetName
          }).ToList();

            return View(customer);
        }

        
        [HttpGet]
        public IActionResult Index()
        {
            var customers = _customerService.GetAllCustomersDetails(); 
            return View(customers);
        }

    }
}


