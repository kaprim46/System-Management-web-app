using System;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.Models;

namespace Gestion_Parc.IRepository.ServiceRepository
{
	public class ServiceBrand : IServiceRepositoryBrand<Brand>
	{
        private readonly AppDbContxt _db;

        public ServiceBrand(AppDbContxt db)
		{
            _db = db;
        }

        public bool Delete(Guid id)
        {
            try
            {
                var brand = FindBy(id);
                brand.CurrentState = 0;
                _db.Brands.Update(brand);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Brand FindBy(Guid id)
        {
            try
            {
                var brand = _db.Brands.FirstOrDefault(x => x.BrandId.Equals(id) && x.CurrentState > 0);
                return brand;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Brand FindBy(string name)
        {
            try
            {
                var brand = _db.Brands.FirstOrDefault(x => x.BrandName.Equals(name) && x.CurrentState > 0);
                return brand;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Brand> GetAll()
        {
            try
            {
                var brands = _db.Brands.Where(x => x.CurrentState > 0).ToList();
                if(brands != null)
                return brands;
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Save(Brand model)
        {
            try
            {
                var brand = FindBy(model.BrandId);
                if (model.BrandId == Guid.Parse(Guid.Empty.ToString()))
                {
                    model.BrandId = Guid.NewGuid();
                    model.CurrentState = 1;
                    _db.Brands.Add(model);
                }
                else
                {
                    brand.BrandName = model.BrandName;
                    brand.CurrentState = 1;
                    _db.Brands.Update(brand);
                }
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

