using StructureMap;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using System.Data.Entity;
using Base2.Data;
using Base2.Domain;

namespace Base2.Infrastructure
{
	public class ControllerRegistry : Registry
	{
		public ControllerRegistry()
		{
            For<DbContext>().Use(() => new ApplicationDbContext());
            For<IUserStore<ApplicationUser>>().Use<UserStore<ApplicationUser>>();
            For<IAuthenticationManager>().Use(() => HttpContext.Current.GetOwinContext().Authentication);
        }
	}
}