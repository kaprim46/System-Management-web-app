using System;
namespace Gestion_Parc.IRepository
{
	public interface IServiceRepositoryMaintenance<T> where T:class
	{
		List<T> GetAll();
		T FindBy(Guid id);
		bool Save(T model, Guid id);
		bool Confirm(Guid id);
	}
}

