using System;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Parc.IRepository.ServiceRepository
{
	public class ServiceLogPrinter : IServiceRepositoryLogPrinter<LogPrinter>
	{
        private readonly AppDbContxt _db;

        public ServiceLogPrinter(AppDbContxt db)
		{
            _db = db;
        }

        public bool Delete(Guid id, Guid UserId)
        {
            try
            {
                var logPrinter = new LogPrinter
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Action = "Delete",
                    MaterialId = id,
                    UserId = UserId
                };
                _db.LogPrinters.Add(logPrinter);
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
                var logPrinter = FindBy(id);
                _db.LogPrinters.Remove(logPrinter);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public LogPrinter FindBy(Guid id)
        {
            try
            {
                var logPrinter = _db.LogPrinters.Include(x => x.Printer).FirstOrDefault(x => x.MaterialId.Equals(id));
                return logPrinter;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<LogPrinter> GetAll()
        {
            try
            {
                var logPrinters = _db.LogPrinters.Include(x => x.Printer).OrderBy(x => x.Date).ToList();
                return logPrinters;
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
                var logPrinter = new LogPrinter
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Action = "Create",
                    MaterialId = id,
                    UserId = UserId
                };
                _db.LogPrinters.Add(logPrinter);
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
                var logPrinter = new LogPrinter
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Action = "Update",
                    MaterialId = id,
                    UserId = UserId
                };
                _db.LogPrinters.Add(logPrinter);
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

