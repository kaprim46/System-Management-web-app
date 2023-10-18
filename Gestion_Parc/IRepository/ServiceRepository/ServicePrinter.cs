using System;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.Models;

namespace Gestion_Parc.IRepository.ServiceRepository
{
	public class ServicePrinter : IServiceRepositoryPrinter<Printer>
	{
        private readonly AppDbContxt _db;

        public ServicePrinter(AppDbContxt db)
		{
            _db = db;
        }

        public bool Delete(Guid id)
        {
            try
            {
                var printer = FindBy(id);
                printer.CurrentState = 0;
                _db.Printers.Update(printer);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(Printer model)
        {
            try
            {
                var printer = FindBy(model.MaterialId);
                printer.MaterialId = model.MaterialId;
                printer.MaterialName = model.MaterialName;
                printer.MaximumPrintSpeed = model.MaximumPrintSpeed;
                printer.MaxPrintspeed = model.MaxPrintspeed;
                printer.PrinterOutput = model.PrinterOutput;
                printer.PrintingTechnology = model.PrintingTechnology;
                printer.Quantity = model.Quantity;
                printer.Description = model.Description;
                printer.Brand = model.Brand;
                printer.Department = model.Department;
                printer.Color = model.Color;
                printer.CurrentState = 1;

                _db.Printers.Update(printer);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Printer FindBy(Guid id)
        {
            try
            {
                var printer = _db.Printers.FirstOrDefault(x => x.MaterialId.Equals(id) && x.CurrentState > 0);
                return printer;
            }
            catch (Exception ex)
            {
                return null;
            }  
        }

        public List<Printer> GetAll()
        {
            try
            {
                var printers = _db.Printers.Where(x => x.CurrentState > 0).ToList();
                return printers;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Printer> GetAllUnUsed()
        {
            try
            {
                var printers = _db.Printers.Where(x => x.CurrentState == 0).ToList();
                return printers;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Save(Printer model)
        {
            try
            {
                model.MaterialId = Guid.NewGuid();
                var printer = new Printer
                {
                    MaterialId = model.MaterialId,
                    MaterialName = model.MaterialName,
                    MaximumPrintSpeed = model.MaximumPrintSpeed,
                    MaxPrintspeed = model.MaxPrintspeed,
                    Brand = model.Brand,
                    Department = model.Department,
                    Description = model.Description,
                    Color = model.Color,
                    CurrentState = 1,
                    PrinterOutput = model.PrinterOutput,
                    PrintingTechnology = model.PrintingTechnology,
                    Quantity = model.Quantity
                };
                _db.Printers.Add(printer);
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

