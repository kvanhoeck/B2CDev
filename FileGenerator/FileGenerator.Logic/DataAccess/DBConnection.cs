using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

namespace FileGenerator.Logic
{
    public class DBConnection
    {
        /// <summary>
        /// Declaration
        /// </summary>
        private string _ConnectionString;
        private SqlConnection _SQLConnection;
        private string _ConnectionError;


        public DBConnection(int database)
        {
            switch(database)
            {
                case 0:
                    _ConnectionString = ConfigurationManager.ConnectionStrings["B2C_ACC"].ConnectionString;
                    break;
                case 1:
                    _ConnectionString = ConfigurationManager.ConnectionStrings["B2C_PRD"].ConnectionString;
                    break;
                case 2:
                    _ConnectionString = ConfigurationManager.ConnectionStrings["B2B_ACC"].ConnectionString;
                    break;
               case 3:
                    _ConnectionString = ConfigurationManager.ConnectionStrings["B2B_PRD"].ConnectionString;
                    break;
            }
            
            // Intitialize the connection
            InitializeConnection();
        }

        /// <summary>
        /// Initialize the SQL Connection
        /// </summary>
        private void InitializeConnection()
        {
            try
            {

                _SQLConnection = new SqlConnection(_ConnectionString);
                _SQLConnection.Open();

            }
            catch (SqlException ex)
            {
                _ConnectionError += "Connection to: " + _ConnectionString + " failed! \n";
                _ConnectionError += ex.Message + "\n";

            }
        }

        /// <summary>
        /// Check connection is open
        /// </summary>
        /// <returns>Boolean: true = connection open, false = connection is closed</returns>
        public Boolean IsConnectionOpen()
        {
            if (_SQLConnection.State == ConnectionState.Open)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Close the connection
        /// </summary>
        public void CloseConnection()
        {
            if (IsConnectionOpen())
                _SQLConnection.Close();
        }

        /// <summary>
        /// Getter Connection.
        /// </summary>
        public SqlConnection Connection()
        {
            return _SQLConnection;
        }

        /// <summary>
        /// Return the connection Erorrs
        /// </summary>
        /// <returns></returns>
        public string ConnectionError()
        {
            return _ConnectionError;
        }

    }
}
