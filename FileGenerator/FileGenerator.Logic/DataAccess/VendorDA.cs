using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace FileGenerator.Logic
{
    public class VendorDA : IVendorDA
    {
        public List<Vendor> GetVendors(int database, out string ErrorMessage)
        {
            //Variables
            List<Vendor> _VendorList = null;
            SqlCommand _SqlCommand = null;
            SqlDataAdapter _SqlDataAdapter = null;
            DataSet _GLDataSet = new DataSet();
            string _Query = "";

            ErrorMessage = "";

            //Create connection
            DBConnection _Connection = new DBConnection(database + 2);

            if (!_Connection.IsConnectionOpen())
            {
                ErrorMessage = "Connection to: " + _Connection.ToString() + " is not open \n";
                ErrorMessage += _Connection.ConnectionError();
            }
            else
            {
                //Get data
                _Query = "SELECT ven.EANNo FROM DataModel.Vendor ven WHERE ven.VendorType = 'ESP'";
                try
                {
                    _SqlCommand = new SqlCommand(_Query, _Connection.Connection());
                    _SqlDataAdapter = new SqlDataAdapter(_SqlCommand);
                    _SqlDataAdapter.Fill(_GLDataSet);
                    _VendorList = new List<Vendor>();
                    foreach (DataRow _row in _GLDataSet.Tables[0].Rows)
                    {
                        Vendor _Ven = new Vendor(_row["EANNo"].ToString());
                        _VendorList.Add(_Ven);
                    }



                }
                catch (SqlException ex)
                {
                    ErrorMessage = "Error Getting Vendors \n";
                    ErrorMessage += ex.ToString();
                }
                catch (InvalidOperationException ex)
                {
                    ErrorMessage += ex.ToString();
                }
                finally
                {
                    if (_SqlDataAdapter != null)
                        _SqlDataAdapter.Dispose();
                    if (_SqlCommand != null)
                        _SqlCommand.Dispose();
                    if (_Connection.IsConnectionOpen())
                        _Connection.CloseConnection();
                }
            }


            return _VendorList;

        }
    }
}
