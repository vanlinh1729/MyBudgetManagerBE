namespace MyBudgetManager.Application.Interfaces.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
    Task SendActivateEmailAsync(string to,string name , string activationToken);

    
}