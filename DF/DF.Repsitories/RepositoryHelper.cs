using DF.Domain.Abstract;
namespace DF.Repositories
{
	public static class RepositoryHelper
	{
		public static IUnitOfWork GetUnitOfWork(string connectionString)
		{
			return new EFUnitOfWork(connectionString);
		}		
		
		public static AddressRepository GetAddressRepository()
		{
			return new AddressRepository();
		}

		public static AddressRepository GetAddressRepository(IUnitOfWork unitOfWork)
		{
			var repository = new AddressRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static BuyerRepository GetBuyerRepository()
		{
			return new BuyerRepository();
		}

		public static BuyerRepository GetBuyerRepository(IUnitOfWork unitOfWork)
		{
			var repository = new BuyerRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static CompanyRepository GetCompanyRepository()
		{
			return new CompanyRepository();
		}

		public static CompanyRepository GetCompanyRepository(IUnitOfWork unitOfWork)
		{
			var repository = new CompanyRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static ContactRepository GetContactRepository()
		{
			return new ContactRepository();
		}

		public static ContactRepository GetContactRepository(IUnitOfWork unitOfWork)
		{
			var repository = new ContactRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static DealRepository GetDealRepository()
		{
			return new DealRepository();
		}

		public static DealRepository GetDealRepository(IUnitOfWork unitOfWork)
		{
			var repository = new DealRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static DealershipRepository GetDealershipRepository()
		{
			return new DealershipRepository();
		}

		public static DealershipRepository GetDealershipRepository(IUnitOfWork unitOfWork)
		{
			var repository = new DealershipRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static DocumentRepository GetDocumentRepository()
		{
			return new DocumentRepository();
		}

		public static DocumentRepository GetDocumentRepository(IUnitOfWork unitOfWork)
		{
			var repository = new DocumentRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static InstructionsRepository GetInstructionsRepository()
		{
			return new InstructionsRepository();
		}

		public static InstructionsRepository GetInstructionsRepository(IUnitOfWork unitOfWork)
		{
			var repository = new InstructionsRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static LenderRepository GetLenderRepository()
		{
			return new LenderRepository();
		}

		public static LenderRepository GetLenderRepository(IUnitOfWork unitOfWork)
		{
			var repository = new LenderRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static MenuRepository GetMenuRepository()
		{
			return new MenuRepository();
		}

		public static MenuRepository GetMenuRepository(IUnitOfWork unitOfWork)
		{
			var repository = new MenuRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static MenuTypeRepository GetMenuTypeRepository()
		{
			return new MenuTypeRepository();
		}

		public static MenuTypeRepository GetMenuTypeRepository(IUnitOfWork unitOfWork)
		{
			var repository = new MenuTypeRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static UserRepository GetUserRepository()
		{
			return new UserRepository();
		}

		public static UserRepository GetUserRepository(IUnitOfWork unitOfWork)
		{
			var repository = new UserRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		
	}
}