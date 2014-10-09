using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityServices.Models;
using System.IO;
using System.Data;
using UtilityServices.Cache;
using System.Reflection;

namespace UtilityServices.Controllers
{
    public class HttpParamActionAttribute : ActionNameSelectorAttribute
    {
        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            if (actionName.Equals(methodInfo.Name, StringComparison.InvariantCultureIgnoreCase))
                return true;

            if (!actionName.Equals("Action", StringComparison.InvariantCultureIgnoreCase))
                return false;

            var request = controllerContext.RequestContext.HttpContext.Request;
            return request[methodInfo.Name] != null;
        }
    } 

    public class BudgetMeterController : Controller
    {
        private string _ErrorMessage;
        
        private BudgetMeterDA _db = new BudgetMeterDA();

        //
        // GET: /BudgetMeter/
        public ActionResult Index(string ProcessingStatus = "", string EAN = "",string Id = "")
        {
            TempData["SaveError"] = "";

            //if (Id != "")
            //    ReleaseLock(Id);

            MaintainCache();

            // Build Status Filters
            List<String> StatusList = new List<String>();
            StatusList.Add("Open");
            StatusList.Add("Booking");
            StatusList.Add("To Invoice");
            ViewBag.processingStatus = new SelectList(StatusList);

            var _BudgetMeters = _db.GetBudgetMeterData(out _ErrorMessage);
            IEnumerable<BudgetMeter> _List = _BudgetMeters.ToList();

            if (!String.IsNullOrEmpty(EAN))
            {
                _List = _List.Where(p => p.EAN.Contains(EAN));
            }

            if (!String.IsNullOrEmpty(ProcessingStatus) && ProcessingStatus != "All")
            {
                _List = _List.Where(p => p.ProcessingStatus == ProcessingStatus);
            }

            if (!String.IsNullOrEmpty(_ErrorMessage))
                ViewBag.Message = _ErrorMessage;

            if (TempData["Locked"] != null)
            {
                ViewBag.Message += TempData["Locked"].ToString(); 
            }
          
            ViewBag.item = _List;

            return View(_List);

        }

        /// <summary>
        /// Get Details for specified Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Details(string Id = "")
        {
            
            BudgetMeter _BudgetMeter = _db.GetBudgetMeterData(out _ErrorMessage).Find(p => p.ID == Id);
            if (_BudgetMeter.ProcessingStatus == "To Invoice")
                ViewBag.Actions = "Make statement";

            if (_BudgetMeter.ProcessingStatus == "Booking")
                ViewBag.Actions = "Make booking";

            if (_BudgetMeter.ProcessingStatus == "Open")
                ViewBag.Actions = "Make prepayment";

            if (_BudgetMeter == null)
            {
                return HttpNotFound();
            }
            return View(_BudgetMeter);

        }

        /// <summary>
        /// Get Edit mode
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Edit(string Id = "")
        {
            TempData["Locked"] = "";
            if(TempData["SaveError"] != null)
            {
                ViewBag.Message = TempData["SaveError"].ToString();
                TempData["SaveError"] = "";
            }
                // Check Locked by someone else
            if (!CanHaveLock(Id))
            {
                TempData["Locked"] = "Record is locked by another person!";
                return RedirectToAction("Index");
            }
            //// Update lock
            //if (HasLock(Id))
            //{
            //    UpdateLock(Id);
            //}
            //else
            //{
            //    ApplyLock(Id);
            //}
            
            BudgetMeter _BudgetMeter = _db.GetBudgetMeterData(out _ErrorMessage).Find(p => p.ID == Id);
            if (!string.IsNullOrEmpty(_ErrorMessage))
            {
                ViewBag.Message += _ErrorMessage;
            }
            if (_BudgetMeter == null)
            {
                return HttpNotFound();
            }
            var _BudgetMeterCODAList = _db.GetCODAReferences(out _ErrorMessage, _BudgetMeter.ID);
            IEnumerable<BudgetMeterCODA> _List = _BudgetMeterCODAList.ToList();

            EditModel _model = new EditModel();
            _model._budgetMeter = _BudgetMeter;
            _model._budgetMeterCODA = _List;

            var _Tuple = new Tuple<BudgetMeter, IEnumerable<BudgetMeterCODA>>(_BudgetMeter, _List);
            //return View(_BudgetMeter);
            //return View(_Tuple);
            return View(_model);
        }

        public ActionResult RetryError(string codaReference = "")
        {
            try
            {
                List<string> errorFixed = _db.RetryError(codaReference);
                if (errorFixed.Count != 0)
                    ViewBag.Message = string.Format("{0} errors occured. First error: {1}", errorFixed.Count, errorFixed[0]);
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
            }

            return RedirectToAction("ErrorList", "BudgetMeter");

            //return ErrorList();
        }

        public ActionResult RetryAllErrors()
        {
            TempData["LockedError"] = "";
            
            try
            {
                List<string> errors = _db.RetryAllErrors();
                foreach (string error in errors)
                {
                    _ErrorMessage += error + Environment.NewLine;
                    ViewBag.Message += error + Environment.NewLine;
                    TempData["LockedError"] += error + Environment.NewLine;
                }
            }
            catch (Exception e)
            {
                _ErrorMessage = e.Message;
                ViewBag.Message = e.Message;
            }
                        
            return RedirectToAction("ErrorList", "BudgetMeter");
        }

        //
        // POST: /BudgetMeter/Edit

        [HttpPost]
        public ActionResult Edit(EditModel editModel, string btnName, string btnSave)
        {
            switch (btnSave)
	        {
                case "Save":                        return UpdateEditModel(editModel);
                case "Update Contract Number":      return UpdateContractNumber(editModel);
                default: ViewBag.Message = "Unknown command";
                    TempData["SaveError"] = "Unknown command";
                    return Refresh(editModel);
	        }
        }

        private ActionResult UpdateEditModel(EditModel editModel)
        {
            string _ErrorMessage = "";
            BudgetMeter _BudgetMeter = new BudgetMeter();
            // Check not locked
            TempData["Locked"] = "";
            // Check Locked by someone else
            if (!CanHaveLock(editModel._budgetMeter.ID))
            {
                TempData["Locked"] = "Record is locked by another person!";
                return RedirectToAction("Index");
            }

            if (editModel._budgetMeter != null)
            {
                TempData["SaveError"] = "";
                // Save Changes
                _db.UpdateBudgetMeterData(editModel._budgetMeter, out _ErrorMessage);
                
                ViewBag.Message = _ErrorMessage;
                TempData["SaveError"] = _ErrorMessage;
            }
            ViewBag.Message = _ErrorMessage;
            
            return Refresh(editModel);                  
        }

        private ActionResult UpdateContractNumber(EditModel editModel)
        {
            TempData["SaveError"] = "";
            // Save Changes
            _db.UpdateContractNo(editModel._budgetMeter.AccountNo, out _ErrorMessage);

            ViewBag.Message = _ErrorMessage;
            TempData["SaveError"] = _ErrorMessage;

            ViewBag.Message = _ErrorMessage;

            return Refresh(editModel);
        }

        private ActionResult Refresh(EditModel editModel)
        {
            if (!string.IsNullOrEmpty(editModel._budgetMeter.ID))
            {
                BudgetMeter _BudgetMeter = _db.GetBudgetMeterData(out _ErrorMessage).Find(p => p.ID == editModel._budgetMeter.ID);
                if (_BudgetMeter != null && _BudgetMeter.ProcessingStatus != "Invoiced")
                {
                    var _BudgetMeterCODAList = _db.GetCODAReferences(out _ErrorMessage, _BudgetMeter.ID);
                    IEnumerable<BudgetMeterCODA> _List = _BudgetMeterCODAList.ToList();
                    EditModel _model = new EditModel();
                    _model._budgetMeter = _BudgetMeter;
                    _model._budgetMeterCODA = _List;

                    return View(_model);
                }
                else
                {
                    //ReleaseLock(editModel._budgetMeter.ID);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ExcelResult GetExcelData(string Id = "")
        {

            DataTable _Table    = new DataTable("SourceData");
            _Table = _db.GetSourceDetails(out _ErrorMessage,Id.Replace(" ",""));
            string _XmlStream = "";
            string _FileName = "";
            if (Id == "")
                _FileName = "Souce_All.xml";
            else
                _FileName = "Source_" + Id + ".xml";
                       
            ViewBag.Message = _ErrorMessage;

            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    
                    _Table.WriteXml(sw, true);
                    _XmlStream = sw.ToString();
                }
                
            }
            catch (Exception ex)
            {
                ViewBag.Message("Error Creating Download!");
            }
            // Return the excel AS XML file
            return new ExcelResult
            {
                FileName = _FileName,
                XMLStream = _XmlStream
            };

        }
      
        /// <summary>
        /// Get all errors
        /// </summary>
        /// <returns></returns>
        public ActionResult ErrorList(string CODAReference = "", bool retry = false)
        {
            ////Release Lock
            //if (CODAReference != "")
            //    ReleaseLock(CODAReference);

            var _BudgetMeters = _db.GetBudgetMeterErrors(out _ErrorMessage);
            
            IEnumerable<BudgetMeterError> _List = _BudgetMeters.ToList();

            if (!String.IsNullOrEmpty(_ErrorMessage))
                ViewBag.Message = _ErrorMessage;

            if (TempData["LockedError"] != null)
            {
                ViewBag.Message += TempData["LockedError"].ToString();
            }

            ViewBag.item = _List;

            return View(_List);
        }
        
        /// <summary>
        /// Get details for one error for a coda reference
        /// </summary>
        /// <param name="CODAReference">String: CODAReference to get the details for</param>
        /// <param name="EAN">String: EAN to set Checked</param>
        /// <returns></returns>
        public ActionResult ErrorDetails(string CODAReference = "", string EAN = "", string ChargeDate = "")
        {
            TempData["LockedError"] = "";
            // Check Locked by someone else
            if (!CanHaveLock(CODAReference))
            {
                TempData["LockedError"] = "Record is locked by another person!";
                return RedirectToAction("ErrorList");
            }
            //// Update lock
            //if (HasLock(CODAReference))
            //{
            //    UpdateLock(CODAReference);
            //}
            //else
            //{
            //    ApplyLock(CODAReference);
            //}


            // EAN and CODAReference not empty => Set contract error checked
            if (EAN != "" && CODAReference != "")
            {
                // Save Changes
                _db.UpdateErrorSetChecked(CODAReference,EAN,Convert.ToDateTime(ChargeDate),"", out _ErrorMessage);
                //// Release locked CODAReference
                //ReleaseLock(CODAReference);
            }

            var _BudgetMeters = _db.GetBudgetMeterErrorsDetails(CODAReference,out _ErrorMessage);
            
            if (!String.IsNullOrEmpty(_ErrorMessage))
                ViewBag.Message = _ErrorMessage;

            if (_BudgetMeters != null && _BudgetMeters.Count() != 0)
            {
                IEnumerable<BudgetMeterError> _List = _BudgetMeters.ToList();

                ViewBag.item = _List;

                return View(_List);
            }
            else
            {
                return RedirectToAction("ErrorList");
            }
           
        }


        /// <summary>
        /// Export the Errors for one CODAReference to XML
        /// </summary>
        /// <param name="CODAReference">string: Codareference to export</param>
        /// <returns></returns>
        public ExcelResult GetExcelErrorData(string CODAReference = "")
        {

            DataTable _Table    = new DataTable("SourceData");
            _Table = _db.GetErrorExportDetails(out _ErrorMessage, CODAReference);
            string _XmlStream = "";
            string _FileName = "";
            _FileName = "Errors_" + CODAReference + ".xml";
                       
            ViewBag.Message = _ErrorMessage;

            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    
                    _Table.WriteXml(sw, true);
                    _XmlStream = sw.ToString();
                }
                
            }
            catch (Exception ex)
            {
                ViewBag.Message("Error Creating Download!");
            }
            // Return the excel AS XML file
            return new ExcelResult
            {
                FileName = _FileName,
                XMLStream = _XmlStream
            };

        }
        // Cache Methods

        /// <summary>
        /// Apply a lock
        /// </summary>
        /// <param name="Id">string: Id of the record to lock</param>
        protected virtual void ApplyLock(string Id)
        {
            var list = new List<Cache.Cache>();
            if (HttpContext.Cache["IdList"] != null)
                list = (List<Cache.Cache>)HttpContext.Cache["IdList"];
            list.Add(new Cache.Cache { RecordId = Id, SessionId = Session.SessionID, TimeStamp = DateTime.Now });
            HttpContext.Cache["IdList"] = list;
        }

        /// <summary>
        /// Check Record has a lock for my session
        /// </summary>
        /// <param name="Id">String: Id of the record that we want to lock</param>
        /// <returns></returns>
        protected virtual bool HasLock(string Id)
        {
            var list = new List<Cache.Cache>();
            if (HttpContext.Cache["IdList"] != null)
                list = (List<Cache.Cache>)HttpContext.Cache["IdList"];
            return list.Any(x => x.RecordId.Equals(Id) && x.SessionId.Equals(Session.SessionID));
        }

        /// <summary>
        /// Check we can lock the record / not locked by someone else
        /// </summary>
        /// <param name="Id">String: Id of the record that we want to lock</param>
        /// <returns></returns>
        protected virtual bool CanHaveLock(string Id)
        {
            var list = new List<Cache.Cache>();
            if (HttpContext.Cache["IdList"] != null)
                list = (List<Cache.Cache>)HttpContext.Cache["IdList"];
            return !list.Any(x => x.RecordId.Equals(Id) && !x.SessionId.Equals(Session.SessionID));
        }

        /// <summary>
        /// Maintain Cache to release
        /// </summary>
        protected virtual void MaintainCache()
        {
            var list = new List<Cache.Cache>();
            if (HttpContext.Cache["IdList"] != null)
                list = (List<Cache.Cache>)HttpContext.Cache["IdList"];
            list.RemoveAll(x => x.TimeStamp < DateTime.Now.AddMinutes(-15));
            HttpContext.Cache["IdList"] = list;
        }

        ///// <summary>
        ///// Release the lock for the record
        ///// </summary>
        ///// <param name="Id">String: Id of the record ro release</param>
        //protected virtual void ReleaseLock(string Id)
        //{
        //    var list = new List<Cache.Cache>();
        //    if (HttpContext.Cache["IdList"] != null)
        //        list = (List<Cache.Cache>)HttpContext.Cache["IdList"];
        //    list.RemoveAll(x => x.RecordId.Equals(Id) && x.SessionId.Equals(Session.SessionID));
        //    HttpContext.Cache["IdList"] = list;
        //}

        ///// <summary>
        ///// Update the lock
        ///// </summary>
        ///// <param name="Id">String: Id of the lock to update</param>
        //protected virtual void UpdateLock(string Id)
        //{
        //    // Release Lock
        //    ReleaseLock(Id);
    
        //    //Apply a lock
        //    ApplyLock(Id);
            
        //}
    
    }
}
