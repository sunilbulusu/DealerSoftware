		
using System;
using System.Linq;
using System.Collections.Generic;
using DF.Domain.Abstract;
using DF.Domain.Concrete;
	
namespace DF.Repositories
{   
	public interface IAddressRepository : IRepository<Address>
	{
	
	}

	public partial class AddressRepository : EFRepository<Address>, IAddressRepository
	{

	}
}