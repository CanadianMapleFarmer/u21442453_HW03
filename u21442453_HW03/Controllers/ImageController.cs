using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u21442453_HW03.Models;

namespace u21442453_HW03.Controllers
{
    public class ImageController : Controller
    {
        List<FileModel> fileArr = new List<FileModel>();
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ImagePage()
        {
            string rootPath = Server.MapPath("~/Media/Images");


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
            return RedirectToAction("ImagePage");
        }

        [HttpPost]
        public ActionResult Download(string dPath)
        {
            FileInfo f = new FileInfo(dPath);
            string file = dPath;
            string ct = MimeMapping.GetMimeMapping(f.Name);

            RedirectToAction("ImagePage");
            return File(file, ct, Path.GetFileName(file));
        }
    }
}