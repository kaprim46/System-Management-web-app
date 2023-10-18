using System;
namespace Gestion_Parc.IRepository
{
	public interface IServiceRepositoryOther<T> where T:class
	{
		List<T> GetAll();
		T FindBy(Guid id);
		bool Save(T model);
        bool Edit(T id);
        bool Delete(Guid id);
		T Detail(Guid id);
        List<T> GetAllUnUsed();
    }
}

