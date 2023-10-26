using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DaoMinhTri_NET1606_A03_UI.Models;

namespace DaoMinhTri_NET1606_A03_UI.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly DaoMinhTri_NET1606_A03_UI.Models.FUCarRentingManagementContext _context;

        public IndexModel(DaoMinhTri_NET1606_A03_UI.Models.FUCarRentingManagementContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Customers != null)
            {
                Customer = await _context.Customers.ToListAsync();
            }
        }
    }
}
