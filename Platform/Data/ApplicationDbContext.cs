using System.Data.Entity;
using Platform.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using MemberDatabase;

namespace Platform.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("name=DefaultConnection")
		{
        }

        public DbSet<Issue> Contacts { get; set; }
        public DbSet<LogAction> Logs { get; set; }
    }
}