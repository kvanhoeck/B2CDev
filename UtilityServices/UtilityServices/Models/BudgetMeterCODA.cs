using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace UtilityServices.Models
{
    public class BudgetMeterCODA
    {
        private string _ID;
        private string  _CODAReference;
        private decimal _AmountIncl;
        private DateTime _PaymentDate;

        
        public BudgetMeterCODA()
        {

        }

        public BudgetMeterCODA(string id, string CODAReference, decimal amountIncl, DateTime PaymentDate)
        {
            _ID = id;
            _CODAReference = CODAReference;
            _AmountIncl = amountIncl;
            _PaymentDate = PaymentDate;
          
        }

        public string ID
        {
            get { return _ID; }
            set{_ID = value;}
        }

        public string CODAReference
        {
            get { return _CODAReference; }
            set { _CODAReference = value; }
        }

       
        public string AmountIncl
        {
            get { return string.Format("{0:0.00}", _AmountIncl); }
            set { _AmountIncl = Convert.ToDecimal(value); }
        }
    
        public string PaymentDate
        {
            get { return string.Format("{0:dd/MM/yyyy}",_PaymentDate); }
            set { _PaymentDate = Convert.ToDateTime(value); }
        }
    }
}