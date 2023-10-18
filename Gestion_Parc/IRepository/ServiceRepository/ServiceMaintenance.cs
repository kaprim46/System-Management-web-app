using System;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.Models;
using Gestion_Parc.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Parc.IRepository.ServiceRepository
{
	public class ServiceMaintenance:IServiceRepositoryMaintenance<Maintenance>
	{
        private readonly AppDbContxt _db;

        public ServiceMaintenance(AppDbContxt db)
		{
            _db = db;
        }

        public Maintenance FindBy(Guid id)
        {
            try
            {
                var maintenance = _db.Maintenances.Include(x => x.BreakDown)
                                  .Include(x => x.User).FirstOrDefault(x => x.Id.Equals(id));
                return maintenance;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Maintenance> GetAll()
        {
            try
            {
                var maintenances = _db.Maintenances.Include(x => x.BreakDown)
                                   .Include(x => x.User).OrderBy(x => x.Date)
                                   .ToList();
                return maintenances;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Save(Maintenance model, Guid id)
        {
            try
            {
                var maintenance = new Maintenance
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Notes = model.Notes,
                    Status = Helper.Status.InProgress.ToString(),
                    Type = model.Type,
                    UserId = id.ToString(),
                    BreakDownId = model.BreakDownId,
                    BreakDown = null,
                    User = null
                };
                var breakDown = _db.BreakDowns.Find(model.BreakDownId);
                breakDown.Status = Helper.Status.InProgress.ToString();
                
                _db.BreakDowns.Update(breakDown);
                _db.Maintenances.Add(maintenance);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Confirm(Guid id)
        {
            try
            {
                var maintenance = FindBy(id);
                var breakDown = _db.BreakDowns.Find(maintenance.BreakDownId);
                maintenance.Status = Helper.Status.Completed.ToString();
                breakDown.Status = Helper.Status.Completed.ToString();
                _db.Maintenances.Update(maintenance);
                _db.BreakDowns.Update(breakDown);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

