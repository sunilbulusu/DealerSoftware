		
using System;
using System.Linq;
using System.Collections.Generic;
using DF.Domain.Abstract;
using DF.Domain.Concrete;
	
namespace DF.Repositories
{   
	public interface IContactRepository : IRepository<Contact>
	{
	
	}

	public partial class ContactRepository : EFRepository<Contact>, IContactRepository
	{

	}
}