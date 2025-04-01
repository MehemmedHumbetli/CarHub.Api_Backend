using Domain.Entities;
using Domain.Enums;

namespace Application.CQRS.Users.ResponseDtos;

public class GetByIdDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public UserRoles UserRole { get; set; }
    public List<Car> UserCars { get; set; } 
    public string UserImagePath { get; set; }
    public List<int> FavoriteCarIds { get; set; } 
}
