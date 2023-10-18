using System;
using Microsoft.AspNetCore.Http;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Gestion_Parc.ViewModel.Helper;

namespace Gestion_Parc.IRepository.ServiceRepository
{
	public class ServiceBreakDown : IServiceRepositoryBreakDown<BreakDown>
	{
        private readonly AppDbContxt _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServiceBreakDown(AppDbContxt db, UserManager<ApplicationUser> userManager,
                               IHttpContextAccessor httpContextAccessor)
		{
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public BreakDown FindBy(Guid id)
        {
            try
            {
                var breakDown = _db.BreakDowns.Include(x => x.User)
                                              .FirstOrDefault(x => x.Id.Equals(id));
                return breakDown;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<BreakDown> GetAll()
        {
            try
            {
                var breakDowns = _db.BreakDowns.Include(x => x.User)
                                               .ToList();
                return breakDowns;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<BreakDown> GetAllForEachUser()
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                var userId = _userManager.GetUserId(httpContext.User);
                var breakDowns = _db.BreakDowns.Include(x => x.User)
                                              .OrderBy(x => x.Date).Where(x => x.UserId.Equals(userId)).ToList();
                return breakDowns;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<BreakDown> GetAllReported()
        {
            try
            {
                var breakDowns = _db.BreakDowns.Include(x => x.User)
                                              .Where(x => x.Status.Equals(Status.Reported.ToString()))
                                              .ToList();
                return breakDowns;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<BreakDown> GetAllCompleted()
        {
            try
            {
                var breakDowns = _db.BreakDowns.Include(x => x.User)
                                              .Where(x => x.Status.Equals(Status.Completed.ToString()))
                                              .ToList();
                return breakDowns;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<BreakDown> GetAllInProgress()
        {
            try
            {
                var breakDowns = _db.BreakDowns.Include(x => x.User)
                                              .Where(x => x.Status.Equals(Status.InProgress.ToString()))
                                              .ToList();
                return breakDowns;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Save(BreakDown model, string userId)
        {
            try
            {
                model.Id = Guid.NewGuid();
                var breakDown = new BreakDown
                {
                    Id = model.Id,
                    Date = DateTime.Now,
                    Type = model.Type,
                    Category = model.Category,
                    SubCategory = model.SubCategory,
                    Description = model.Description,
                    Status = Status.Reported.ToString(),
                    UserId = userId,
                    User = null
                };
                _db.BreakDowns.Add(breakDown);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<BreakDown> GetAllComputers()
        {
            var computers = _db.BreakDowns.Where(x => x.Category == "Computer").ToList();
            return computers;
        }

        public List<BreakDown> GetAllPrinters()
        {
            var printers = _db.BreakDowns.Where(x => x.Category == "Printer").ToList();
            return printers;
        }

        public List<BreakDown> GetAllServers()
        {
            var servers = _db.BreakDowns.Where(x => x.Category == "Server").ToList();
            return servers;
        }

        public List<BreakDown> GetAllOthers()
        {
            var others = _db.BreakDowns.Where(x => x.Category == "Other").ToList();
            return others;
        }
    }
}

