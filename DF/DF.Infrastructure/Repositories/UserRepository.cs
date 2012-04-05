		
using System;
using System.Linq;
using System.Collections.Generic;
using CWS.Domain.Abstract;
using CWS.Infrastructure.Concrete;
	
namespace CWS.Infrastructure.Repositories
{   
    public interface IUserRepository : IRepository<User>
    {}
	public partial class UserRepository : EFRepository<User>, IUserRepository
	{

	}
}