using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eneco.Invest.WebUI.Models
{
    public class UploadModel
    {
        private DateTime _isabelLastUpload;
        private DateTime _axaptaLastSync;

        [DisplayName("Last upload")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime IsabelLastUpload 
        { 
            get 
                {
                    if (_isabelLastUpload == DateTime.MinValue)
                        _isabelLastUpload = DateTime.Now;
                    return _isabelLastUpload;
                }
            set { _isabelLastUpload = value;  }
        }

        [DisplayName("Last sync")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime AxaptaLastSync
        {
            get
            {
                if (_axaptaLastSync == DateTime.MinValue)
                    _axaptaLastSync = DateTime.Now;
                return _axaptaLastSync;
            }
            set { _axaptaLastSync = value; }
        }

        public HttpPostedFileBase IsabelFile { get; set; }

        public string ProcessProgression { get; set; }
    }
}