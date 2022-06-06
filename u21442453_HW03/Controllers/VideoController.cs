using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u21442453_HW03.Models;
using NReco.VideoConverter;

namespace u21442453_HW03.Controllers
{
    public class VideoController : Controller
    {

        List<FileModel> fileArr = new List<FileModel>();
        // GET: Video
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult VideoPage()
        {
            string rootPath = Server.MapPath("~/Media/Videos");

            string fN = "";
            string fP = "";
            string tPath = "";
            string tempfN = "";
            foreach (string file in Directory.EnumerateFiles(rootPath))
            {
                fP = file.Replace(@"\", @"/");
                fN = fP.Split('/').Last();
                tempfN = fN.Split('.').First();
                fileArr.Add(new FileModel(fN, fP));
                if (!System.IO.File.Exists($"~/Media/Images/Thumbnails/{tempfN}.jpg"))
                {
                var ffmpeg = new FFMpegConverter();      
                ffmpeg.GetVideoThumbnail(fP, Server.MapPath($"~/Media/Images/Thumbnails/{tempfN}.jpg"));
                }
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
                    string fN = path.Split('/').Last();
                    string imgP = fN.Split('.').First() + ".jpg";
                    string temp = Server.MapPath($"~/Media/Images/Thumbnails/{imgP}");
                    System.IO.File.Delete(temp);
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
            return RedirectToAction("VideoPage");
        }

        [HttpPost]
        public ActionResult Download(string dPath)
        {
            FileInfo f = new FileInfo(dPath);
            string file = dPath;
            string ct = MimeMapping.GetMimeMapping(f.Name);

            RedirectToAction("VideoPage");
            return File(file, ct, Path.GetFileName(file));
        }
    }
}