using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using UI.ViewModels.Transaction;

namespace UI.Pages.RentingTransactions
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IList<RentingReponse> RentingTransaction { get; set; } = new List<RentingReponse>();

        public async Task<ActionResult> OnGetAsync()
        {
            
            var email =  HttpContext.Session.GetString("email");
            if (email == null)
            {
               return RedirectToPage("/Login");
            }
            var response = await _httpClient.GetAsync("https://localhost:7214/api/transactions");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                RentingTransaction = JsonConvert.DeserializeObject<List<RentingReponse>>(content);
            }
            else
            {
                RentingTransaction = new List<RentingReponse>();
            }

            return Page();
        }
    }
}