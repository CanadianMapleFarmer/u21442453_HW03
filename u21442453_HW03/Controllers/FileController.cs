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
        //Create a FileModel List<>.
        List<FileModel> fileArr = new List<FileModel>();
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FilePage()
        {
            //We know where the document files are saved. 
            //Set rootPath for Directory Enumeration use.
            string rootPath = Server.MapPath("~/Media/Documents");


            string fN = "";
            string fP = "";

            //Enumerate files in the rootPath and add to fileArr List<>.
            foreach (string file in Directory.EnumerateFiles(rootPath))
            {
                fP = file.Replace(@"\", @"/");
                fN = fP.Split('/').Last();
 
                fileArr.Add(new FileModel(fN, fP));
            };
            //Return to View().
            return View(fileArr);
        }


        //Delete method.
        [HttpPost]
        public ActionResult Delete(string path)
        {
            //Check if file exists from path parameter.
            if (System.IO.File.Exists(path))
            {
                try
                {
                    //Delete file.
                    System.IO.File.Delete(path);
                }
                catch (Exception ex)
                {
                    //Error handling
                    System.Diagnostics.Debug.WriteLine("Deletion failed:" + ex.Message);
                }

                for (int i = 0; i < fileArr.Count; i++)
                {
                    //Remove from fileArr.
                    if (fileArr[i].FilePath == path)
                    {
                        fileArr.RemoveAt(i);
                    }
                }
            }
            //Redirect back to "/File/FilePage"
            return RedirectToAction("FilePage");
        }

        //Download method.
        [HttpPost]
        public ActionResult Download(string dPath)
        {
            //Create a FileInfo object.
            FileInfo f = new FileInfo(dPath);
            string file = dPath;
            //Get MIME type from file.
            string ct = MimeMapping.GetMimeMapping(f.Name);

            //Redirect to "/File/FilePage"
            RedirectToAction("FilePage");
            //Return the corresponding file to View() for user to download.
            return File(file, ct, Path.GetFileName(file));
        }
    }
}