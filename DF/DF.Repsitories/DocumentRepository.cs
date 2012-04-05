		
using System;
using System.Linq;
using System.Collections.Generic;
using DF.Domain.Abstract;
using DF.Domain.Concrete;
	
namespace DF.Repositories
{   
	public interface IDocumentRepository : IRepository<Document>
	{
	
	}

	public partial class DocumentRepository : EFRepository<Document>, IDocumentRepository
	{

	}
}