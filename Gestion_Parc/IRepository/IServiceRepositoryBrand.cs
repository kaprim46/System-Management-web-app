using System;
namespace Gestion_Parc.IRepository
{
	public interface IServiceRepositoryBrand<T> where T : class
	{
		List<T> GetAll();
		bool Delete(Guid id);
		T FindBy(Guid id);
		T FindBy(string name);
		bool Save(T model);
	}
}

