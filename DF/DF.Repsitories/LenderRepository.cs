		
using System;
using System.Linq;
using System.Collections.Generic;
using DF.Domain.Abstract;
using DF.Domain.Concrete;
	
namespace DF.Repositories
{   
	public interface ILenderRepository : IRepository<Lender>
	{
	
	}

	public partial class LenderRepository : EFRepository<Lender>, ILenderRepository
	{

	}
}