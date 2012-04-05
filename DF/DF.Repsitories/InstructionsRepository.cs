		
using System;
using System.Linq;
using System.Collections.Generic;
using DF.Domain.Abstract;
using DF.Domain.Concrete;
	
namespace DF.Repositories
{   
	public interface IInstructionsRepository : IRepository<Instructions>
	{
	
	}

	public partial class InstructionsRepository : EFRepository<Instructions>, IInstructionsRepository
	{

	}
}