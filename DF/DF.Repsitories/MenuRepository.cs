		
using System;
using System.Linq;
using System.Collections.Generic;
using DF.Domain.Abstract;
using DF.Domain.Concrete;
	
namespace DF.Repositories
{   
	public interface IMenuRepository : IRepository<Menu>
	{
	
	}

	public partial class MenuRepository : EFRepository<Menu>, IMenuRepository
	{

	}
}