using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using UI.ViewModels.Users;

namespace UI.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient client)
        {
            _httpClient = client;
        }

        public IList<UserResponse>? Users { get; set; } = new List<UserResponse>();

        public async Task<ActionResult> OnGetAsync()
        {
            var email = HttpContext.Session.GetString("email");
            if (email == null)
            {
               return RedirectToPage("/Login");
            }
            
            var response = await _httpClient.GetAsync("https://localhost:7214/api/customers");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Users = JsonConvert.DeserializeObject<List<UserResponse>>(content);
            }
            else
            {
                Users = new List<UserResponse>();
            }

            return Page();
        }
        
    }
}
