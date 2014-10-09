using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileGenerator.Logic
{
    public interface IVendorDA
    {
        List<Vendor> GetVendors(int database, out string ErrorMessage);
    }
}
