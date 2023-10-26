using DaoMinhTri_NET1606_A03_UI.ViewModels.Customers;
using DaoMinhTri_NET1606_A03_UI.ViewModels.ErrorObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace DaoMinhTri_NET1606_A03_UI.Pages
{
    public class RegisterModel : PageModel
    {
        
        private readonly HttpClient _httpClient;

        public RegisterModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string? ErrorMessage { get; set; }
        public int? StatusCode { get; set; }

        [BindProperty]
        public CreateCustomerDto Customer { get; set; } = null!;
        
        public IActionResult OnGet()
        {
            var email = HttpContext.Session.GetString("email");
            if (email != null)
            {
                //Check role
                if (email != "admin@FUCarRentingSystem.com")
                    return RedirectToPage("/Customers/Index");
            }

            return Page();
        }
        
        public async Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5044/api/Authenticate/register", new CreateCustomerDto
            {
                Email = Customer.Email.Trim(),
                Password = Customer.Password,
                Telephone = Customer.Telephone,
                CustomerBirthday = Customer.CustomerBirthday,
                CustomerName = Customer.CustomerName

            });

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<LoginCustomerDto>(content);
                HttpContext.Session.SetString("fullName", user?.CustomerName ?? "");
                HttpContext.Session.SetString("email", user?.Email ?? "");
                HttpContext.Session.SetString("id", user?.CustomerId.ToString() ?? "");
                HttpContext.Session.SetString("token", user?.Token ?? "");
                //Check role
                if (user?.Email != "admin@FUCarRentingSystem.com")
                    return RedirectToPage("/Customers/Index");
                return Page();
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<ErrorDetail>(content);
                ErrorMessage = error?.Message ?? "Undefined Error.";
                StatusCode = error?.StatusCode ?? null;
                return Page();
            }
        }
    }
}
