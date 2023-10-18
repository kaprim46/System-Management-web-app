using System;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Parc.IRepository.ServiceRepository
{
	public class ServiceLogBrand : IServiceRepositoryLogBrand<LogBrand>
	{
        private readonly AppDbContxt _db;

        public ServiceLogBrand(AppDbContxt db)
		{
            _db = db;
        }

        public bool Delete(Guid id, Guid userId)
        {
            try
            {
                var logBrand = new LogBrand
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Action = "Delete",
                    BrandId = id,
                    UserId = userId
                };
                _db.LogBrands.Add(logBrand);
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
                var logBrand = FindBy(id);
                if (logBrand != null)
                {
                    _db.LogBrands.Remove(logBrand);
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

        public LogBrand FindBy(Guid id)
        {
            try
            {
                var logBrand = _db.LogBrands.Include(x => x.Brand).FirstOrDefault(x => x.Id.Equals(id));
                return logBrand;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<LogBrand> GetAllLog()
        {
            try
            {
                var logBrand = _db.LogBrands.Include(x => x.Brand).OrderBy(x => x.Date).ToList();
                return logBrand;
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
                var logBrand = new LogBrand
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Action = "Create",
                    BrandId = id,
                    UserId = userId
                };

                _db.LogBrands.Add(logBrand);
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
              var logBrand = new LogBrand
              { 
                    Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Action = "Update",
                BrandId = id,
                UserId = userId
              };
                _db.LogBrands.Add(logBrand);
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

