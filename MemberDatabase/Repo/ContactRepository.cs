using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberDatabase.Repo
{
    public class ContactRepository //: GenericRepository.Repository<Contact>
    {
        internal MemberContext Context;

        public ContactRepository() //: base(Context)
        {
            Context = new MemberContext();
        }

        public virtual Contact FindById(int id)
        {
            return Context.DbSet<Contact>().Include("comments").Single(x => x.Id == id);
        }


    }
}
