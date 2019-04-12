using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MemberDatabase;
using Platform.Filters;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Mvc;

namespace Platform.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {

        // GET: Contact
        public ActionResult Index(string SearchString, MemberContext mem)
        {
            //ContactList objContacts = new ContactList();

            ////something feels redundant here
            //List<Models.Contact> _objContact = new List<Models.Contact>();
            //_objContact = null; //GetContactList();
            //objContacts.ContactModel = _objContact;
            //string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //MemberContext mem = new MemberContext(connection);
            var unitOfWork = new GenericRepository.UnitOfWork(mem);

            List<Contact> contactList = unitOfWork.Repository<Contact>().Query().Get().ToList();


            if (!String.IsNullOrEmpty(SearchString))
            {
                //TODO fix
                contactList = unitOfWork.Repository<Contact>().Query().Get().ToList();

            }
            return View(contactList);
        }

        // GET: Contact
        public ActionResult Member(MemberContext mem)
        {
            //ContactList objContacts = new ContactList();

            ////something feels redundant here
            //List<Models.Contact> _objContact = new List<Models.Contact>();
            //_objContact = null; //GetContactList();
            ////objContacts.ContactModel = _objContact;
            //string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            //MemberContext mem = new MemberContext(connection);
            var unitOfWork = new GenericRepository.UnitOfWork(mem);

            List<Contact> contactList = unitOfWork.Repository<Contact>().Query().Get().ToList();
            return View(contactList);
        }

        [HttpGet]
        public ActionResult Add()
        {
            
            return View(new MemberDatabase.Contact());
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            MemberDatabase.Repo.ContactRepository repo = new MemberDatabase.Repo.ContactRepository();

            MemberDatabase.Contact contact1 = repo.FindById(Id);



            return View(contact1);
        }

        [HttpGet]
        public ActionResult View(int Id, MemberContext mem)
        {
            var unitOfWork = new GenericRepository.UnitOfWork(mem);


            MemberDatabase.Contact contact1 = unitOfWork.Repository<Contact>().FindById(Id);
            


            return View(contact1);
        }

        [HttpPost]
        public ActionResult Update(MemberDatabase.Contact myContact, MemberContext mem)
        {

            //string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //MemberContext mem = new MemberContext(connection);
            var unitOfWork = new GenericRepository.UnitOfWork(mem);

            unitOfWork.Repository<Contact>().Update(myContact);

            //  mem.Entry(myContact).Property(m => m.Username).IsModified = true;
            //mem.Entry(myContact).Property(m => m.FirstName).IsModified = true;
            //mem.Entry(myContact).Property(m => m.LastName).IsModified = true;
            //mem.Entry(myContact).Property(m => m.Email).IsModified = true;

            unitOfWork.Save();
             List<Contact> memList = unitOfWork.Repository<Contact>().Query().Get().ToList();


            return View("Index", memList);
        }

        [HttpPost]
        public ActionResult Comment(MemberDatabase.Contact myContact, MemberContext mem)
        {
            MemberDatabase.Comment com = new Comment();
            com.comment = "new comment";
            com.ContactId = myContact.Id;
            com.postDate = DateTime.Now;
            com.postedBy = "mfreedm";

            mem.DbSet<Comment>().Add(com);
            mem.SaveChanges();

            MemberDatabase.Repo.ContactRepository repo = new MemberDatabase.Repo.ContactRepository();

            MemberDatabase.Contact contact1 = repo.FindById(myContact.Id);



            return View("Edit", contact1);
        }

        [HttpPost]
        [Log("Add Contact {id}")]
        public ActionResult Add(MemberDatabase.Contact myContact, MemberContext mem)
        {
        //    string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        //    MemberContext mem = new MemberContext(connection);
            var unitOfWork = new GenericRepository.UnitOfWork(mem);

            unitOfWork.Repository<Contact>().InsertGraph(myContact);

            unitOfWork.Save();
            List<Contact> memList = unitOfWork.Repository<Contact>().Query().Get().ToList();


            return View("Index", memList);
        }

        //public List<Models.Contact> GetContactList()
        //{

        //List<Models.Contact> objContact = new List<Models.Contact>();
        ///*Create instance of entity model*/
        //MemberContext objentity = new MemberDatabase.MemberContext();
        ///*Getting data from database for user validation*/
        //var contactDetail = (from data in objentity.myContacts
        //                     select data);
        //    foreach (var item in contactDetail)
        //    {
        //    objContact.
        //    }
        
//            return objContact;
     
        //}


    }
}