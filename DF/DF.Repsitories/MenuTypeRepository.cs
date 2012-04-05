		
using System;
using System.Linq;
using System.Collections.Generic;
using DF.Domain.Abstract;
using DF.Domain.Concrete;
	
namespace DF.Repositories
{   
	public interface IMenuTypeRepository : IRepository<MenuType>
	{
	
	}

	public partial class MenuTypeRepository : EFRepository<MenuType>, IMenuTypeRepository
	{

	}
}