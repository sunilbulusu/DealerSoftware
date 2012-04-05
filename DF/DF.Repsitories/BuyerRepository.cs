		
using System;
using System.Linq;
using System.Collections.Generic;
using DF.Domain.Abstract;
using DF.Domain.Concrete;
	
namespace DF.Repositories
{   
	public interface IBuyerRepository : IRepository<Buyer>
	{
	
	}

	public partial class BuyerRepository : EFRepository<Buyer>, IBuyerRepository
	{

	}
}