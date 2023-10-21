public class Sale
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public int HouseId { get; set; }
    public DateTime SaleDate { get; set; }
    public double SalePrice { get; set; }

    
    public House? House { get; set; }

    
}
