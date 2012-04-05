		
using System;
using System.Linq;
using System.Collections.Generic;
using CWS.Domain.Abstract;
using CWS.Infrastructure.Concrete;
	
namespace CWS.Infrastructure.Repositories
{   
    public interface IMenuRepository : IRepository<Menu>
    {}

	public partial class MenuRepository : EFRepository<Menu> , IMenuRepository
	{

	}
}