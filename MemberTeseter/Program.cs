using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MemberTeseter
{
    class Program
    {
        private static MemberDatabase.MemberContext db = new MemberDatabase.MemberContext();

        static void Main(string[] args)
        {
         //   Database.SetInitializer(new MigrateDatabaseToLatestVersion<MemberDatabase.MemberContext, MemberDatabase.Migrations.Configuration>());

            MemberDatabase.Group groupA = createGroup("Test 3");

            AddMemberToGroup(groupA, "Joe cool New Guy", "Last Name", "mfreedm@integrationpoint.net");


        }

        private static MemberDatabase.Group createGroup(string groupName)
        {
            var group = new MemberDatabase.Group();
            group.GroupName = groupName;
            db.myGroups.Add(group);
            db.SaveChanges();

            return group;
        }

        private static void AddMemberToGroup(MemberDatabase.Group groupA, string FirstName, 
            string LastName, string email)
        {
            var Member = new MemberDatabase.Member();
            Member.JoinDate = DateTime.Now;
            Member.FirstName = FirstName;
            Member.LastName = LastName;
            Member.Email = email;
            db.myContacts.Add(Member);

            groupA.Members.Add(Member);


          //  db.MemberGroups.Add(new MemberDatabase.MemberGroup(Member.Id, GroupId));

            db.SaveChanges();

        }

    }
}
