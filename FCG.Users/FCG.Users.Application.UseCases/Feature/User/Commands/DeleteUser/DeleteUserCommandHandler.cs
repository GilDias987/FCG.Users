using FCG.Users.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var repUser = await _userRepository.GetByIdAsync(request.Id);
            if (repUser != null)
            {
                await _userRepository.DeleteAsync(repUser.Id);

                return true;
            }
            else
            {
                return false;

                throw new ArgumentException("Usuário não foi encontrado.");
            }
        }
    }
}
