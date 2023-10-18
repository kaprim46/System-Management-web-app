using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.ViewModel;

namespace Gestion_Parc.Views.Account
{
    public class RegisterModel : PageModel
    {
        private readonly Gestion_Parc.DataDbContext.AppDbContxt _context;

        public RegisterModel(Gestion_Parc.DataDbContext.AppDbContxt context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.RegisterViewModel == null || RegisterViewModel == null)
            {
                return Page();
            }

            _context.RegisterViewModel.Add(RegisterViewModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
