using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberDatabase
{
    public class Contact : Person
    {
        public string ContactType { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public string Description { get; set; }
        public ICollection<Comment> comments { get; set; }


    }
}
