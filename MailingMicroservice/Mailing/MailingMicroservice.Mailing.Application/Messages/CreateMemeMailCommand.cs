namespace MailingMicroservice.Mailing.Application.Messages;

public class CreateMemeMailCommand
{
    public string ToFullName { get; set; }
    public string ToEmail { get; set; }

    public CreateMemeMailCommand()
    {
    }

    public CreateMemeMailCommand(string toFullName, string toEmail)
    {
        ToFullName = toFullName;
        ToEmail = toEmail;
    }
}

