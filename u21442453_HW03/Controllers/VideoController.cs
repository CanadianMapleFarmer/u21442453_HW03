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
        //Create a FileModel List<>.
        List<FileModel> fileArr = new List<FileModel>();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult VideoPage()
        {
            //We know where the video files are saved. 
            //Set rootPath for Directory Enumeration use.
            string rootPath = Server.MapPath("~/Media/Videos");

            string fN = "";
            string fP = "";
            string tPath = "";
            string tempfN = "";

            //Enumerate files in the rootPath and add to fileArr List<>.
            foreach (string file in Directory.EnumerateFiles(rootPath))
            {
                fP = file.Replace(@"\", @"/");
                fN = fP.Split('/').Last();
                tempfN = fN.Split('.').First();
                fileArr.Add(new FileModel(fN, fP));
                //I used a cool tool that can create thumbnail jpegs for all the videos in the "./Media/Videos"
                //folder. It uses the video file path and creates a thumbnail in "./Media/Images/Thumbnails"

                //Check if the image already exists.
                if (!System.IO.File.Exists($"~/Media/Images/Thumbnails/{tempfN}.jpg"))
                {
                    //Initialize the tool.
                var ffmpeg = new FFMpegConverter();  
                    //Pass the paramaters for the tool to create the thumbnail.
                ffmpeg.GetVideoThumbnail(fP, Server.MapPath($"~/Media/Images/Thumbnails/{tempfN}.jpg"));
                }
            };
            //Return tto View().
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
                    //Delete file for "./Media/Videos".
                    System.IO.File.Delete(path);
                    string fN = path.Split('/').Last();
                    string imgP = fN.Split('.').First() + ".jpg";
                    string temp = Server.MapPath($"~/Media/Images/Thumbnails/{imgP}");
                    //Delete thumbnail from "./Media/Images/Thumbnails"
                    System.IO.File.Delete(temp);
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
            //Redirect back to "/Video/VideoPage"
            return RedirectToAction("VideoPage");
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

            //Redirect to "/Video/VideoPage"
            RedirectToAction("VideoPage");
            //Return the corresponding file to View() for user to download.
            return File(file, ct, Path.GetFileName(file));
        }
    }
}