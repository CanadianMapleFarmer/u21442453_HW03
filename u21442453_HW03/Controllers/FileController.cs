using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u21442453_HW03.Models;

namespace u21442453_HW03.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FilePage()
        {
            string rootPath = Server.MapPath("~/Media/Documents");

            List<FileModel> fileArr = new List<FileModel>();

            string fN = "";
            string fP = "";

            foreach (string file in Directory.EnumerateFiles(rootPath))
            {
                fP = file.Replace(@"\", @"/");
                fN = fP.Split('/').Last();

                System.Diagnostics.Debug.WriteLine($"filePath: {fP} \n fileName: {fN}"); 
                fileArr.Add(new FileModel(fN, fP));
            };

            return View(fileArr);
        }
    }
}