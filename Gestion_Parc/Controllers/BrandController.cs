using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.IRepository;
using Gestion_Parc.IRepository.ServiceRepository;
using Gestion_Parc.Models;
using Gestion_Parc.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Parc.Controllers
{
    public class BrandController : Controller
    {
        private readonly IServiceRepositoryBrand<Brand> _serviceBrand;
        private readonly IServiceRepositoryLogBrand<LogBrand> _serviceLogBrand;
        private readonly AppDbContxt _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public BrandController(IServiceRepositoryBrand<Brand> serviceBrand,
                               IServiceRepositoryLogBrand<LogBrand> serviceLogBrand,
                               AppDbContxt db,
                               UserManager<ApplicationUser> userManager)
        {
            _serviceBrand = serviceBrand;
            _serviceLogBrand = serviceLogBrand;
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Brands()
        {
            var brands = new BrandViewModel
            {
                NNewBrand = new NewBrand(),
                Brands = _serviceBrand.GetAll(),
                LogBrands = _serviceLogBrand.GetAllLog()
            };
            return View(brands);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Brands(BrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.NNewBrand.BrandId == null)
                    model.NNewBrand.BrandId = Guid.Parse(Guid.Empty.ToString());


                var brand = new BrandViewModel
                {
                    NewBrand = new Brand()
                };

                brand.NewBrand.BrandId = (Guid)model.NNewBrand.BrandId;
                brand.NewBrand.BrandName = model.NNewBrand.BrandName;
                var userId = _userManager.GetUserId(User);
                if(brand.NewBrand.BrandId == Guid.Parse(Guid.Empty.ToString()))
                {
                    var exist = _serviceBrand.FindBy(brand.NewBrand.BrandName);
                    if (exist != null)
                    {
                        if (exist.BrandName.ToUpper().Equals(brand.NewBrand.BrandName.ToUpper()))
                            SessionMsg(Helper.Error, "Duplicated", "Brand is already exist!");
                    }
                    else
                    {
                        if (_serviceBrand.Save(brand.NewBrand) && _serviceLogBrand.Save(brand.NewBrand.BrandId, Guid.Parse(userId)))

                            SessionMsg(Helper.Success, "Enregistrer", "Marque Enregistrer avec succes");
                        else
                            SessionMsg(Helper.Error, "Ne pas enregistrer", "Marque ne pas Enregistrer!");
                    }
                         
                }
                else
                {
                    if (_serviceBrand.FindBy(brand.NewBrand.BrandId) != null)
                        if (_serviceBrand.Save(brand.NewBrand) && _serviceLogBrand.Update(brand.NewBrand.BrandId, Guid.Parse(userId)))

                        SessionMsg(Helper.Success, "Modifier", "Marque Modifier avec succes");
                    else
                        SessionMsg(Helper.Error, "Ne pas modifier", "BMarque ne pas Modifier!");
                }

                return RedirectToAction("Brands", "Brand");
            }
            return View(model);
        }

        public IActionResult Delete(Guid id)
        {
            var userId = _userManager.GetUserId(User);
            if (_serviceBrand.Delete(id) && _serviceLogBrand.Delete(id, Guid.Parse(userId)))
                return RedirectToAction(nameof(Brands));

            return RedirectToAction(nameof(Brands));
        }

        public IActionResult DeleteLog(Guid id)
        {
            if (_serviceLogBrand.DeleteLog(id))
                return RedirectToAction(nameof(Brands));
            return RedirectToAction(nameof(Brands));
        }

        public void SessionMsg(string msgType, string title, string msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, msgType);
            HttpContext.Session.SetString(Helper.Title, title);
            HttpContext.Session.SetString(Helper.Msg, msg);
        }
    }
}