using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Base2.Domain;

namespace Base2.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("DefaultConnection")
		{
		}

		public DbSet<LogAction> Logs { get; set; }
	}
}