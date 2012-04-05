		
using System;
using System.Linq;
using System.Collections.Generic;
using DF.Domain.Abstract;
using DF.Domain.Concrete;
	
namespace DF.Repositories
{   
	public interface IDealershipRepository : IRepository<Dealership>
	{
	
	}

	public partial class DealershipRepository : EFRepository<Dealership>, IDealershipRepository
	{

	}
}