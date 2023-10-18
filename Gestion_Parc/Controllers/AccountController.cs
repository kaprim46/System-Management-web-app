using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.Models;
using Gestion_Parc.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Parc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContxt _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, AppDbContxt db,
                                 RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _db = db;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        //[Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var users = new RegisterViewModel
            {
                NewRegister = new NewRegisterViewModel(),
                Roles = _roleManager.Roles.ToList(),
                Users = _db.ViewUsers.ToList(),
                Departments = _db.Departments.ToList()
            };
            return View(users);
        }

        //[Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            ViewBag.Department = _db.Departments.ToList();
            var users = new RegisterViewModel
            {
                Roles = _roleManager.Roles.ToList()
            };
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var file = HttpContext.Request.Form.Files;
                if (file.Count > 0)
                {
                    var ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                    var filestream = new FileStream(Path.Combine(@"wwwroot/", "Images/Users", ImageName), FileMode.Create);
                    file[0].CopyTo(filestream);
                    model.NewRegister.ImageUser = ImageName;
                }
                else
                    model.NewRegister.ImageUser = model.NewRegister.ImageUser;

                var user = new ApplicationUser
                {
                    Email = model.NewRegister.Email,
                    UserName = model.NewRegister.Email,
                    FullName = model.NewRegister.FullName,
                    ActiveUser = model.NewRegister.ActiveUser,
                    DepartmentName = model.NewRegister.DepartmentName,
                    ImageUser = model.NewRegister.ImageUser,
       
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, model.NewRegister.Password);
                model.NewRegister.Code = user.Id;
                if (result.Succeeded)
                {
                    var role = await _userManager.AddToRoleAsync(user, model.NewRegister.RoleName);
                    if (role.Succeeded)
                        return RedirectToAction(nameof(Index));
                }
                    
                    

                return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            ViewBag.RolesList = _roleManager.Roles.ToList();
            ViewBag.DepartmentList = _db.Departments.ToList();
            var result = await _userManager.FindByIdAsync(id);
            var role = await _userManager.GetRolesAsync(result);
            if (result != null)
            {
                var user = new EditViewModel();
                user.Code = result.Id;
                user.FullName = result.FullName;
                user.Email = result.Email;
                user.ActiveUser = result.ActiveUser;
                user.ImageUser = result.ImageUser;
                user.RoleName = role.ToString();
                return View(user);
            }
            
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Code);
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.ActiveUser = model.ActiveUser;
                if(model.ImageUser != null)
                {
                    var file = HttpContext.Request.Form.Files;
                    if (file.Count > 0)
                    {
                        var ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                        var filestream = new FileStream(Path.Combine(@"wwwroot/", "Images/Users", ImageName), FileMode.Create);
                        file[0].CopyTo(filestream);
                        model.ImageUser = ImageName;
                    }
                    else
                        model.ImageUser = model.ImageUser;

                    user.ImageUser = model.ImageUser;
                }

                if (string.IsNullOrEmpty(model.Password))
                {
                    user.ImageUser = user.ImageUser;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        
                            var oldRole = await _userManager.GetRolesAsync(user);
                            await _userManager.RemoveFromRolesAsync(user, oldRole);
                            var newRole = await _userManager.AddToRoleAsync(user, model.RoleName);
                            if (newRole.Succeeded)
                                return RedirectToAction(nameof(Index));
                    }
                        
                }
                else
                {
                    user.ImageUser = user.ImageUser;
                    await _userManager.RemovePasswordAsync(user);
                    var result = await _userManager.AddPasswordAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                       
                            var oldRole = await _userManager.GetRolesAsync(user);
                            await _userManager.RemoveFromRolesAsync(user, oldRole);
                            var newRole = await _userManager.AddToRoleAsync(user, model.RoleName);
                            if (newRole.Succeeded)
                                return RedirectToAction(nameof(Index)); 
                    }
                }
            }
            
            return View(model);
        }

        //[Authorize(Roles = "Admin")]
        public IActionResult Delete(string? id)
        {
            var result = _userManager.Users.FirstOrDefault(x => x.Id == id);
            if (result != null)
            {
                var user = new NewRegisterViewModel
                {
                    Code = result.Id,
                    FullName = result.FullName,
                    Email = result.Email,
                    ActiveUser = result.ActiveUser,
                    ImageUser = result.ImageUser
                };
                return View(user);
            }
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(NewRegisterViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Code);

            if (user.ImageUser != null && user.ImageUser != Guid.Empty.ToString())
            {
                var imagePath = Path.Combine(@"wwwroot/", "/Images/Users", user.ImageUser);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }

            var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
            
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                    ViewBag.ErrorLogin = false;
            }

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> LogOut(LoginViewModel model)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

    }
}