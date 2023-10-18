using System;
namespace Gestion_Parc.IRepository
{
	public interface IServiceRepositoryLogComputer<T> where T:class
	{
		List<T> GetAll();
		T FindBy(Guid id);
		bool Save(Guid id, Guid UserId);
        bool Delete(Guid id, Guid UserId);
        bool Update(Guid id, Guid UserId);
		bool DeleteLog(Guid id);
    }
}

