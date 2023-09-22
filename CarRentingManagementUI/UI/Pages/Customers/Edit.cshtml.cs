using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using UI.ViewModels.ErrorObject;
using UI.ViewModels.Users;

namespace UI.Pages.Customers
{
    public class EditModel : PageModel
    {
        //Need to add message
        private readonly HttpClient _httpClient;

        public EditModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string? InforMessage { get; set; }
        public string? ErrorMessage { get; set; }
        public int CustomerId { get; set; }

        [BindProperty] public UpdateUserRequest UpdateUser { get; set; } = new UpdateUserRequest();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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
                var user = JsonConvert.DeserializeObject<UserResponse>(content);
                CustomerId = user.CustomerId;
                UpdateUser.Email = user.Email;
                UpdateUser.Telephone = user.Telephone;
                UpdateUser.CustomerName = user.CustomerName;
                if (user.CustomerBirthday != null && !user.CustomerBirthday.Trim().Equals(""))
                {
                    DateTime dateTime;
                    bool isValid = DateTime.TryParseExact(user.CustomerBirthday, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out dateTime);
                    if (isValid)
                    {
                        UpdateUser.CustomerBirthday = dateTime;
                    }
                }

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id == null)
            {
                return RedirectToPage("/Error", new
                {
                    title = "Bad Request.",
                    message = "Id is missing."
                });
            }

            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7214/api/customers/{id}", UpdateUser);

            if (response.IsSuccessStatusCode)
            {
                InforMessage = "Update Success.";
                ErrorMessage = null;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var content = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<ErrorDetail>(content);
                ErrorMessage = error?.Message ?? "Bad request";
                InforMessage = null;
            }
            else if (response.StatusCode == HttpStatusCode.Conflict)
            {
                var content = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<ErrorDetail>(content);
                ErrorMessage = error?.Message ?? "Resource conflict.";
                InforMessage = null;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                var content = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<ErrorDetail>(content);
                ErrorMessage = error?.Message ?? "Resource not found.";
                InforMessage = null;
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                var content = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<ErrorDetail>(content);
                ErrorMessage = error?.Message ?? "Internal server error.";
                InforMessage = null;
            }

            return Page();
        }
    }
}