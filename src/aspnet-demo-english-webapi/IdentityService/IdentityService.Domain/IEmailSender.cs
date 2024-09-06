namespace Identity.Repository
{
    /// <summary>
    /// 邮件发送接口
    /// </summary>
    public interface IEmailSender
    {
        public Task SendAsync(string toEmail, string subject, string body);
    }
}
