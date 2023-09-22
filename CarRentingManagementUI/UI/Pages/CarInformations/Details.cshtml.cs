using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using UI.ViewModels.CarInformation;
using UI.ViewModels.ErrorObject;

namespace UI.Pages.CarInformations
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DetailsModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public CarInformationResponse? CarInformation { get; set; }

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

            var response = await _httpClient.GetAsync($"https://localhost:7214/api/car-informations/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                CarInformation = JsonConvert.DeserializeObject<CarInformationResponse>(content);
                return Page();
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorDetail = JsonConvert.DeserializeObject<ErrorDetail>(content);
                return RedirectToPage("/Error", new
                {
                    message = errorDetail?.Message ?? "Something wrong.",
                    title = errorDetail?.Title ?? "Undefined Error."
                });
            }
        }
    }
}