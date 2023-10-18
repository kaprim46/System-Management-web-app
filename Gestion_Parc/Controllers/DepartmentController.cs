using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.IRepository;
using Gestion_Parc.Models;
using Gestion_Parc.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Parc.Controllers
{
   [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IServiceRepositoryDepartment<Department> _serviceDepartment;
        private readonly UserManager<ApplicationUser> _userManager;

        public DepartmentController(IServiceRepositoryDepartment<Department> ServiceDepartment,
                                    UserManager<ApplicationUser> userManager)
        {
            _serviceDepartment = ServiceDepartment;
            _userManager = userManager;
        }

        public IActionResult Departments()
        {
            var department = new DepartmentViewModel
            {
                Departments = _serviceDepartment.GetAll(),
                NNewDepartment = new NewDepartment()
            };
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Departments(DepartmentViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (model.NNewDepartment.DepartmentId == null)
                    model.NNewDepartment.DepartmentId = Guid.Parse(Guid.Empty.ToString());


                var department = new DepartmentViewModel
                {
                    NewDepartment = new Department()
                };

                department.NewDepartment.DepartmentId = (Guid)model.NNewDepartment.DepartmentId;
                department.NewDepartment.DepartmentName = model.NNewDepartment.DepartmentName;

                var userId = _userManager.GetUserId(User);
                if (department.NewDepartment.DepartmentId == Guid.Parse(Guid.Empty.ToString()))
                {
                        var exist = _serviceDepartment.FindBy(department.NewDepartment.DepartmentName);
                    if (exist != null)
                    {
                        if (exist.DepartmentName.ToUpper().Equals(department.NewDepartment.DepartmentName.ToUpper()))
                            SessionMsg(Helper.Error, "Duplicated", "Department existe deja!");
                    }
                    
                    else
                    {
                        if(_serviceDepartment.Save(department.NewDepartment))
                            SessionMsg(Helper.Success, "Ajouter", "Department ajouter avec succes");
                        else
                            SessionMsg(Helper.Error, "Ne pas ajouter", "Department ne pas ajouter!");
                    }
                }
                else
                {
                    if (_serviceDepartment.FindBy((Guid)department.NewDepartment.DepartmentId) != null)
                    {
                        if (_serviceDepartment.Save(department.NewDepartment))
                            SessionMsg(Helper.Success, "Modifier", "Department modifier avec succes");
                        else
                            SessionMsg(Helper.Error, "Ne pas modifier", "Department ne pas modifier!");
                    }
                }

                return RedirectToAction("Departments", "Department");
            }
            return View(model);
        }

       
        public IActionResult Delete(Guid id)
        {
            
                var userId = _userManager.GetUserId(User);
                  if (_serviceDepartment.Delete(id))
                return RedirectToAction(nameof(Departments));

            return RedirectToAction(nameof(Departments));
        }


        public void SessionMsg(string msgType, string title, string msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, msgType);
            HttpContext.Session.SetString(Helper.Title, title);
            HttpContext.Session.SetString(Helper.Msg, msg);
        }

    }
}