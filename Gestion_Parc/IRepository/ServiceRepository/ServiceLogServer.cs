using System;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Parc.IRepository.ServiceRepository
{
	public class ServiceLogServer : IServiceRepositoryLogServer<LogServer>
	{
        private readonly AppDbContxt _db;

        public ServiceLogServer(AppDbContxt db)
		{
            _db = db;
        }

        public bool Delete(Guid id, Guid UserId)
        {
            try
            {
                var logServer = new LogServer
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Action = "Delete",
                    MaterialId = id,
                    UserId = UserId
                };
                _db.LogServers.Add(logServer);
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
                var logServer = FindBy(id);
                _db.LogServers.Remove(logServer);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public LogServer FindBy(Guid id)
        {
            try
            {
                var logServer = _db.LogServers.Include(x => x.Server).FirstOrDefault(x => x.MaterialId.Equals(id));
                return logServer;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<LogServer> GetAll()
        {
            try
            {
                var logServers = _db.LogServers.Include(x => x.Server).OrderBy(x => x.Date).ToList();
                return logServers;
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
                var logServer = new LogServer
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Action = "Create",
                    MaterialId = id,
                    UserId = UserId
                };
                _db.LogServers.Add(logServer);
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
                var logServer = new LogServer
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Action = "Update",
                    MaterialId = id,
                    UserId = UserId
                };
                _db.LogServers.Add(logServer);
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

