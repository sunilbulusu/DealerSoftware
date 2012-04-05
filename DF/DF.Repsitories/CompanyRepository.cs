		
using System;
using System.Linq;
using System.Collections.Generic;
using DF.Domain.Abstract;
using DF.Domain.Concrete;
	
namespace DF.Repositories
{   
	public interface ICompanyRepository : IRepository<Company>
	{
	
	}

	public partial class CompanyRepository : EFRepository<Company>, ICompanyRepository
	{

	}
}