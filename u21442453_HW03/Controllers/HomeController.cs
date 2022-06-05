using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace u21442453_HW03.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            try
            {
                string fType = "";
                if (file != null)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string filePath = "";
                    string filter = file.ContentType.Split('/').Last();
                    switch (filter)
                    {
                        case "jpg":
                            filePath = Path.Combine(Server.MapPath("~/Media/Images"), fileName);
                            fType = "Image";
                            break;
                        case "jpeg":
                            filePath = Path.Combine(Server.MapPath("~/Media/Images"), fileName);
                            fType = "Image";
                            break;
                        case "png":
                            filePath = Path.Combine(Server.MapPath("~/Media/Images"), fileName);
                            fType = "Image";
                            break;
                        case "webp":
                            filePath = Path.Combine(Server.MapPath("~/Media/Images"), fileName);
                            fType = "Image";
                            break;
                        case "tiff":
                            filePath = Path.Combine(Server.MapPath("~/Media/Images"), fileName);
                            fType = "Image";
                            break;
                        case "svg":
                            filePath = Path.Combine(Server.MapPath("~/Media/Images"), fileName);
                            fType = "Image";
                            break;
                        case "ico":
                            filePath = Path.Combine(Server.MapPath("~/Media/Images"), fileName);
                            fType = "Image";
                            break;
                        case "gif":
                            filePath = Path.Combine(Server.MapPath("~/Media/Images"), fileName);
                            fType = "Image";
                            break;
                        case "bmp":
                            filePath = Path.Combine(Server.MapPath("~/Media/Images"), fileName);
                            fType = "Image";
                            break;
                        case "avif":
                            filePath = Path.Combine(Server.MapPath("~/Media/Images"), fileName);
                            fType = "Image";
                            break;
                        case "avi":
                            filePath = Path.Combine(Server.MapPath("~/Media/Videos"), fileName);
                            fType = "Video";
                            break;
                        case "mp4":
                            filePath = Path.Combine(Server.MapPath("~/Media/Videos"), fileName);
                            fType = "Video";
                            break;
                        case "ogg":
                            filePath = Path.Combine(Server.MapPath("~/Media/Videos"), fileName);
                            fType = "Video";
                            break;
                        case "mp2t":
                            filePath = Path.Combine(Server.MapPath("~/Media/Videos"), fileName);
                            fType = "Video";
                            break;
                        case "webm":
                            filePath = Path.Combine(Server.MapPath("~/Media/Videos"), fileName);
                            fType = "Video";
                            break;
                        case "mov":
                            filePath = Path.Combine(Server.MapPath("~/Media/Videos"), fileName);
                            fType = "Video";
                            break;
                        case "mkv":
                            filePath = Path.Combine(Server.MapPath("~/Media/Videos"), fileName);
                            fType = "Video";
                            break;
                        case "":
                            filePath = Path.Combine(Server.MapPath("~/Media/Videos"), fileName);
                            fType = "Video";
                            break;
                        default:
                            filePath = Path.Combine(Server.MapPath("~/Media/Documents"), fileName);
                            fType = "Document";
                            break;
                    }
                    file.SaveAs(filePath);
                }
                ViewBag.Message = $"You successfully uploaded a(n): {fType}";
                ViewBag.Style = "bg-success";
                return View();
            }
            catch
            {
                ViewBag.Message = "File uploaded failed!";
                ViewBag.Style = "bg-danger";
                return View();
            }
        }

    }
}