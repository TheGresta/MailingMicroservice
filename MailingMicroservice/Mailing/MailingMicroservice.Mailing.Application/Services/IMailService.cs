using MailingMicroservice.Mailing.Application.Dtos;

namespace MailingMicroservice.Mailing.Application.Services;

public interface IMailService
{
    void SendMail(Mail mail);
}