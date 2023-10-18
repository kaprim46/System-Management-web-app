using System;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.Models;

namespace Gestion_Parc.IRepository.ServiceRepository
{
	public class ServiceDepartment : IServiceRepositoryDepartment<Department>
	{
        private readonly AppDbContxt _db;

        public ServiceDepartment(AppDbContxt db)
		{
            _db = db;
        }

        public bool Delete(Guid id)
        {
            try
            {
                var department = FindBy(id);
                    _db.Departments.Remove(department);
                    _db.SaveChanges();

                    return true;
                
            }
            catch (Exception)
            {
                return false;
            } 
        }

        public Department FindBy(Guid id)
        {
            try
            {
                var department = _db.Departments.FirstOrDefault(x => x.DepartmentId.Equals(id));
                    return department;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Department FindBy(string name)
        {
            try
            {
                var department = _db.Departments.FirstOrDefault(x => x.DepartmentName.Equals(name));
                return department;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Department> GetAll()
        {
            try
            {
                var departments = _db.Departments.ToList(); ;
                return departments;
            }
            catch (Exception)
            {
                return null;
            } 
        }

       

       
        public bool Save(Department model)
        {
            try
            {
                var result = FindBy(model.DepartmentId);
                if (model.DepartmentId == Guid.Parse(Guid.Empty.ToString()))
                {
                    model.DepartmentId = Guid.NewGuid();
                    _db.Departments.Add(model);
                }
                else
                {
                    result.DepartmentName = model.DepartmentName;
                    _db.Departments.Update(result);
                }
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

