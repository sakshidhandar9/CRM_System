using CustomerRelationManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CustomerRelationManagement.Services
{
    public interface ICustomerService
    {
        List<Location> GetLocations();

        public List<Budget> GetBudgetOptions();

        List<PreferredLocation> GetPreferredLocations();

        List<Builder> GetBuilders();
        List<Project> GetProjectsByBuilderId(int BuilderId);
         
        public List<CustomerViewModel> GetAllCustomersDetails();
        List<Project> GetProjects();
        void AddCustomer(Customer customer);









        //List<Requirement> GetRequirements();
        //List<Budget> GetBudgets();

    }
}
