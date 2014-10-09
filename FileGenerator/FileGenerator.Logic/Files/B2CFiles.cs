using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using Microsoft.SqlServer.Management.Smo;

namespace FileGenerator.Logic
{
    public class B2CFiles
    {
        /// <summary>
        /// Create a soctar file
        /// </summary>
        /// <param name="dumpDate">Date: what is the max date that is needed for the dump</param>
        /// <param name="deliveryDate">Date: what is the date we deliver the dump</param>
        /// <param name="database">int: the database to execute 0 = Accept 1 = Production</param>
        /// <param name="type">int: 0 = FullDump - 1 = MutationFile</param>
        /// <param name="filePath">string: location to save the file</param>
        /// <returns>String: errormessage</returns
        public string CreateSoctarFile(DateTime dumpDate, DateTime deliveryDate,int database,int type,string filePath)
        {
             // Declarations 
            string _ErrorMessage = "";
            int _ErrorNumber = 0;
            SqlCommand _BuildDumpFile = null;

            // Create Connection
            DBConnection _connection = new DBConnection(database);
           
            // Check connection to SQL is open
            if (!_connection.IsConnectionOpen())
            {
                // Error handling
                _ErrorMessage += _connection.ConnectionError();
                return _ErrorMessage;
            }
            try
            {
                // Make a temp Folder in the file location
                if (Directory.Exists(filePath + "\\Tmp") == false)
                {
                    Directory.CreateDirectory(filePath + "\\Tmp");
                }
                // Get SP and parameters
                SqlParameter _Type = new SqlParameter("@Type", SqlDataType.Int);
                _Type.Value = type;
                _Type.Direction = System.Data.ParameterDirection.Input;
                SqlParameter _Database = new SqlParameter("@Database", SqlDataType.Int);
                _Database.Value = database;
                _Database.Direction = System.Data.ParameterDirection.Input;
                SqlParameter _ErrorNo = new SqlParameter("@ErrorNumber", SqlDataType.Int);
                _ErrorNo.Direction = System.Data.ParameterDirection.ReturnValue;
                SqlParameter _FilePath = new SqlParameter("@FilePath", SqlDataType.VarChar);
                _FilePath.Value = filePath;
                _FilePath.Direction = System.Data.ParameterDirection.Input;
                SqlParameter _DumpDate = new SqlParameter("@DumpDate", SqlDataType.Date);
                _DumpDate.Value = dumpDate;
                _DumpDate.Direction = System.Data.ParameterDirection.Input;
                SqlParameter _DeliveryDate = new SqlParameter("@DeliveryDate", SqlDataType.Date);
                _DeliveryDate.Value = deliveryDate;
                _DeliveryDate.Direction = System.Data.ParameterDirection.Input;
                _BuildDumpFile = new SqlCommand("[Soctar].[USP_CreateFile]", _connection.Connection());
                _BuildDumpFile.CommandType = System.Data.CommandType.StoredProcedure;
                _BuildDumpFile.Parameters.Add(_Type);
                _BuildDumpFile.Parameters.Add(_Database);
                _BuildDumpFile.Parameters.Add(_ErrorNo);
                _BuildDumpFile.Parameters.Add(_DumpDate);
                _BuildDumpFile.Parameters.Add(_DeliveryDate);
                _BuildDumpFile.Parameters.Add(_FilePath);
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
            finally
            {

                if (_BuildDumpFile != null)
                    _BuildDumpFile.Dispose();

                if (_connection.IsConnectionOpen())
                    _connection.CloseConnection();

            }
             // Return Message
            return _ErrorMessage;
        }

        /// <summary>
        /// Build an AX dump files
        /// </summary>
        /// <param name="type">int: file to generate 0 = NAW Dump, 1= Contract Dump, 2= EAN Dump,  3=Renewal Dump</param>
        /// <param name="database">int: the database to execute 0 = Accept 1 = Production</param>
        /// <param name="filePath"> string: location to save the file</param>
        /// <param name="month"> int: month for the renewal</param>
        /// <returns>string: errormessage</returns>
        public string BuildDumpFile(int type, int database, string filePath, int month)
        {
            string _ErrorMessage = "";
            int _ErrorNumber = 0;
            SqlCommand _AXDump = null;


            // Create Connection
            DBConnection _connection = new DBConnection(database);
            // Check connection to SQL is open
            if (!_connection.IsConnectionOpen())
            {
                // Error handling
                _ErrorMessage += _connection.ConnectionError();
                return _ErrorMessage;
            }
            else

              try
                {
                    SqlParameter _Type = new SqlParameter("@Type", SqlDataType.Int);
                    _Type.Value = type;
                    _Type.Direction = System.Data.ParameterDirection.Input;
                    SqlParameter _Database = new SqlParameter("@Database", SqlDataType.Int);
                    _Database.Value = database;
                    _Database.Direction = System.Data.ParameterDirection.Input;
                    SqlParameter _FilePath = new SqlParameter("@FilePath", SqlDataType.VarChar);
                    _FilePath.Value = filePath;
                    _FilePath.Direction = System.Data.ParameterDirection.Input;
                    SqlParameter _ErrorNo = new SqlParameter("@ErrorNumber", SqlDataType.Int);
                    _ErrorNo.Direction = System.Data.ParameterDirection.ReturnValue;
                    SqlParameter _Month = new SqlParameter("@Month", SqlDataType.Int);
                    _Month.Value = month;
                    _Month.Direction = System.Data.ParameterDirection.Input;

                    _AXDump = new SqlCommand("[DumpData].[USP_CreateFile]", _connection.Connection());
                    _AXDump.CommandTimeout = 240;

                    _AXDump.CommandType = System.Data.CommandType.StoredProcedure;
                    _AXDump.Parameters.Add(_Type);
                    _AXDump.Parameters.Add(_Database);
                    _AXDump.Parameters.Add(_ErrorNo);
                    _AXDump.Parameters.Add(_FilePath);
                    _AXDump.Parameters.Add(_Month);

                    _AXDump.ExecuteNonQuery();
                    _ErrorNo.Value = (int)_AXDump.Parameters["@ErrorNumber"].Value;

                    if (_ErrorNumber != 0)
                    {
                        _ErrorMessage += "Error executing Create file process\n";
                        _ErrorMessage += _ErrorNumber.ToString() + " \n";

                    }

                }
                catch (SqlException ex)
                {
                    _ErrorMessage += "Error executing Build process\n";
                    _ErrorMessage += ex.Message;
                    return _ErrorMessage;
                }
                catch (FormatException ex)
                {
                    _ErrorMessage += "Error executing Build process\n";
                    _ErrorMessage += ex.Message;
                    return _ErrorMessage;
                }
                catch (FileNotFoundException ex)
                {
                    _ErrorMessage += "Error executing Build process\n";
                    _ErrorMessage += ex.Message;
                    return _ErrorMessage;
                }
                finally
                {

                    if (_AXDump != null)
                        _AXDump.Dispose();

                    if (_connection.IsConnectionOpen())
                        _connection.CloseConnection();

                }
            return _ErrorMessage;
        }

        /// <summary>
        /// Build a AX Portfolio file
        /// </summary>
        /// <param name="database">int: the database to execute 0 = Accept 1 = Production</param>
        /// <param name="filePath"> string: location to save the file</param>
        /// <returns>string: errormessage</returns>
        public string BuildPortfolio(int database, string filePath)
        {
            string _ErrorMessage = "";
            int _ErrorNumber = 0;
            SqlCommand _AXDump = null;


            // Create Connection
            DBConnection _connection = new DBConnection(database);
            // Check connection to SQL is open
            if (!_connection.IsConnectionOpen())
            {
                // Error handling
                _ErrorMessage += _connection.ConnectionError();
                return _ErrorMessage;
            }
            else


                try
                {
                    SqlParameter _Sourcing = new SqlParameter("@Sourcing", SqlDataType.VarChar);
                    _Sourcing.Value = "1";
                    _Sourcing.Direction = System.Data.ParameterDirection.Input;
                    SqlParameter _Database = new SqlParameter("@Database", SqlDataType.Int);
                    _Database.Value = database;
                    _Database.Direction = System.Data.ParameterDirection.Input;
                    SqlParameter _FilePath = new SqlParameter("@FilePath", SqlDataType.VarChar);
                    _FilePath.Value = filePath;
                    _FilePath.Direction = System.Data.ParameterDirection.Input;
                    SqlParameter _ErrorNo = new SqlParameter("@ErrorNumber", SqlDataType.Int);
                    _ErrorNo.Direction = System.Data.ParameterDirection.ReturnValue;


                    _AXDump = new SqlCommand("[dumpData].[USP_CreateSourcingFile]", _connection.Connection());
                    _AXDump.CommandTimeout = 240;

                    _AXDump.CommandType = System.Data.CommandType.StoredProcedure;
                    _AXDump.Parameters.Add(_Sourcing);
                    _AXDump.Parameters.Add(_Database);
                    _AXDump.Parameters.Add(_ErrorNo);
                    _AXDump.Parameters.Add(_FilePath);


                    _AXDump.ExecuteNonQuery();
                    _ErrorNo.Value = (int)_AXDump.Parameters["@ErrorNumber"].Value;

                    if (_ErrorNumber != 0)
                    {
                        _ErrorMessage += "Error executing Create file process\n";
                        _ErrorMessage += _ErrorNumber.ToString() + " \n";

                    }

                }
                catch (SqlException ex)
                {
                    _ErrorMessage += "Error executing Build process\n";
                    _ErrorMessage += ex.Message;
                    return _ErrorMessage;
                }
                catch (FormatException ex)
                {
                    _ErrorMessage += "Error executing Build process\n";
                    _ErrorMessage += ex.Message;
                    return _ErrorMessage;
                }
                catch (FileNotFoundException ex)
                {
                    _ErrorMessage += "Error executing Build process\n";
                    _ErrorMessage += ex.Message;
                    return _ErrorMessage;
                }
                finally
                {
                    
                    if (_AXDump != null)
                        _AXDump.Dispose();

                    if (_connection.IsConnectionOpen())
                        _connection.CloseConnection();

                }
            return _ErrorMessage;
        }
    
        /// <summary>
        /// Process the Answer file from soctar
        /// </summary>
        /// <param name="database">int: the database to execute 0 = Accept 1 = Production</param>
        /// <param name="filePath">string: location to save the file</param>
        /// <param name="UploadFile">string: file to upload</param>
        /// <returns></returns>
        public string BuildAnswerSoctarFile(int database, string filePath, string UploadFile)
        {
            string  _ErrorMessage = "";
            int     _ErrorNumber = 0;

            SqlCommand _AXDump = null;

            // Create Connection
            DBConnection _connection = new DBConnection(database);
            // Check connection to SQL is open
            if (!_connection.IsConnectionOpen())
            {
                // Error handling
                _ErrorMessage += _connection.ConnectionError();
                return _ErrorMessage;
            }
            else
            {
                try
                {
                    SqlParameter _SourceFile= new SqlParameter("@SourceFile", SqlDataType.VarChar);
                    _SourceFile.Value = UploadFile;
                    _SourceFile.Direction = System.Data.ParameterDirection.Input;
                    
                    SqlParameter _Database = new SqlParameter("@Database", SqlDataType.Int);
                    _Database.Value = database;
                    _Database.Direction = System.Data.ParameterDirection.Input;
                    
                    SqlParameter _FilePath = new SqlParameter("@FilePath", SqlDataType.VarChar);
                    _FilePath.Value = filePath;
                    _FilePath.Direction = System.Data.ParameterDirection.Input;
                    
                    SqlParameter _ErrorNo = new SqlParameter("@ErrorNumber", SqlDataType.Int);
                    _ErrorNo.Direction = System.Data.ParameterDirection.ReturnValue;


                    _AXDump = new SqlCommand("[Soctar].[USP_LoadAnswerFile]", _connection.Connection());
                    _AXDump.CommandTimeout = 240;

                    _AXDump.CommandType = System.Data.CommandType.StoredProcedure;
                    _AXDump.Parameters.Add(_SourceFile);
                    _AXDump.Parameters.Add(_Database);
                    _AXDump.Parameters.Add(_ErrorNo);
                    _AXDump.Parameters.Add(_FilePath);


                    _AXDump.ExecuteNonQuery();
                    _ErrorNo.Value = (int)_AXDump.Parameters["@ErrorNumber"].Value;

                    if (_ErrorNumber != 0)
                    {
                        _ErrorMessage += "Error executing Create file process\n";
                        _ErrorMessage += _ErrorNumber.ToString() + " \n";

                    }


                }
                catch (SqlException ex)
                {
                    _ErrorMessage += "Error executing Build process\n";
                    _ErrorMessage += ex.Message;
                    return _ErrorMessage;
                }
                catch (FormatException ex)
                {
                    _ErrorMessage += "Error executing Build process\n";
                    _ErrorMessage += ex.Message;
                    return _ErrorMessage;
                }
                catch (FileNotFoundException ex)
                {
                    _ErrorMessage += "Error executing Build process\n";
                    _ErrorMessage += ex.Message;
                    return _ErrorMessage;
                }
                finally
                {

                    if (_AXDump != null)
                        _AXDump.Dispose();

                    if (_connection.IsConnectionOpen())
                        _connection.CloseConnection();

                }
            }
            return _ErrorMessage;
        }
    }
}
