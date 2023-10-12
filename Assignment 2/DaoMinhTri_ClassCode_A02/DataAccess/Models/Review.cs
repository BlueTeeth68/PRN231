namespace DataAccess.Models;

public class Review:BaseEntity
{

    public int CustomerId { get; set; }
    public int CarId { get; set; }
    public int ReviewStar { get; set; }
    public string Comment { get; set; } = string.Empty;

    public virtual Customer Customer { get; set; } = null!;
    public virtual Car Car { get; set; } = null!;
}