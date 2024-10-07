using Application.Services.User.Commands.Create;
using Application.Services.Users;
using Mapster;
using Presentation.Contracts.Users;

namespace Presentation.Mapping;

public class UserMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserRequest, CreateUserCommand>()
              .Map(dest => dest.FirstName, src => src.FirstName)
              .Map(dest => dest.LastName, src => src.LastName)
              .Map(dest => dest.Email, src => src.Email)
              .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
              .Map(dest => dest.DateOfBirth, src => src.DateOfBirth)
              .Map(dest => dest.Password, src => src.Password)
              .Map(dest => dest.Role, src => src.Role)
              .Map(dest => dest.Balance, src => src.Balance);

        config.NewConfig<UserResult, UserResponse>()
              .Map(dest => dest, src => src);
    }
}