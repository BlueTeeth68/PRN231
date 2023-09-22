using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using UI.ViewModels.ErrorObject;
using UI.ViewModels.Users;

namespace UI.Pages
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public LoginModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string? ErrorMessage { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email can not empty")]
        [EmailAddress(ErrorMessage = "Email is not in correct formats.")]
        public string Email { get; set; } = null!;

        [BindProperty]
        [Required(ErrorMessage = "Password can not be empty.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 4,
            ErrorMessage = "Password must not less than 4 character and can not exceed 20 character")]
        public string Password { get; set; } = null!;

        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7214/api/customers/authenticate/login", new
            {
                Email = Email,
                Password = Password
            });

            if (response.IsSuccessStatusCode)
            { 
                var content = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserResponse>(content);
                HttpContext.Session.SetString("fullName", user?.CustomerName ?? "");
                HttpContext.Session.SetString("email", user?.Email ?? "");
                return RedirectToPage("/RentingTransactions/Index");
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<ErrorDetail>(content);
                ErrorMessage = error?.Message ?? "Undefined Error.";
                return Page();
            }
        }
    }
}