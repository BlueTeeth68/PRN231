﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DaoMinhTri_NET1606_A03_UI.Models;

namespace DaoMinhTri_NET1606_A03_UI.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        private readonly DaoMinhTri_NET1606_A03_UI.Models.FUCarRentingManagementContext _context;

        public DeleteModel(DaoMinhTri_NET1606_A03_UI.Models.FUCarRentingManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }
            else 
            {
                Customer = customer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);

            if (customer != null)
            {
                Customer = customer;
                _context.Customers.Remove(Customer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
