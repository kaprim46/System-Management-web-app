using System;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Parc.IRepository.ServiceRepository
{
	public class ServiceLogOther : IServiceRepositoryLogOther<LogOther>
	{
        private readonly AppDbContxt _db;

        public ServiceLogOther(AppDbContxt db)
        {
            _db = db;
        }

        public bool Delete(Guid id, Guid UserId)
        {
            try
            {
                var logOther = new LogOther
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Action = "Delete",
                    MaterialId = id,
                    UserId = UserId
                };
                _db.LogMaterials.Add(logOther);
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
                var logOther = FindBy(id);
                _db.LogMaterials.Remove(logOther);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public LogOther FindBy(Guid id)
        {
            try
            {
                var logOther = _db.LogMaterials.Include(x => x.Other).FirstOrDefault(x => x.MaterialId.Equals(id));
                return logOther;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<LogOther> GetAll()
        {
            try
            {
                var logOthers = _db.LogMaterials.Include(x => x.Other).OrderBy(x => x.Date).ToList();
                return logOthers;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Save(Guid id, Guid UserId)
        {
            try
            {
                var logOther = new LogOther
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Action = "Create",
                    MaterialId = id,
                    UserId = UserId
                };
                _db.LogMaterials.Add(logOther);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Guid id, Guid UserId)
        {
            try
            {
                var logOther = new LogOther
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Action = "Update",
                    MaterialId = id,
                    UserId = UserId
                };
                _db.LogMaterials.Add(logOther);
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

