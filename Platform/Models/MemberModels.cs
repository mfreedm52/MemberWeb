using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Platform.Models
{
    public class ContactList
    {
        public List<Contact> ContactModel { get; set; }
    }
    public class Contact
    {

        public string Email { get; set; }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


    }
    
}