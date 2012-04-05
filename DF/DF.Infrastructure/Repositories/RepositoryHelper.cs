
using CWS.Domain.Abstract;
using CWS.Infrastructure.Repositories;
using CWS.Infrastructure.Concrete;
namespace CWS.Infrastructure.Repositories 
{
	public static class RepositoryHelper
	{
        //public static IUnitOfWork GetUnitOfWork()
        //{
        //    return new EFUnitOfWork();
        //}		
		
        public static IUnitOfWork GetUnitOfWork(string connectionString)
		{
			return new EFUnitOfWork(connectionString);
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

		public static MenuPatternRepository GetMenuPatternRepository()
		{
			return new MenuPatternRepository();
		}

		public static MenuPatternRepository GetMenuPatternRepository(IUnitOfWork unitOfWork)
		{
			var repository = new MenuPatternRepository();
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