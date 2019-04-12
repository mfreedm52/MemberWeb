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
    public class GroupController : Controller
    {

        // GET: Contact
        public ActionResult Index(MemberContext mem)
        {
            var unitOfWork = new GenericRepository.UnitOfWork(mem);

            List<Group> groupList = unitOfWork.Repository<Group>().Query().Get().ToList();

            int members = groupList[1].Members.Count();

            return View(groupList);

        }

        [HttpGet]
        public ActionResult Add()
        {

            return View(new MemberDatabase.Group());
        }


        [HttpPost]
        public ActionResult Add(MemberDatabase.Group myGroup)
        {
            string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            MemberContext mem = new MemberContext(connection);

            var unitOfWork = new GenericRepository.UnitOfWork(mem);

            unitOfWork.Repository<MemberDatabase.Group>().Insert(myGroup);


            unitOfWork.Save();
            List<Group> groupList = unitOfWork.Repository<Group>().Query().Get().ToList();


            return View("Index", groupList);
        }


    }
}