using Notebook.Data.Model;

namespace Notebook.Data.Interface
{
    public interface IEmail
    {
        void SendEmail(EmailLetter letter);
    }
}