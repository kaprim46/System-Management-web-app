using System;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.Models;

namespace Gestion_Parc.IRepository.ServiceRepository
{
	public class ServiceOther : IServiceRepositoryOther<Other>
	{
        private readonly AppDbContxt _db;

        public ServiceOther(AppDbContxt db)
		{
            _db = db;
        }

        public bool Delete(Guid id)
        {
            try
            {
                var other = FindBy(id);
                other.CurrentState = 0;
                _db.Materials.Update(other);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Other Detail(Guid id)
        {
            try
            {
                var other = FindBy(id);
                return other;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Edit(Other model)
        {
            try
            {
                var other = FindBy(model.MaterialId);
                other.MaterialName = model.MaterialName;
                other.Quantity = model.Quantity;
                other.Description = model.Description;
                other.Brand = model.Brand;
                other.Color = model.Color;
                other.Department = model.Department;
                other.CurrentState = 1;

                _db.Materials.Update(other);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Other FindBy(Guid id)
        {
            try
            {
                var other = _db.Materials.FirstOrDefault(x => x.MaterialId.Equals(id) && x.CurrentState > 0);
                return other;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Other> GetAll()
        {
            try
            {
                var others = _db.Materials.Where(x => x.CurrentState.Equals(1)).ToList();
                return others;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Other> GetAllUnUsed()
        {
            try
            {
                var others = _db.Materials.Where(x => x.CurrentState == 0).ToList();
                return others;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Save(Other model)
        {
            try
            {
                model.MaterialId = Guid.NewGuid();
                var other = new Other
                {
                    MaterialId = model.MaterialId,
                    MaterialName = model.MaterialName,
                    Brand = model.Brand,
                    Department = model.Department,
                    Color = model.Color,
                    CurrentState = 1,
                    Description = model.Description,
                    Quantity = model.Quantity
                };
                _db.Materials.Add(other);
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

