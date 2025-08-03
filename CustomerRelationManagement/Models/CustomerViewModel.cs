using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerRelationManagement.Models
{
    public class CustomerViewModel
    {

            public int CustomerId { get; set; }
            public string CustomerName { get; set; }

            public int LocationId { get; set; }
            public string LocationName { get; set; }

            public string PreferredLocationName { get; set; }
            public int PreferredLocationId { get; set; }
           
            public int BuilderId { get; set; }
            public string BuilderName { get; set; }

            public int ProjectId { get; set; }
            public string ProjectName { get; set; }

            public int BudgetId { get; set; }
            public string BudgetName { get; set; }


    }
    }



    

