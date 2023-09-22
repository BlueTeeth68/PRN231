using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using UI.ViewModels.Transaction;

namespace UI.Pages.RentingTransactions
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DetailsModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public RentingReponse RentingTransaction { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var email = HttpContext.Session.GetString("email");
            if (email == null)
            {
               return RedirectToPage("/Login");
            }

            if (id == null)
            {
                return RedirectToPage("/Error", new
                {
                    message = "Id is missing.",
                    title = "Bad Request."
                });
            }

            var response = await _httpClient.GetAsync($"https://localhost:7214/api/transactions/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                RentingTransaction = JsonConvert.DeserializeObject<RentingReponse>(content);
                return Page();
            }

            return Page();
        }
    }
}