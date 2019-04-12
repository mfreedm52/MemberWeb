using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MemberDatabase;
using System.Configuration;

namespace Platform.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Payment
        public ActionResult Index(MemberContext mem)
        {
          
            
            string username = User.Identity.Name;


            MemberDatabase.Member myMemberObj = new MemberDatabase.Member(); // unitOfWork.Repository<MemberDatabase.Member>();
            return View(myMemberObj);
        }

        
        [HttpPost]
        public ActionResult Update(MemberDatabase.Member myMember, MemberContext mem)
        {
            //var unitOfWork = new GenericRepository.UnitOfWork(mem);

            mem.DbSet<Member>().Add(myMember);

            //unitOfWork.Repository<MemberDatabase.Member>().Update(myMember);

            //  mem.Entry(myContact).Property(m => m.Username).IsModified = true;
            //            mem.Entry(myMember).Property(m => m.FirstName).IsModified = true;
            //          mem.Entry(myMember).Property(m => m.LastName).IsModified = true;
            //        mem.Entry(myMember).Property(m => m.Email).IsModified = true;

            mem.SaveChanges();


            return RedirectToAction("Index", "Home");
        }


    }
}