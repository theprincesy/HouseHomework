using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
namespace MvcHouse.Models;
public class HouseSearchCriteria
{
    public List<House>? Houses { get; set; }
    public string? Address{ get; set;}
    public SelectList? Area { get; set; }
    public int NumberOfRooms { get; set; }
    public double MaxPrice { get; set; }
    // Add other search criteria as needed
}