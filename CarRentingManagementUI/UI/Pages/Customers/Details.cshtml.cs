using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using UI.Models;
using UI.ViewModels.ErrorObject;
using UI.ViewModels.Users;

namespace UI.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DetailsModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public UserResponse? User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var email = HttpContext.Session.GetString("email");
            if (email == null)
            {
              return  RedirectToPage("/Login");
            }
            
            if (id == null)
            {
                return RedirectToPage("/Error", new
                {
                    title = "Bad Request.",
                    message = "Id is missing."
                });
            }

            var response = await _httpClient.GetAsync($"https://localhost:7214/api/customers/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                User = JsonConvert.DeserializeObject<UserResponse>(content);
                return Page();
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<ErrorDetail>(content);
                return RedirectToPage("/Error", new
                {
                    title = error?.Title ?? "Undefined error.",
                    message = error?.Message ?? "Something wrong."
                });
            }
        }
    }
}