using Eneco.Invest.Business;
using Eneco.Invest.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eneco.Invest.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload()
        {
            return View(new UploadModel());
        }

        [HttpPost]
        public ActionResult Upload(UploadModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.ProcessProgression += "Reading Isabel file...";
            
            byte[] uploadedFile = new byte[model.IsabelFile.InputStream.Length];
            //Onderstaande is niet nodig
            model.IsabelFile.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

            InvestBusiness business = new InvestBusiness();

            model.ProcessProgression += Environment.NewLine + "Parsing Isabel file...";
            UpdateModel<UploadModel>(model);
            business.UploadIsabelFile(uploadedFile);
            model.ProcessProgression += Environment.NewLine + "Saving Isabel file...";
            UpdateModel<UploadModel>(model);
            business.SaveIsabelFile();
            model.ProcessProgression += Environment.NewLine + "Matching Isabel file...";
            UpdateModel<UploadModel>(model);
            business.MatchIsabelFile();
            
            return View(model);
        }
    }
}
