using System.Text.Json;
using BusinessLogic.Utils;

namespace BusinessLogic.ErrorHandlers;

public class ErrorDetail
{
    public int StatusCode { get; set; }
    public string? Title { get; set; }
    public string? Message { get; set; }
    public string Date { get; set; } = DateTimeUtils.FormatDateTimeToDateV1(DateTime.Now);
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}