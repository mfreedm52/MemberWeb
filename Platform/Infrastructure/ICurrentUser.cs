using Platform.Domain;

namespace Platform.Infrastructure
{
	public interface ICurrentUser
	{
		ApplicationUser User { get; } 
	}
}