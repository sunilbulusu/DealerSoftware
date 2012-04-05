		
using System;
using System.Linq;
using System.Collections.Generic;
using DF.Domain.Abstract;
using DF.Domain.Concrete;
	
namespace DF.Repositories
{   
	public interface IUserRepository : IRepository<User>
	{
	
	}

	public partial class UserRepository : EFRepository<User>, IUserRepository
	{

	}
}