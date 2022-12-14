using MailingMicroservice.Registering.Application.Dtos;
using MailingMicroservice.Registering.Application.Messages;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace MailingMicroservice.Registering.Application.Commands;

public class RegisterCommand : IRequest<RegisterCommandReadDto>
{
    public RegisterCommandWriteDto RegisterCommandWriteDto { get; set; }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandReadDto>
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IConfiguration _configuration;

        public RegisterCommandHandler(ISendEndpointProvider sendEndpointProvider, IConfiguration configuration)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _configuration = configuration;
        }

        public async Task<RegisterCommandReadDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var sendEndPoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(_configuration.GetSection("EndPoints:MemeMail").Value));

            CreateMemeMailCommand createMemeMailCommand = new()
            {
                ToFullName = $"{request.RegisterCommandWriteDto.FirstName} + {request.RegisterCommandWriteDto.LastName}",
                ToEmail = request.RegisterCommandWriteDto.Email
            };

            await sendEndPoint.Send<CreateMemeMailCommand>(createMemeMailCommand);

            return new() { Message = "Mail send" };
        }
    }
}