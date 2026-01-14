using FCG.Users.Application.Dto.User;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Feature.User.Commands.AddUser
{
    public class TesteConsumer : IConsumer<UserDto>
    {
        public async Task Consume(ConsumeContext<UserDto> context)
        {
            var dados = context.Message;
            // Lógica de negócio aqui
            await Task.CompletedTask;
        }
    }
}
