using System.Data.Objects;
using CWS.Domain.Abstract;
using CWS.Infrastructure.Repositories;
using CWS.Infrastructure.Concrete;

namespace CWS.Infrastructure.Repositories
{
	public class EFUnitOfWork : IUnitOfWork
	{
		public ObjectContext Context { get; set; }

		public EFUnitOfWork(string connectionString)
		{
			Context = new CWSContext(connectionString);
		}

		public void Commit()
		{
			Context.SaveChanges();
		}
		
		public bool LazyLoadingEnabled
		{
			get { return Context.ContextOptions.LazyLoadingEnabled; }
			set { Context.ContextOptions.LazyLoadingEnabled = value;}
		}

		public bool ProxyCreationEnabled
		{
			get { return Context.ContextOptions.ProxyCreationEnabled; }
			set { Context.ContextOptions.ProxyCreationEnabled = value; }
		}
		
		public string ConnectionString
		{
			get { return Context.Connection.ConnectionString; }
			set { Context.Connection.ConnectionString = value; }
		}
	}
}
