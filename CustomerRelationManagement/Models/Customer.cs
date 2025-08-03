using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerRelationManagement.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        [Display(Name = "Location")]
        public int LocationId { get; set; }


        [Display(Name = "PreferredLocation")]
        
        public int PreferredLocationId { get; set; }


        public int BuilderId { get; set; }
        public int ProjectId { get; set; }

        public int BudgetId { get; set; }

        
        public List<int> PreferredLocationIds { get; set; }

        public List<int> SelectedBudgetIds { get; set; }

        public List<SelectListItem> BudgetOptions { get; set; }

        public string SelectedBudgetNames { get; set; }



    }
}
