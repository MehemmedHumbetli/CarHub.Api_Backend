using Domain.BaseEntities;
using Domain.Enums;

namespace Domain.Entities;

public class Car : BaseEntity
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int Price { get; set; }
    public FuelTypes Fuel { get; set; }
    public TransmissionTypes Transmission { get; set; }
    public double Miles { get; set; }
    public List<string> CarImagePaths { get; set; }
    public BodyTypes Body { get; set; }
    public string Color { get; set; }
    public string VIN { get; set; }
    public string Text { get; set; }
    public List<User> FavoritedByUsers { get; set; }
}
