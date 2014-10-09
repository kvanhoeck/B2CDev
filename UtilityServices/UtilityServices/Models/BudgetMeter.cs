using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace UtilityServices.Models
{
    public class BudgetMeter
    {
        private string _ID;
        private string  _ContractNo;
        private string _AccountNo;
        private string _ProcessingStatus;
        private string _ErrorMessage;
        private string _EAN;
        private string _PrepaymentReference;
        private string _StatementReference;
        private decimal _AmountExcl;
        private decimal _AmountIncl;
        private bool _Booking;
        private string _GridOwnerName;
        private int _VATPercentage;
        
        public BudgetMeter()
        {

        }

        public BudgetMeter(string id, string contractNo, string accountNo, string processingStatus, string errorMessage, string ean, string prepaymentReference, string statementReference, decimal amountExcl, decimal amountIncl, bool booking, string gridOwnerName, int vatPercentage)
        {
            _ID = id;
            _ContractNo = contractNo;
            _AccountNo = accountNo;
            _ProcessingStatus = processingStatus;
            _ErrorMessage = errorMessage;
            _EAN = ean;
            _PrepaymentReference = prepaymentReference;
            _StatementReference = statementReference;
            _AmountExcl = amountExcl;
            _AmountIncl = amountIncl;
            _Booking = booking;
            _GridOwnerName = gridOwnerName;
            _VATPercentage = vatPercentage;
        }

        public string ID
        {
            get { return _ID; }
            set{_ID = value;}
        }

        public string ContractNo
        {
            get { return _ContractNo; }
            set { _ContractNo = value; }
        }

        public string AccountNo
        {
            get { return _AccountNo; }
            set { _AccountNo = value; }
        }

        public string ProcessingStatus
        {
            get { return _ProcessingStatus; }
            set { _ProcessingStatus = value; }
        }

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }

        public string EAN
        {
            get { return _EAN; }
            set { _EAN = value; }
        }
        [Required(ErrorMessage = "Prepayment Reference is required!")]
        public string PrepaymentReference
        {
            get { return _PrepaymentReference; }
            set { _PrepaymentReference = value; }
        }
        [Required(ErrorMessage = "Statement Reference is required!")]
        public string StatementReference
        {
            get { return _StatementReference; }
            set { _StatementReference = value; }
        }
        public string AmountExcl
        {
            get {
                if (_ProcessingStatus == "To Invoice")
                {
                    return string.Format("{0:0.00}", (_AmountExcl * -1));
                }else
                    return string.Format("{0:0.00}", _AmountExcl);
            }
            set { _AmountExcl = Convert.ToDecimal(value); }
        }
        public string AmountIncl
        {
            get { return string.Format("{0:0.00}", _AmountIncl); }
            set { _AmountIncl = Convert.ToDecimal(value); }
        }

        public bool Booking
        {
            get { return _Booking; }
            set { _Booking = value; }
        }
        public string GridOwnerName
        {
            get { return _GridOwnerName; }
            set { _GridOwnerName = value; }
        }

        public int VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
    }
}