using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using UI.ViewModels.CarInformation;

namespace UI.Pages.CarInformations
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IList<CarInformationResponse>? CarInformation { get;set; }

        public async Task<ActionResult> OnGetAsync()
        {
            var email = HttpContext.Session.GetString("email");
            if (email == null)
            {
               return RedirectToPage("/Login");
            }
            
            var response = await _httpClient.GetAsync("https://localhost:7214/api/car-informations");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                CarInformation = JsonConvert.DeserializeObject<List<CarInformationResponse>>(content);
            }
            else
            {
                CarInformation = new List<CarInformationResponse>();
            }

            return Page();
        }
    }
}
