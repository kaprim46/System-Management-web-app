using System;
namespace Gestion_Parc.IRepository
{
	public interface IServiceRepositoryServer<T> where T:class
	{
        List<T> GetAll();
        T FindBy(Guid id);
        bool Save(T model);
        bool Edit(T model);
        bool Delete(Guid id);
        List<T> GetAllUnUsed();
    }
}

