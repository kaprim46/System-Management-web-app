using System;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.Models;

namespace Gestion_Parc.IRepository.ServiceRepository
{
	public class ServiceServer : IServiceRepositoryServer<Server>
	{
        private readonly AppDbContxt _db;

        public ServiceServer(AppDbContxt db)
        {
            _db = db;
        }

        public bool Delete(Guid id)
        {
            try
            {
                var server = FindBy(id);
                server.CurrentState = 0;
                _db.Servers.Update(server);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(Server model)
        {
            try
            {
                var server = FindBy(model.MaterialId);
                server.MaterialId = model.MaterialId;
                server.MaterialName = model.MaterialName;
                server.Quantity = model.Quantity;
                server.Description = model.Description;
                server.Brand = model.Brand;
                server.Department = model.Department;
                server.Color = model.Color;
                server.CurrentState = 1;
                server.Memory = model.Memory;
                server.Processor = model.Processor;
                server.Storage = model.Storage;
                server.Cores = model.Cores;

                _db.Servers.Update(server);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Server FindBy(Guid id)
        {
            try
            {
                var server = _db.Servers.FirstOrDefault(x => x.MaterialId.Equals(id) && x.CurrentState > 0);
                return server;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Server> GetAll()
        {
            try
            {
                var servers = _db.Servers.Where(x => x.CurrentState > 0).ToList();
                return servers;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Server> GetAllUnUsed()
        {
            try
            {
                var servers = _db.Servers.Where(x => x.CurrentState > 0).ToList();
                return servers;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Save(Server model)
        {
            try
            {
                model.MaterialId = Guid.NewGuid();
                var server = new Server
                {
                    MaterialId = model.MaterialId,
                    MaterialName = model.MaterialName,
                    Brand = model.Brand,
                    Department = model.Department,
                    Description = model.Description,
                    Color = model.Color,
                    CurrentState = 1,
                    Quantity = model.Quantity,
                    Processor = model.Processor,
                    Memory = model.Memory,
                    Storage = model.Storage,
                    Cores = model.Cores
                };
                _db.Servers.Add(server);
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

