using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MemberDatabase;
using Platform.Models;
using System.Threading.Tasks;
using System.Configuration;

namespace Platform.Controllers
{
    public class ImageController : Controller
    {

        // GET: Contact
        public ActionResult Index(MemberContext mem)
        {

            var unitOfWork = new GenericRepository.UnitOfWork(mem);

            List<File> fileList = unitOfWork.Repository<MemberDatabase.File>().Query().Get().ToList();

            return View(fileList);
        }

        [HttpGet]
        public ActionResult Add()
        {

            return View(new MemberDatabase.File());
        }


        [HttpPost]
        public ActionResult UploadPhoto(HttpPostedFileBase file, MemberContext mem)
        {
            string serverPath = "";
            string filePath = "";

            if (file != null && file.ContentLength > 0)
                try
                {
                    //useful, but actually not used here. I eventually figured out changing permissions on the folder
                    //this returned mfreedm
                    string user = HttpContext.User.Identity.Name;

                    serverPath = "/Platform/Images/" + file.FileName;
                    filePath = Server.MapPath(serverPath);
                    file.SaveAs(filePath);
                    ViewBag.Message = "File uploaded successfully";

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            var unitOfWork = new GenericRepository.UnitOfWork(mem);

            MemberDatabase.File photo = new MemberDatabase.File();

            photo.FilePath = serverPath;
            photo.FileType = "Photo";
            photo.UploadDate = DateTime.Now;
            photo.Description = "uploaded document";

            unitOfWork.Repository<MemberDatabase.File>().Insert(photo);
            unitOfWork.Save();


            List<File> fileList = unitOfWork.Repository<MemberDatabase.File>().Query().Get().ToList();

            return View("Index", fileList);

        }


    }
}