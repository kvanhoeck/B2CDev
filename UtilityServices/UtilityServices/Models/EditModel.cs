using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityServices.Models
{
    public class EditModel
    {
        public BudgetMeter _budgetMeter { get; set; }
        public IEnumerable<BudgetMeterCODA> _budgetMeterCODA { get; set; }
    }
}