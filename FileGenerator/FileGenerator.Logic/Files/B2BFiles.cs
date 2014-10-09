using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using Microsoft.SqlServer.Management.Smo;

namespace FileGenerator.Logic
{
    public class B2BFiles
    {
        /// <summary>
        /// Build the price evolution file
        /// </summary>
        /// <param name="startPeriod">Date: What is the startdate of the period to report</param>
        /// <param name="endPeriod">Date: What is then enddate of the period to report</param>
        /// <param name="database">Int: The database to execute 0 = Accept 1 = Production</param>
        /// <param name="serviceType">Int: The service type 0 = Electricity 1 = Gas</param>
        /// <param name="region">Int: The region 0 = Brussels 1 = Wallonia</param>
        /// <param name="filePath">String: Location to save the file</param>
        /// <param name="vendorGLN">String GLN for the vendor</param>
        /// <returns>String: ErrorMessage</returns>
        public string BuildPriceEvolution(DateTime startPeriod, DateTime endPeriod,int database,int serviceType,int region,string filePath, string vendorGLN)
        {
            
            // Declarations 
            string _ErrorMessage = "";
            int _ErrorNumber = 0;
             SqlCommand _BuildDumpFile = null;

            // Create Connection
            DBConnection _connection = new DBConnection(database+2);
           
            // Check connection to SQL is open
            if (!_connection.IsConnectionOpen())
            {
                // Error handling
                _ErrorMessage += _connection.ConnectionError();
                return _ErrorMessage;
            }
            try
            {
                 // Get SP and parameters
                SqlParameter _ServiceType = new SqlParameter("@ServiceType", SqlDataType.Char);
                _ServiceType.Value = serviceType == 0 ? "E" : "G";
                _ServiceType.Direction = System.Data.ParameterDirection.Input;
                SqlParameter _Region = new SqlParameter("@Region", SqlDataType.Char);
                _Region.Value = region == 0 ? "B" : "W";
                _Region.Direction = System.Data.ParameterDirection.Input;
                SqlParameter _Database = new SqlParameter("@Database", SqlDataType.Int);
                _Database.Value = database;
                _Database.Direction = System.Data.ParameterDirection.Input;
                SqlParameter _ErrorNo = new SqlParameter("@ErrorNumber", SqlDataType.Int);
                _ErrorNo.Direction = System.Data.ParameterDirection.ReturnValue;
                SqlParameter _FilePath = new SqlParameter("@FilePath", SqlDataType.VarChar);
                _FilePath.Value = filePath;
                _FilePath.Direction = System.Data.ParameterDirection.Input;
                SqlParameter _StartDate = new SqlParameter("@StartPeriod", SqlDataType.Date);
                _StartDate.Value = startPeriod;
                _StartDate.Direction = System.Data.ParameterDirection.Input;
                SqlParameter _EndDate = new SqlParameter("@EndPeriod", SqlDataType.Date);
                _EndDate.Value = endPeriod;
                _EndDate.Direction = System.Data.ParameterDirection.Input;
                SqlParameter _LevGLN = new SqlParameter("@LevGLN", SqlDataType.VarChar);
                _LevGLN.Value = vendorGLN;
                _LevGLN.Direction = System.Data.ParameterDirection.Input;
                _BuildDumpFile = new SqlCommand("[DumpData].[USP_CreatePriceEvolution]", _connection.Connection());
                _BuildDumpFile.CommandType = System.Data.CommandType.StoredProcedure;
                _BuildDumpFile.Parameters.Add(_ServiceType);
                _BuildDumpFile.Parameters.Add(_Region);
                _BuildDumpFile.Parameters.Add(_StartDate);
                _BuildDumpFile.Parameters.Add(_EndDate);
                _BuildDumpFile.Parameters.Add(_Database);
                _BuildDumpFile.Parameters.Add(_ErrorNo); ;
                _BuildDumpFile.Parameters.Add(_FilePath);
                _BuildDumpFile.Parameters.Add(_LevGLN);
                _BuildDumpFile.ExecuteNonQuery();
                _ErrorNumber = (int)_BuildDumpFile.Parameters["@ErrorNumber"].Value;
                if (_ErrorNumber != 0)
                {
                    _ErrorMessage += "Error executing Create file process\n";
                }
            }
            catch (SqlException ex)
            {
                _ErrorMessage += "Error executing Create file process\n";
                _ErrorMessage += ex.Message;
                return _ErrorMessage;
            }
            catch (FormatException ex)
            {
                _ErrorMessage += "Error executing Create file process\n";
                _ErrorMessage += ex.Message;
                return _ErrorMessage;
            }
            catch (DirectoryNotFoundException ex)
            {
                _ErrorMessage += "Error executing Create file process\n";
                _ErrorMessage += ex.Message;
                return _ErrorMessage;
            }
            catch (FileNotFoundException ex)
            {
                _ErrorMessage += "Error executing Create file process\n";
                _ErrorMessage += ex.Message;
                return _ErrorMessage;
            }
            finally
            {
                if (_BuildDumpFile != null)
                    _BuildDumpFile.Dispose();

                if (_connection.IsConnectionOpen())
                    _connection.CloseConnection();

            }

            // Return Result
            return _ErrorMessage;
        }
    }
}
