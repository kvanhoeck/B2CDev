using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileGenerator.Logic
{
    public class Vendor
    {
        #region Variables
        private string _VendorEAN;

        #endregion

        #region Constructors
        public Vendor(string VendorEAN)
        {
            _VendorEAN = VendorEAN;
        }
        #endregion

        #region Getter / Setter
        public string VendorEAN
        {
            get { return _VendorEAN; }
    }
        #endregion
    }
}
