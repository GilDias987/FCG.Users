using FCG.Users.Application.Dto.User;
using FCG.Users.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Queries.GetUser
{

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(request.Id);
            if (user is null)
            {
                throw new ArgumentException("Usuário não encontrado.");
            }

            return new UserDto { Id = user.Id, 
                                 Email = user.Email, 
                                 Name = user.Name, 
                                 UserGroupId = user.UserGroupId ,
                                 Group = user.UserGroup.Name
                                };
        }
    }
}
