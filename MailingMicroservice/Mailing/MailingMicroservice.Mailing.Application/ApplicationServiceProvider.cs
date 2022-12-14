using MailingMicroservice.Mailing.Application.Services;
using MailingMicroservice.Mailing.Application.Services.MailKitImplementations;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MailingMicroservice.Mailing.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddScoped<IMailService, MailKitMailService>();

        return services;
    }
}

