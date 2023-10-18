using System;
namespace Gestion_Parc.IRepository
{
	public interface IServiceRepositoryLogBrand<T> where T : class
	{
		List<T> GetAllLog();
		bool Delete(Guid id, Guid userId);
		bool Save(Guid id, Guid userId);
		bool Update(Guid id, Guid userId);
		bool DeleteLog(Guid id);
		T FindBy(Guid id);
	}
}

