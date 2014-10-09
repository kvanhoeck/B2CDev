using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace UtilityServices.Models
{
    public class BudgetMeterDA
    {
    
        //KVH: Central use of code
        private SqlConnection GetConnection()
        {
            return new SqlConnection(WebConfigurationManager.ConnectionStrings["UtilityServiceDBContext"].ConnectionString);
        }

        //KVH: Central use of code
        private SqlParameter GetSQLParameter(string name, SqlDbType type, object value, ParameterDirection direction = ParameterDirection.Input)
        {
            SqlParameter parameter = new SqlParameter(name, type);
            parameter.Value = value;
            parameter.Direction = direction;

            return parameter;
        }

        //KVH: Central use of code
        private SqlParameter GetSQLParameter(string name, SqlDbType type, int size, object value, ParameterDirection direction = ParameterDirection.Input)
        {
            SqlParameter parameter = new SqlParameter(name, type, size);
            parameter.Value = value;
            parameter.Direction = direction;

            return parameter;
        }

        /// <summary>
        /// Update the specified budgetmeter
        /// </summary>
        /// <param name="budgetMeter">BudgetMeter: to update</param>
        /// <param name="errorMessage">String: Errormessage when occured</param>
        public void UpdateBudgetMeterData(BudgetMeter budgetMeter ,out string errorMessage)
        {
            errorMessage = "";
            int _errorNumber = 0, _actionType = 0;
            string _invoiceSalesNo = "";

            SqlConnection _connection = null;
            SqlCommand _command = null;

            try
            {
                _connection = GetConnection(); //Fix KVH: Central use of config parameter readings
                //_SQLConnection.Open(); // Fix KVH: Open connection as late as possible!

                if (String.IsNullOrEmpty(budgetMeter.StatementReference) && budgetMeter.Booking)
                    _actionType = 3;
                
                else if (String.IsNullOrEmpty(budgetMeter.StatementReference) && !budgetMeter.Booking)
                {
                    _actionType = 1;
                    _invoiceSalesNo = budgetMeter.PrepaymentReference;
                }
                else if (String.IsNullOrEmpty(budgetMeter.StatementReference))
                {
                    _actionType = 2;
                    _invoiceSalesNo = budgetMeter.StatementReference;
                }

                //Fix Koen: code length reduction by using private method GetSQLParameter and the use of the Parameters.Add method

                _command = new SqlCommand("[BudgetMeter].[USP_UpdateSourceData]", _connection);
                _command.CommandTimeout = 120;
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.Parameters.Add(GetSQLParameter("@AccountNo", SqlDbType.VarChar, budgetMeter.AccountNo));
                _command.Parameters.Add(GetSQLParameter("@ContractNo", SqlDbType.VarChar, budgetMeter.ContractNo));
                _command.Parameters.Add(GetSQLParameter("@InvoiceNo", SqlDbType.VarChar, _invoiceSalesNo));
                _command.Parameters.Add(GetSQLParameter("@Type", SqlDbType.Int, _actionType));
                _command.Parameters.Add(GetSQLParameter("@Booking", SqlDbType.Bit, budgetMeter.Booking));
                _command.Parameters.Add(GetSQLParameter("@ErrorMessage", SqlDbType.VarChar, 250, errorMessage, ParameterDirection.Output));
                _command.Parameters.Add(GetSQLParameter("@ErrorNumber", SqlDbType.Int, null, System.Data.ParameterDirection.ReturnValue));

                _connection.Open();
                _command.ExecuteNonQuery();
                _errorNumber = (int)_command.Parameters["@ErrorNumber"].Value;
                if (_errorNumber != 0)
                    errorMessage = _command.Parameters["@ErrorMessage"].Value.ToString();
            }
            catch (SqlException ex)
            {
                errorMessage = "Error Update BugetMeter Data \n";
                errorMessage += ex.ToString();
            }
            catch (InvalidOperationException ex)
            {
                errorMessage = "Error Update BudgetMeter Data\n";
                errorMessage += ex.ToString();
            }
            finally
            {
             
                if (_command != null)
                    _command.Dispose();
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }

        /// <summary>
        /// Update the specified budgetmeter
        /// </summary>
        /// <param name="budgetMeter">BudgetMeter: to update</param>
        /// <param name="ErrorMessage">String: Errormessage when occured</param>
        public void UpdateErrorSetChecked(string CODAReference, string EAN, DateTime ChargeDate, string CheckedComment, out string ErrorMessage)
        {
            ErrorMessage = "";
            int _ErrorNumber = 0;

            SqlConnection _SQLConnection = null;
            SqlCommand _SqlCommand = null;

            try
            {
                _SQLConnection = GetConnection(); //Fix KVH: Central use of config parameter readings
                //_SQLConnection.Open(); // Fix KVH: Open connection as late as possible!


                SqlParameter _CheckedComment = new SqlParameter("@CheckedComment", SqlDbType.VarChar, 250);
                _CheckedComment.Value = CheckedComment.Length > 250 ? CheckedComment.Substring(1, 250) : CheckedComment;

                _CheckedComment.Direction = ParameterDirection.Input;

                SqlParameter _CODAReference = new SqlParameter("@CODAReference", SqlDbType.VarChar);
                _CODAReference.Value = CODAReference;
                _CODAReference.Direction = ParameterDirection.Input;

                SqlParameter _EAN = new SqlParameter("@EAN", SqlDbType.VarChar, 50);
                _EAN.Value = EAN;
                _EAN.Direction = ParameterDirection.Input;

                SqlParameter _ErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.VarChar, 250);
                _ErrorMessage.Value = ErrorMessage;
                _ErrorMessage.Direction = ParameterDirection.Output;

                SqlParameter _ErrorNo = new SqlParameter("@ErrorNumber", SqlDbType.Int);
                _ErrorNo.Direction = System.Data.ParameterDirection.ReturnValue;

                SqlParameter _ChargeDate = new SqlParameter("@ChargeDate", SqlDbType.Date);
                _ChargeDate.Direction = ParameterDirection.Input;
                _ChargeDate.Value = ChargeDate;

                _SqlCommand = new SqlCommand("[BudgetMeter].[USP_UpdateSourceDataErrors]", _SQLConnection);
                _SqlCommand.CommandTimeout = 120;
                _SqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                _SqlCommand.Parameters.Add(_EAN);
                _SqlCommand.Parameters.Add(_CODAReference);
                _SqlCommand.Parameters.Add(_ChargeDate);
                _SqlCommand.Parameters.Add(_ErrorMessage);
                _SqlCommand.Parameters.Add(_ErrorNo);
                _SqlCommand.Parameters.Add(_CheckedComment);

                _SQLConnection.Open();
                _SqlCommand.ExecuteNonQuery();
                _ErrorNumber = (int)_SqlCommand.Parameters["@ErrorNumber"].Value;
                if (_ErrorNumber != 0)
                {
                    ErrorMessage = _ErrorMessage.Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                ErrorMessage = "Error Update BugetMeter Error Data \n";
                ErrorMessage += ex.ToString();
            }
            catch (InvalidOperationException ex)
            {
                ErrorMessage = "Error Update BudgetMeter Error Data\n";
                ErrorMessage += ex.ToString();
            }
            finally
            {

                if (_SqlCommand != null)
                    _SqlCommand.Dispose();
                if (_SQLConnection.State == ConnectionState.Open)
                    _SQLConnection.Close();
            }
        }

        public List<string> RetryAllErrors()
        {
            SqlConnection _connection = null;
            SqlCommand _command = null;
            List<string> _resultErrorMessages = new List<string>();
            
            try
            {
                _connection = GetConnection(); //Fix KVH: Central use of config parameter readings

                SqlParameter _errorNo = GetSQLParameter("@ErrorNumber", SqlDbType.Int, ParameterDirection.ReturnValue);

                _command = new SqlCommand("[BudgetMeter].[USP_RetryAllErrors]", _connection);
                _command.CommandTimeout = 120;
                _command.CommandType = CommandType.StoredProcedure;
                //_command.Parameters.Add(_errorNo);

                _connection.Open();
                _command.ExecuteNonQuery();

                if ((int)_command.Parameters["@ErrorNumber"].Value != 0)
                    _resultErrorMessages.Add("Unexpected error occured in the Stored Procedure");
            }
            catch (Exception e)
            {
                _resultErrorMessages.Add(e.InnerException == null ? e.Message : e.InnerException.Message);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }

            return _resultErrorMessages;
        }

        public List<string> RetryError(string codaReference)
        {
            string _detailsErrorMessage;
            List<string> _resultErrorMessages = new List<string>();
            SqlConnection _connection = null;
            SqlCommand _command = null;

            List<BudgetMeterError> _errors = GetBudgetMeterErrorsDetails(codaReference, out _detailsErrorMessage);

            if (_errors.Count == 0)
                _resultErrorMessages.Add("No errors were found.");
            if (String.IsNullOrEmpty(_detailsErrorMessage))
            {
                foreach (BudgetMeterError error in _errors)
                {
                    try
                    {
                        _connection = GetConnection(); //Fix KVH: Central use of config parameter readings

                        SqlParameter _codaReference     = GetSQLParameter("@CODAReference", SqlDbType.VarChar, 12, codaReference);
                        SqlParameter _ean               = GetSQLParameter("@EAN", SqlDbType.VarChar, 18, error.EAN);
                        SqlParameter _errorMessage      = GetSQLParameter("@ErrorMessage", SqlDbType.VarChar, 250, null, ParameterDirection.Output);
                        SqlParameter _errorNo           = GetSQLParameter("@ErrorNumber", SqlDbType.Int, ParameterDirection.ReturnValue);

                        _command = new SqlCommand("[BudgetMeter].[USP_RetryError]", _connection);
                        _command.CommandTimeout = 120;
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.Add(_codaReference);
                        _command.Parameters.Add(_ean);
                        _command.Parameters.Add(_errorMessage);
                        //_command.Parameters.Add(_errorNo);

                        _connection.Open();
                        _command.ExecuteNonQuery();

                        //if ((int)_command.Parameters["@ErrorNumber"].Value != 0)
                        if (!String.IsNullOrEmpty(_errorMessage.Value.ToString()))
                            _resultErrorMessages.Add(_errorMessage.Value.ToString());
                    }
                    catch (Exception e)
                    {
                        _resultErrorMessages.Add(e.InnerException == null ? e.Message : e.InnerException.Message);
                    }
                    finally
                    {
                        if (_connection.State == ConnectionState.Open)
                            _connection.Close();
                    }
                }
            }
            else
                _resultErrorMessages.Add(_detailsErrorMessage);

            return _resultErrorMessages;
        }


        public void UpdateContractNo(string accountNo, out string errorMessage)
        {
            errorMessage = "";
            int _errorNumber = 0;

            SqlConnection _connection = null;
            SqlCommand _command = null;

            try
            {
                _connection = GetConnection(); //Fix KVH: Central use of config parameter readings
                //_SQLConnection.Open(); // Fix KVH: Open connection as late as possible!

                _command = new SqlCommand("[BudgetMeter].[USP_UpdateContractNo]", _connection);
                _command.CommandTimeout = 120;
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.Parameters.Add(GetSQLParameter("@AccountNo", SqlDbType.VarChar, accountNo));
                _command.Parameters.Add(GetSQLParameter("@ErrorMessage", SqlDbType.VarChar, 250, errorMessage, ParameterDirection.Output));
                _command.Parameters.Add(GetSQLParameter("@ErrorNumber", SqlDbType.Int, null, System.Data.ParameterDirection.ReturnValue));

                _connection.Open();
                _command.ExecuteNonQuery();
                _errorNumber = (int)_command.Parameters["@ErrorNumber"].Value;
                if (_errorNumber != 0)
                    errorMessage = _command.Parameters["@ErrorMessage"].Value.ToString();
            }
            catch (SqlException ex)
            {
                errorMessage = "Error Update Contract Number\n";
                //ErrorMessage += ex.ToString();
            }
            catch (InvalidOperationException ex)
            {
                errorMessage = "Error Update Contract Number\n";
                //ErrorMessage += ex.ToString();
            }
            finally
            {

                if (_command != null)
                    _command.Dispose();
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }

        /// <summary>
        /// Get The source Details
        /// </summary>
        /// <param name="ErrorMessage">String: Errormessage when occured</param>
        /// <param name="Id">String: Specified Id to export</param>
        /// <returns>DataTable with all</returns>
        public DataTable GetSourceDetails( out string ErrorMessage, string Id)
        {

            DataTable _Table = new DataTable("SourceData");
            SqlConnection _SQLConnection = null;
            SqlCommand _SqlCommand = null;
            SqlDataAdapter _SqlDataAdapter = null;
            string _Query = "";

            ErrorMessage = "";

            //Get data
            if (Id    != "")
                _Query = "	SELECT [ContractNo],[AccountNo],[ProcessingStatus],[ErrorMessage],[EAN],[AmountExcl],[AmountIncl],[PrePaymentReference],[StatementReference],[GridOwnerName] FROM [BudgetMeter].[vw_InitialSourceData] WHERE ID = '" + Id + "' ";
            else
                _Query = " SELECT [ContractNo], [AccountNo],[EAN],[ChargeDate],[AmountExcl],[AmountIncl],[PrePaymentReference],[StatementReference],[StatementDate],[Booking],[CODAExists],[CODAReference],[PaymentDate],[ConsumptionExists],[TalexusReference],[GridOwnerName],[ProcessingStatus],[ErrorMessage] FROM [BudgetMeter].[vw_AllSourceData] ";
            try
            {
                _SQLConnection = GetConnection(); //Fix KVH: Central use of config parameter readings

                _SqlCommand = new SqlCommand(_Query, _SQLConnection);
                _SqlDataAdapter = new SqlDataAdapter(_SqlCommand);
                _SqlDataAdapter.Fill(_Table);
            }
            catch (SqlException ex)
            {
                ErrorMessage = "Error Getting BugetMeter Data \n";
                //ErrorMessage += ex.ToString();
            }
            catch (InvalidOperationException ex)
            {
                ErrorMessage = "Error Getting BugetMeter Data \n";
                //ErrorMessage += ex.ToString();
            }
            finally
            {
                if (_SqlDataAdapter != null)
                    _SqlDataAdapter.Dispose();
                if (_SqlCommand != null)
                    _SqlCommand.Dispose();
                if (_SQLConnection.State == ConnectionState.Open)
                    _SQLConnection.Close();
            }
            return _Table;

        }

        /// <summary>
        /// Get the error details for export to Excel
        /// </summary>
        /// <param name="ErrorMessage"></param>
        /// <param name="CODAReference"></param>
        /// <returns></returns>
        public DataTable GetErrorExportDetails(out string ErrorMessage, string CODAReference)
        {
            DataTable _Table = new DataTable("SourceData");
            SqlConnection _SQLConnection = null;
            SqlCommand _SqlCommand = null;
            SqlDataAdapter _SqlDataAdapter = null;
            string _Query = "";

            ErrorMessage = "";

            //Get data

            _Query = "	SELECT ID,CODAReference,GridOwnerName, EAN,ErrorMessage,MasterdataChecked,TalexusAmount,ChargeDate	FROM [BudgetMeter].[vw_ErrorDataDetailsExport] WHERE CODARef ='" + CODAReference + "'";

            try
            {
                _SQLConnection = GetConnection(); //Fix KVH: Central use of config parameter readings

                _SqlCommand = new SqlCommand(_Query, _SQLConnection);
                _SqlDataAdapter = new SqlDataAdapter(_SqlCommand);
                _SqlDataAdapter.Fill(_Table);
            }
            catch (SqlException ex)
            {
                ErrorMessage = "Error Getting BugetMeter Error Data \n";
                //ErrorMessage += ex.ToString();
            }
            catch (InvalidOperationException ex)
            {
                ErrorMessage = "Error Getting BugetMeter Error Data \n";
                //ErrorMessage += ex.ToString();
            }
            finally
            {
                if (_SqlDataAdapter != null)
                    _SqlDataAdapter.Dispose();
                if (_SqlCommand != null)
                    _SqlCommand.Dispose();
                if (_SQLConnection.State == ConnectionState.Open)
                    _SQLConnection.Close();
            }
            return _Table;
        }
    

        /// <summary>
        /// Get List of all budgetMeters to invoice
        /// </summary>
        /// <param name="ErrorMessage">String: Errormessage when occurred</param>
        /// <returns>List of BudgetMeters</returns>
        public List<BudgetMeter> GetBudgetMeterData(out string ErrorMessage)
        {
            //Variables
            List<BudgetMeter> _BudgetMeterList = new List<BudgetMeter>();
            SqlConnection _SQLConnection = null;
            SqlCommand _SqlCommand = null;
            SqlDataAdapter _SqlDataAdapter = null;
            DataSet _BudgetMeterDataSet = new DataSet();
            string _Query = "";

            ErrorMessage = "";

            //Get data
            _Query = "	SELECT ID,ContractNo,AccountNo,ProcessingStatus,ErrorMessage,EAN,AmountExcl,PrepaymentReference,StatementReference,AmountIncl,Booking,GridOwnerName,VATPercentage	FROM [BudgetMeter].[vw_SourceData]";
            try
            {
                _SQLConnection = GetConnection(); //Fix KVH: Central use of config parameter readings

                _SqlCommand = new SqlCommand(_Query, _SQLConnection);
                _SqlDataAdapter = new SqlDataAdapter(_SqlCommand);
                _SqlDataAdapter.Fill(_BudgetMeterDataSet);

                foreach (DataRow _row in _BudgetMeterDataSet.Tables[0].Rows)
                {
                    BudgetMeter _Budget = new BudgetMeter(  _row["ID"].ToString(),
                                                            _row["ContractNo"].ToString(),
                                                            _row["AccountNo"].ToString(),
                                                            _row["ProcessingStatus"].ToString(),
                                                            _row["ErrorMessage"].ToString(),
                                                            _row["EAN"].ToString(),
                                                            _row["PrepaymentReference"].ToString(),
                                                            _row["StatementReference"].ToString(),
                                                            Convert.ToDecimal(_row["AmountExcl"]),
                                                            Convert.ToDecimal(_row["AmountIncl"]),
                                                            Convert.ToBoolean(_row["Booking"]),
                                                            _row["GridOwnerName"].ToString(),
                                                            Convert.ToInt32(_row["VATPercentage"]));
                    _BudgetMeterList.Add(_Budget);
                }

            }
            catch (SqlException ex)
            {
                ErrorMessage = "Error Getting BugetMeter Data \n";
                //ErrorMessage += ex.ToString();

            }
            catch (InvalidOperationException ex)
            {
                ErrorMessage = "Error Getting BugetMeter Data \n";
                //ErrorMessage += ex.ToString();
            }
            finally
            {
                if (_SqlDataAdapter != null)
                    _SqlDataAdapter.Dispose();
                if (_SqlCommand != null)
                    _SqlCommand.Dispose();
                if (_BudgetMeterDataSet != null)
                    _BudgetMeterDataSet.Dispose();
                if (_SQLConnection.State == ConnectionState.Open)
                    _SQLConnection.Close();
            }

            return _BudgetMeterList;

        }

        /// <summary>
        /// Get a list of all CODAReferences and amounts used for specified ID
        /// </summary>
        /// <param name="ErrorMessage">string: Errormessage when occured</param>
        /// <param name="ID">string: ID to find</param>
        /// <returns></returns>
        public List<BudgetMeterCODA> GetCODAReferences(out string ErrorMessage, string ID)
        {

            List<BudgetMeterCODA> _BudgetMeterCODAList = null;
            SqlConnection _SQLConnection = null;
            SqlCommand _SqlCommand = null;
            SqlDataAdapter _SqlDataAdapter = null;
            DataSet _BudgetMeterDataSet = new DataSet();
            string _Query = "";

            ErrorMessage = "";

            //Get data
            _Query = "	SELECT ID,CODAReference,AmountIncl,PaymentDate	FROM [BudgetMeter].[vw_CODAData] WHERE ID ='" + ID + "'";
            try
            {
                _SQLConnection = GetConnection(); //Fix KVH: Central use of config parameter readings

                _SqlCommand = new SqlCommand(_Query, _SQLConnection);
                _SqlDataAdapter = new SqlDataAdapter(_SqlCommand);
                _SqlDataAdapter.Fill(_BudgetMeterDataSet);
                _BudgetMeterCODAList = new List<BudgetMeterCODA>();
                foreach (DataRow _row in _BudgetMeterDataSet.Tables[0].Rows)
                {
                    BudgetMeterCODA _Budget = new BudgetMeterCODA(_row["ID"].ToString(),
                                                                    _row["CODAReference"].ToString(),
                                                                    Convert.ToDecimal(_row["AmountIncl"]),
                                                                    Convert.ToDateTime(_row["PaymentDate"]));
                    _BudgetMeterCODAList.Add(_Budget);
                }

            }
            catch (SqlException ex)
            {
                ErrorMessage = "Error Getting BugetMeter CODA Details Data \n";
                //ErrorMessage += ex.ToString();
            }
            catch (InvalidOperationException ex)
            {
                ErrorMessage = "Error Getting BugetMeter CODA Details Data \n";
                //ErrorMessage += ex.ToString();
            }
            finally
            {
                if (_SqlDataAdapter != null)
                    _SqlDataAdapter.Dispose();
                if (_SqlCommand != null)
                    _SqlCommand.Dispose();
                if (_BudgetMeterDataSet != null)
                    _BudgetMeterDataSet.Dispose();
                if (_SQLConnection.State == ConnectionState.Open)
                    _SQLConnection.Close();
            }

            return _BudgetMeterCODAList;
        }

        /// <summary>
       /// Get the BudgetMeters that are in Error
       /// </summary>
       /// <param name="ErrorMessage">string: Errormessage when occured</param>
       /// <returns>List with all Errors</returns>
        public List<BudgetMeterError> GetBudgetMeterErrors(out string ErrorMessage)
        {
            //Variables
            List<BudgetMeterError> _BudgetMeterErrorList = new List<BudgetMeterError>();
            SqlConnection _SQLConnection = null;
            SqlCommand _SqlCommand = null;
            SqlDataAdapter _SqlDataAdapter = null;
            DataSet _BudgetMeterDataSet = new DataSet();
            string _Query = "";

            ErrorMessage = "";

            //Get data
            _Query = "	SELECT CODAReference,GridOwnerName,TalexusAmount,TalexusAmountError,CODAAmount	FROM [BudgetMeter].[vw_ErrorData] ";
            try
            {
                _SQLConnection = GetConnection(); //Fix KVH: Central use of config parameter readings

                _SqlCommand = new SqlCommand(_Query, _SQLConnection);
                _SqlDataAdapter = new SqlDataAdapter(_SqlCommand);
                _SqlDataAdapter.Fill(_BudgetMeterDataSet);
                _BudgetMeterErrorList = new List<BudgetMeterError>();
                foreach (DataRow _row in _BudgetMeterDataSet.Tables[0].Rows)
                {
                    BudgetMeterError _Budget = new BudgetMeterError(_row["CODAReference"].ToString(),
                                                                    _row["GridOwnerName"].ToString(),
                                                                    Convert.ToDecimal(_row["TalexusAmount"]),
                                                                    Convert.ToDecimal(_row["CODAAmount"]),
                                                                    Convert.ToDecimal(_row["TalexusAmountError"]));
                    _BudgetMeterErrorList.Add(_Budget);
                }

            }
            catch (SqlException ex)
            {
                ErrorMessage = "Error Getting BugetMeter Error Data \n";
                //ErrorMessage += ex.ToString();
            }
            catch (InvalidOperationException ex)
            {
                ErrorMessage = "Error Getting BugetMeter Error Data \n";
                //ErrorMessage += ex.ToString();
            }
            finally
            {
                if (_SqlDataAdapter != null)
                    _SqlDataAdapter.Dispose();
                if (_SqlCommand != null)
                    _SqlCommand.Dispose();
                if (_BudgetMeterDataSet != null)
                    _BudgetMeterDataSet.Dispose();
                if (_SQLConnection.State == ConnectionState.Open)
                    _SQLConnection.Close();
            }

            return _BudgetMeterErrorList;
        }

        public List<BudgetMeterError> GetBudgetMeterErrorsDetails(string CODAReference, out string ErrorMessage)
        {
            //Variables
            List<BudgetMeterError> _BudgetMeterErrorList = null;
            SqlConnection _SQLConnection = null;
            SqlCommand _SqlCommand = null;
            SqlDataAdapter _SqlDataAdapter = null;
            DataSet _BudgetMeterDataSet = new DataSet();
            string _Query = "";

            ErrorMessage = "";

            //Get data
            _Query = "	SELECT ID,CODAReference,GridOwnerName, EAN,ErrorMessage,MasterdataChecked,TalexusAmount,CODAAmount,ChargeDate	FROM [BudgetMeter].[vw_ErrorDataDetails] WHERE CODAReference ='" + CODAReference + "'";
            try
            {
                _SQLConnection = GetConnection(); //Fix KVH: Central use of config parameter readings

                _SqlCommand = new SqlCommand(_Query, _SQLConnection);
                _SqlDataAdapter = new SqlDataAdapter(_SqlCommand);
                _SqlDataAdapter.Fill(_BudgetMeterDataSet);
                _BudgetMeterErrorList = new List<BudgetMeterError>();
                foreach (DataRow _row in _BudgetMeterDataSet.Tables[0].Rows)
                {
                    BudgetMeterError _Budget = new BudgetMeterError(_row["CODAReference"].ToString(),
                                                                    _row["GridOwnerName"].ToString(),
                                                                    Convert.ToDecimal(_row["TalexusAmount"]),
                                                                    Convert.ToDecimal(_row["CODAAmount"]),
                                                                    _row["EAN"].ToString(),
                                                                    _row["ErrorMessage"].ToString(),
                                                                    Convert.ToBoolean(_row["MasterdataChecked"]),
                                                                    _row["ID"].ToString(),
                                                                   Convert.ToDateTime( _row["ChargeDate"]));
                    _BudgetMeterErrorList.Add(_Budget);
                }

            }
            catch (SqlException ex)
            {
                ErrorMessage = "Error Getting BugetMeter Error Data \n";
                //ErrorMessage += ex.ToString();
            }
            catch (InvalidOperationException ex)
            {
                ErrorMessage = "Error Getting BugetMeter Error Data \n";
                //ErrorMessage += ex.ToString();
            }
            finally
            {
                if (_SqlDataAdapter != null)
                    _SqlDataAdapter.Dispose();
                if (_SqlCommand != null)
                    _SqlCommand.Dispose();
                if (_BudgetMeterDataSet != null)
                    _BudgetMeterDataSet.Dispose();
                if (_SQLConnection.State == ConnectionState.Open)
                    _SQLConnection.Close();
            }

            return _BudgetMeterErrorList;
        }
           
    }
    
}