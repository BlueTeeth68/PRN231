using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UI.Models;

namespace UI.Pages.RentingTransactions
{
    public class IndexModel : PageModel
    {
        private readonly UI.Models.FUCarRentingManagementContext _context;

        public IndexModel(UI.Models.FUCarRentingManagementContext context)
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
