using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using UI.ViewModels.CarInformation;
using UI.ViewModels.ErrorObject;

namespace UI.Pages.CarInformations
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public CreateModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string? ErrorMessage { get; set; }
        public string? InforMessage { get; set; }

        [BindProperty] public CreateCarInformationRequest CarInformation { get; set; } = default!;


        public async Task<ActionResult> OnGet()
        {
            var getManufacturersTask = GetAllManufacturerAsync();
            var getSupplierTask = GetAllSupplierAsync();
            await Task.WhenAny(getManufacturersTask, getSupplierTask);

            var manufacturers = getManufacturersTask.Result.Value;
            var suppliers = getSupplierTask.Result.Value;

            ViewData["Manufacturer"] = new SelectList(manufacturers, "ManufacturerId", "ManufacturerName");
            ViewData["Supplier"] = new SelectList(suppliers, "SupplierId", "SupplierName");
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response =
                await _httpClient.PostAsJsonAsync("https://localhost:7214/api/car-informations", CarInformation);

            if (response.IsSuccessStatusCode)
            {
                InforMessage = "Create Success.";
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
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                var content = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<ErrorDetail>(content);
                ErrorMessage = error?.Message ?? "Internal server error.";
                InforMessage = null;
            }
            else
            {
                ErrorMessage = "Error when create new car.";
            }

            return Page();
        }

        private async Task<ActionResult<List<ManufactureResponse>>> GetAllManufacturerAsync()
        {
            var response = await _httpClient.GetAsync($"https://localhost:7214/api/manufacturers");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ManufactureResponse>>(content);
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

        private async Task<ActionResult<List<SupplierResponse>>> GetAllSupplierAsync()
        {
            var response = await _httpClient.GetAsync($"https://localhost:7214/api/supplier");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<SupplierResponse>>(content);
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