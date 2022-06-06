using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using u21442453_HW03.Models;

namespace u21442453_HW03.Controllers
{
    public class FileController : Controller
    {
        List<FileModel> fileArr = new List<FileModel>();
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FilePage()
        {
            string rootPath = Server.MapPath("~/Media/Documents");


            string fN = "";
            string fP = "";

            foreach (string file in Directory.EnumerateFiles(rootPath))
            {
                fP = file.Replace(@"\", @"/");
                fN = fP.Split('/').Last();
 
                fileArr.Add(new FileModel(fN, fP));
            };

            return View(fileArr);
        }

        [HttpPost]
        public ActionResult Delete(string path)
        {
            if (System.IO.File.Exists(path))
            {
                try
                {
                    System.IO.File.Delete(path);
                }
                catch (Exception ex)
                {

                    System.Diagnostics.Debug.WriteLine("Deletion failed:" + ex.Message);
                }

                for (int i = 0; i < fileArr.Count; i++)
                {
                    if (fileArr[i].FilePath == path)
                    {
                        fileArr.RemoveAt(i);
                    }
                }
            }
            return RedirectToAction("FilePage");
        }

        [HttpPost]
        public ActionResult Download(string dPath)
        {
            FileInfo f = new FileInfo(dPath);
            string file = dPath;
            string ct = MimeMapping.GetMimeMapping(f.Name);

            RedirectToAction("FilePage");
            return File(file, ct, Path.GetFileName(file));
        }
    }
}