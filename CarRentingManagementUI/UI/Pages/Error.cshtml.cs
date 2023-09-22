using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace UI.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        // public string? RequestId { get; set; }

        public string? Title { get; set; }
        public string? ErrorMessage { get; set; }

        // public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(string? message, string? title)
        {
            if (message != null)
                ErrorMessage = message;
            if (title != null)
                Title = title;

            // RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}