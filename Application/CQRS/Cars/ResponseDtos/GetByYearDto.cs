using Domain.Entities;
using Domain.Enums;

namespace Application.CQRS.Cars.ResponseDtos;

public class GetByYearDto
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string BrandImagePath { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int Price { get; set; }
    public FuelTypes Fuel { get; set; }
    public TransmissionTypes Transmission { get; set; }
    public double Miles { get; set; }
    public List<CarImage> CarImagePaths { get; set; }
    public BodyTypes Body { get; set; }
    public string BodyTypeImage { get; set; }
    public string Color { get; set; }
    public string VIN { get; set; }
    public string Text { get; set; }
    public List<UserFavorite> FavoritedByUsers { get; set; }
}
