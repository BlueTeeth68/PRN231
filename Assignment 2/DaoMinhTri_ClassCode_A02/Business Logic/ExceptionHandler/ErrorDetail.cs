using System.Text.Json;

namespace Business_Logic.ExceptionHandler;

public class ErrorDetail
{
    public int StatusCode { get; set; }
    public string? Title { get; set; }
    public string? Message { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}