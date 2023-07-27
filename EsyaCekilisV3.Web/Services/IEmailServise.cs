namespace EsyaCekilisV3.Web.Services
{
    public interface IEmailServise
    {
        Task SendResetPasswordEmail(string resetPasswordEmailLink, string ToEmail);
    }
}
