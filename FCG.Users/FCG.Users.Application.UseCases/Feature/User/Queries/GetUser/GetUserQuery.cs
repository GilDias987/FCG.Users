using FCG.Users.Application.Dto.User;
using MediatR;

namespace FCG.Users.Application.UseCases.Feature.User.Queries.GetUser
{

    public class GetUserQuery : IRequest<UserDto>
    {
        public int Id { get; set; }
    }
}
