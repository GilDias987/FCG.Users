using FCG.Users.Application.Dto.User;
using FCG.Users.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Queries.GetAllUser
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var users = (await _userRepository.GetAllUsers()).Select(s => new UserDto
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                UserGroupId = s.UserGroupId,
                Group = s.UserGroup.Name
            });

            if (!users.Any())
            {
                throw new ArgumentException("Nenhum registro encontrado.");
            }

            return users.ToList();
        }
    }
}
