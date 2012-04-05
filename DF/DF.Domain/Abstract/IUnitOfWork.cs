using System.Data.Objects;
namespace DF.Domain.Abstract
{
	public interface IUnitOfWork
	{
		ObjectContext Context { get; set; }
		void Commit();
		bool LazyLoadingEnabled { get; set; }
		bool ProxyCreationEnabled { get; set; }
		string ConnectionString { get; set; }
	}
}		
