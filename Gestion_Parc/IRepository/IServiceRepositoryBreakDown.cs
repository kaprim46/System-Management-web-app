using System;
namespace Gestion_Parc.IRepository
{
	public interface IServiceRepositoryBreakDown<T> where T:class
	{
        List<T> GetAll();
        List<T> GetAllForEachUser();
        List<T> GetAllReported();
        List<T> GetAllInProgress();
        List<T> GetAllCompleted();
        T FindBy(Guid id);
        bool Save(T model, string userId);
        List<T> GetAllComputers();
        List<T> GetAllPrinters();
        List<T> GetAllServers();
        List<T> GetAllOthers();
    }
}

