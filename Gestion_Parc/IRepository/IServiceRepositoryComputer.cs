using System;
namespace Gestion_Parc.IRepository
{
	public interface IServiceRepositoryComputer<T> where T:class
	{
		List<T> GetAll();
		T FindBy(Guid id);
		T FindBy(string name);
		bool Save(T model);
		bool Edit(T model);
		bool Delete(Guid id);
        List<T> GetAllUnUsed();
    }
}

