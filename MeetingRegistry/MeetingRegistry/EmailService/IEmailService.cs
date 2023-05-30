namespace MeetingRegistry.EmailService
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string email);
    }
}
