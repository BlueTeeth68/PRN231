using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UI.ViewModels.CarInformation;
using UI.ViewModels.ErrorObject;

namespace UI.Pages.CarInformations
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public EditModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string? ErrorMessage { get; set; }
        public string? InforMessage { get; set; }
        public int? Id { get; set; }

        [BindProperty]
        public UpdateCarInformationRequest CarInformation { get; set; } = new UpdateCarInformationRequest();

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
                var car = JsonConvert.DeserializeObject<CarInformationResponse>(content);
                Id = car.CarId;
                CarInformation.CarName = car.CarName;
                CarInformation.CarDescription = car.CarDescription;
                CarInformation.NumberOfDoors = car.NumberOfDoors;
                CarInformation.CarStatus = car.CarStatus;
                CarInformation.Year = car.Year;
                CarInformation.FuelType = car.FuelType;
                CarInformation.SeatingCapacity = car.SeatingCapacity;
                CarInformation.CarRentingPricePerDay = car.CarRentingPricePerDay;
                CarInformation.ManufacturerId = car.Manufacturer.ManufacturerId;
                CarInformation.SupplierId = car.Supplier.SupplierId;
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


            var getManufacturersTask = GetAllManufacturerAsync();
            var getSupplierTask = GetAllSupplierAsync();
            await Task.WhenAny(getManufacturersTask, getSupplierTask);

            var manufacturers = getManufacturersTask.Result.Value;
            var suppliers = getSupplierTask.Result.Value;

            ViewData["Manufacturer"] = new SelectList(manufacturers, "ManufacturerId", "ManufacturerName");
            ViewData["Supplier"] = new SelectList(suppliers, "SupplierId", "SupplierName");
            return Page();
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

            var response =
                await _httpClient.PutAsJsonAsync($"https://localhost:7214/api/car-informations/{id}", CarInformation);

            if (response.IsSuccessStatusCode)
            {
                InforMessage = "Update Success.";
                ErrorMessage = null;
            }
            else 
            {
                var content = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<ErrorDetail>(content);
                ErrorMessage = error?.Message ?? "Undefined error.";
                InforMessage = null;
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
            var response = await _httpClient.GetAsync($"https://localhost:7214/api/suppliers");
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