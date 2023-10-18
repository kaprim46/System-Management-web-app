using System;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Parc.IRepository.ServiceRepository
{
    public class ServiceComputer : IServiceRepositoryComputer<Computer>
    {
        private readonly AppDbContxt _db;

        public ServiceComputer(AppDbContxt db)
        {
            _db = db;
        }
        public bool Delete(Guid id)
        {
            try
            {
                var computer = FindBy(id);
                computer.CurrentState = 0;
                _db.Computers.Update(computer);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(Computer model)
        {
            try
            {
                var computer = FindBy(model.MaterialId);
                computer.MaterialName = model.MaterialName;
                computer.Memory = model.Memory;
                computer.OS = model.OS;
                computer.Description = model.Description;
                computer.GraphicsCard = model.GraphicsCard;
                computer.Processor = model.Processor;
                computer.Quantity = model.Quantity;
                computer.Screen = model.Screen;
                computer.DiskSpace = model.DiskSpace;
                computer.Cpu = model.Cpu;
                computer.Color = model.Color;
                computer.CurrentState = 1;
                computer.Department = model.Department;
                computer.Brand = model.Brand;
                _db.Computers.Update(computer);
                _db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Computer FindBy(Guid id)
        {
            try
            {
                var computer = _db.Computers.FirstOrDefault(x => x.MaterialId.Equals(id) && x.CurrentState > 0);
                return computer;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Computer FindBy(string name)
        {
            try
            {
                var computers = _db.Computers.FirstOrDefault(x => x.MaterialName.Equals(name) && x.CurrentState > 0);
                return computers;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Computer> GetAll()
        {
            try
            {
                var computers = _db.Computers.Where(x => x.CurrentState > 0).ToList();
                return computers;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Computer> GetAllUnUsed()
        {
            try
            {
                var computers = _db.Computers.Where(x => x.CurrentState == 0).ToList();
                return computers;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Save(Computer model)
        {
            try
            {
                model.MaterialId = Guid.NewGuid();
                var computer = new Computer
                {
                    MaterialId = model.MaterialId,
                    MaterialName = model.MaterialName,
                    Memory = model.Memory,
                    Color = model.Color,
                    Cpu = model.Cpu,
                    CurrentState = 1,
                    GraphicsCard = model.GraphicsCard,
                    Description = model.Description,
                    DiskSpace = model.DiskSpace,
                    OS = model.OS,
                    Processor = model.Processor,
                    Quantity = model.Quantity,
                    Screen = model.Screen,
                    Department = model.Department,
                    Brand = model.Brand
                };
                
                _db.Computers.Add(computer);
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

