using System;

namespace MailServices
{
    public interface IMailServices
    {
        public void Send(string title, string to, string body);
    }
}
