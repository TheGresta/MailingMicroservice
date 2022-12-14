using MailingMicroservice.Mailing.Application.Dtos;
using MailingMicroservice.Mailing.Application.Messages;
using MailingMicroservice.Mailing.Application.Services;
using MailingMicroservice.Mailing.Application.Services.MailKitImplementations;
using MassTransit;

namespace MailingMicroservice.Mailing.Application.Consumer;

public class CreateMemeMailCommandConsumer : IConsumer<CreateMemeMailCommand>
{
    private readonly IMailService _mailService;

    public CreateMemeMailCommandConsumer(IMailService mailService)
    {
        _mailService = mailService;
    }

    public Task Consume(ConsumeContext<CreateMemeMailCommand> context)
    {
        Mail mail = new()
        {
            Subject = "Welcome",
            TextBody = File.ReadAllText("../Constants/TextBodies/GoodMeMe.txt"),
            HtmlBody = File.ReadAllText("../Constants/HTMLBodies/GoodMeMe.HTML"),
            ToFullName = context.Message.ToFullName,
            ToEmail = context.Message.ToEmail
        };

        _mailService.SendMail(mail);

        return Task.CompletedTask;
    }
}

