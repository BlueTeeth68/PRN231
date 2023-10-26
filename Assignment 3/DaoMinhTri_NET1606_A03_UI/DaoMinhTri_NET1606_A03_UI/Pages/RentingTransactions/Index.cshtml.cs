using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DaoMinhTri_NET1606_A03_UI.Models;

namespace DaoMinhTri_NET1606_A03_UI.Pages.RentingTransactions
{
    public class IndexModel : PageModel
    {
        private readonly DaoMinhTri_NET1606_A03_UI.Models.FUCarRentingManagementContext _context;

        public IndexModel(DaoMinhTri_NET1606_A03_UI.Models.FUCarRentingManagementContext context)
        {
            _context = context;
        }

        public IList<RentingTransaction> RentingTransaction { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.RentingTransactions != null)
            {
                RentingTransaction = await _context.RentingTransactions
                .Include(r => r.Customer).ToListAsync();
            }
        }
    }
}
