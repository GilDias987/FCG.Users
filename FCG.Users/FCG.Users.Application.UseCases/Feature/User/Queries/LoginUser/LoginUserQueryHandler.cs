using FCG.Users.Application.Dto.User;
using FCG.Users.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public LoginUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var argumentException = new ArgumentException("E-mail ou senha inválidos.");

            var userEmail = await _userRepository.UserEmailAsync(request.Email.Trim());

            if (userEmail is null)
                throw argumentException;

            var passwordValid = userEmail.VerifyPassword(request.Password.Trim());

            if (!passwordValid)
                throw argumentException;

            return new UserDto
            {
                Id = userEmail.Id,
                Email = userEmail.Email,
                Name = userEmail.Name,
                Group = userEmail.UserGroup.Name
            };
        }
    }
}
