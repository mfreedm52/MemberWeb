using Base2.Models;

namespace Base2.Infrastructure
{
	public interface ICurrentUser
	{
		ApplicationUser User { get; } 
	}
}