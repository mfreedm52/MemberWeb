using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MemberDatabase;
using System.Threading.Tasks;
using System.Configuration;

namespace Platform.Controllers
{
    public class MemberController : Controller
    {

        // GET: Contact
        public ActionResult Index(string SearchString, MemberContext mem)
        {

            List<Member> memList = mem.DbSet<Member>().ToList();
            if (!String.IsNullOrEmpty(SearchString))
            {
                memList = memList.Where(p => p.FirstName == SearchString).ToList();
                   
            }
            return View(memList);
        }

        // GET: Contact
        public ActionResult Member(MemberContext mem)
        {
            List<Member> memList = mem.DbSet<Member>().ToList();
            return View(memList);
        }

        
         [HttpGet]
        public ActionResult Add()
        {
            
            return View(new MemberDatabase.Member());
        }

        [HttpGet]
        public ActionResult Edit(int Id, MemberContext mem)
        {

            MemberDatabase.Member contact1 = mem.DbSet<Member>().Find(Id);


            return View(contact1);
        }

        [HttpPost]
        public ActionResult Update(MemberDatabase.Member myMember, MemberContext mem)
        {

           
        //    unitOfWork.Repository<MemberDatabase.Member>().Update(myMember);

            //we dont want to expose this, we should handle this in the update method

            //  mem.Entry(myContact).Property(m => m.Username).IsModified = true;
     //       mem.Entry(myMember).Property(m => m.FirstName).IsModified = true;
  ///          mem.Entry(myMember).Property(m => m.LastName).IsModified = true;
//            mem.Entry(myMember).Property(m => m.Email).IsModified = true;

         //   unitOfWork.Save();

            List<Member> memList = mem.DbSet<Member>().ToList();


            return View("Index", memList);
        }

        [HttpPost]
        public ActionResult Add(MemberDatabase.Member myMember, MemberContext mem)
        {
            mem.Members.Add(myMember);
            mem.SaveChanges();


            List<Member> memList = mem.DbSet<Member>().ToList();
          
            return View("Index", memList);
        }

    }
}