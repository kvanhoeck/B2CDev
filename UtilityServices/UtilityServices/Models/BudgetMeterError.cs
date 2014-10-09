using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityServices.Models
{
    public class BudgetMeterError
    {
        private string _ID;
        private string _CODAReference;
        private string _GridOwner;
        private string _ErrorMessage;
        private string _EAN;
        private bool _MasterDataChecked;
        private decimal _TalexusAmount;
        private decimal _TalexusAmountError;
        private decimal _CODAAmount;
        private bool _NoContract;
        private decimal _Sort;
        private DateTime _ChargeDate;
        private string _CheckedComment;
   
        public BudgetMeterError()
        {

        }

        public BudgetMeterError(string CODAReference, string GridOwner, decimal TalexusAmount, decimal CODAAmount, decimal TalexusAmountError)
        {
            _CODAReference = CODAReference;
            _GridOwner = GridOwner;
            _TalexusAmount = TalexusAmount;
            _CODAAmount = CODAAmount;
            _TalexusAmountError = TalexusAmountError;
           
        }
        public BudgetMeterError(string CODAReference, string GridOwner, decimal TalexusAmount, decimal CODAAmount, string EAN, string ErrorMessage, bool MasterdataChecked, string ID, DateTime ChargeDate )
        {
            _CODAReference = CODAReference;
            _GridOwner = GridOwner;
            _TalexusAmount = TalexusAmount;
            _CODAAmount = CODAAmount;
            _EAN = EAN;
            _ErrorMessage = ErrorMessage;
            _MasterDataChecked = MasterdataChecked;
            _ID = ID;
            _ChargeDate = ChargeDate;
            _CheckedComment = CheckedComment;
        }

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string CODAReference
        {
            get { return _CODAReference; }
            set { _CODAReference = value; }
        }

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }

        public string TalexusAmount
        {
            get
            {
                return string.Format("{0:0.00}",_TalexusAmount);
            }
            set { _TalexusAmount = Convert.ToDecimal(value); }
        }
        public string TalexusAmountError
        {
            get
            {
                return string.Format("{0:0.00}", _TalexusAmountError);
            }
            set { _TalexusAmountError = Convert.ToDecimal(value); }
        }

        public string CODAAmount
        {
            get
            {
                if (_CODAAmount == 0)
                {
                    return "N/A";
                }
                else
                    return string.Format("{0:0.00}", _CODAAmount); }
            set { _CODAAmount = Convert.ToDecimal(value); }
        }

        public bool MasterDataChecked
        {
            get { return _MasterDataChecked; }
            set { _MasterDataChecked = value; }
        }
        public string GridOwner
        {
            get { return _GridOwner; }
            set { _GridOwner = value; }
        }

        public bool NoContract
        {
            get { return _NoContract; }
            set { _NoContract = value; }
        }

        public string EAN
        {
            get { return _EAN; }
            set { _EAN = value; }
        }

        public string ChargeDate
        {
            get { return _ChargeDate.ToString("yyyy-MM-dd"); }
            set { _ChargeDate = Convert.ToDateTime(value); }
        }

        public string CheckedComment
        {
            get { return _CheckedComment;}
            set{ _CheckedComment = value;}
        }
    }
}