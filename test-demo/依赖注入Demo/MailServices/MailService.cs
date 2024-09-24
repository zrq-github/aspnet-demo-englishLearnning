using ConfigServices;
using LogServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailServices
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// mailkit 
    /// </remarks>
    public class MailService:IMailServices
    {
        private readonly IConfigServices _configServices;
        private readonly ILogProvider _logProvider;

        public MailService(IConfigServices configServices, ILogProvider logProvider)
        {
            _configServices = configServices;
            _logProvider = logProvider;
        }

        public void Send(string title, string to, string body)
        {
            _logProvider.LogInfo("准备发送邮件了");
            var smtpServer = _configServices.GetValue("SmtpServer");
            Console.WriteLine($"SendMail: title:{title},to:{to},body{body}");
            _logProvider.LogInfo("邮件发送完成");
        }
    }
}
