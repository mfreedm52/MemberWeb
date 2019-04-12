using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MemberDatabase;
using System.Configuration;

namespace Platform.Controllers
{
    public class DocumentController : Controller
    {
        // GET: Document
        public ActionResult Index()
        {
            string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            MemberContext mem = new MemberContext(connection);
            var unitOfWork = new GenericRepository.UnitOfWork(mem);

            //            List<File> fileList = mem.myFiles.Where(f => f.FileType != "Photo").ToList();
            List<File> fileList = unitOfWork.Repository<File>().Query().Get().ToList();

            return View("Documents", fileList);
        }

        [HttpPost]
        public ActionResult UploadDocument(HttpPostedFileBase file)
        {


            string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            MemberContext mem = new MemberContext(connection);
            var unitOfWork = new GenericRepository.UnitOfWork(mem);

            if (file != null && file.ContentLength > 0)
            {
                string path = "C:\\Users\\mfreedm\\Documents\\Visual Studio 2015\\Projects\\Platform\\Platform\\Temp";
                try
                {
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }


                MemberDatabase.File newFile = new MemberDatabase.File();

                newFile.FilePath = path;
                newFile.Description = "File Uploaded";

                unitOfWork.Repository<File>().Insert(newFile);
                unitOfWork.Save();
              

            }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            List<File> fileList = unitOfWork.Repository<File>().Query().Get().ToList();

            //problems with losing viewbag after using redirectToaction
            return View("Documents", fileList);
        }

        public virtual ActionResult Delete(int id)
        {
            string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            MemberContext mem = new MemberContext(connection);
            var unitOfWork = new GenericRepository.UnitOfWork(mem);

            unitOfWork.Repository<File>().Delete(id);
            unitOfWork.Save();


            return RedirectToAction("Index");

        }



    }
}