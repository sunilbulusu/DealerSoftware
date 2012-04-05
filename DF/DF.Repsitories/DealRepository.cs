		
using System;
using System.Linq;
using System.Collections.Generic;
using DF.Domain.Abstract;
using DF.Domain.Concrete;
	
namespace DF.Repositories
{   
	public interface IDealRepository : IRepository<Deal>
	{
	
	}

	public partial class DealRepository : EFRepository<Deal>, IDealRepository
	{

	}
}