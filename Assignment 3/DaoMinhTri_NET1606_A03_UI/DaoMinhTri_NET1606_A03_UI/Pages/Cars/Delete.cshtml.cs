using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DaoMinhTri_NET1606_A03_UI.Models;

namespace DaoMinhTri_NET1606_A03_UI.Pages.Cars
{
    public class DeleteModel : PageModel
    {
        private readonly DaoMinhTri_NET1606_A03_UI.Models.FUCarRentingManagementContext _context;

        public DeleteModel(DaoMinhTri_NET1606_A03_UI.Models.FUCarRentingManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
      public CarInformation CarInformation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CarInformations == null)
            {
                return NotFound();
            }

            var carinformation = await _context.CarInformations.FirstOrDefaultAsync(m => m.CarId == id);

            if (carinformation == null)
            {
                return NotFound();
            }
            else 
            {
                CarInformation = carinformation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.CarInformations == null)
            {
                return NotFound();
            }
            var carinformation = await _context.CarInformations.FindAsync(id);

            if (carinformation != null)
            {
                CarInformation = carinformation;
                _context.CarInformations.Remove(CarInformation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
