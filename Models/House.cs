using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Image
{
    public int Id { get; set; } // Primary key
    public string? ImageUrl { get; set; }
    public int HouseId { get; set; } // Foreign key to House

    public House? House { get; set; } // Navigation property
}
public class House
{
    public int Id { get; set; }

    public string? Address { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Bedrooms cannot be less than 0.")]
    public int Bedrooms { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Bathrooms cannot be less than 0.")]
    public int Bathrooms { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Price cannot be less than 0.")]
    public double Price { get; set; }

    public double Area { get; set; } // Area in square meters

    [Range(0, int.MaxValue, ErrorMessage = "Number of rooms cannot be less than 0.")]
    public int NumberOfRooms { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Floor cannot be less than 0.")]
    public int Floor { get; set; }

    public bool IsSold { get; set; }

    public List<Image> Images { get; set; } = new List<Image>();
}
