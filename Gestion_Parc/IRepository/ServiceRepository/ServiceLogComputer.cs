using System;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Parc.IRepository.ServiceRepository
{
	public class ServiceLogComputer : IServiceRepositoryLogComputer<LogComputer>
	{
        private readonly AppDbContxt _db;

        public ServiceLogComputer(AppDbContxt db)
		{
            _db = db;
        }

        public bool Delete(Guid id, Guid userId)
        {
            try
            {
                var logComputer = new LogComputer
                {
                    Id = Guid.NewGuid(),
                    Action = "Delete",
                    Date = DateTime.Now,
                    MaterialId = id,
                    UserId = userId
                };
                _db.LogComputers.Add(logComputer);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteLog(Guid id)
        {
            try
            {
                var log = FindBy(id);
                if (log != null)
                {
                    _db.LogComputers.Remove(log);
                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public LogComputer FindBy(Guid id)
        {
            try
            {
                var logComputer = _db.LogComputers.Include(x => x.Computer).FirstOrDefault(x => x.Id.Equals(id));
                return logComputer;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<LogComputer> GetAll()
        {
            try
            {
                var logComputers = _db.LogComputers.Include(x => x.Computer).OrderBy(x => x.Date).ToList();
                return logComputers;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Save(Guid id, Guid userId)
        {
            try
            {
                var logComputer = new LogComputer
                {
                    Id = Guid.NewGuid(),
                    Action = "Create",
                    Date = DateTime.Now,
                    MaterialId = id,
                    UserId = userId
                };
                _db.LogComputers.Add(logComputer);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Guid id, Guid userId)
        {
            try
            {
                var logComputer = new LogComputer
                {
                    Id = Guid.NewGuid(),
                    Action = "Update",
                    Date = DateTime.Now,
                    MaterialId = id,
                    UserId = userId
                };
                _db.LogComputers.Add(logComputer);
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

