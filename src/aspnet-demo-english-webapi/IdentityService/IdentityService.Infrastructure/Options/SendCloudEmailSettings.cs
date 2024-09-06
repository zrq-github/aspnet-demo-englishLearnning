namespace IdentityService.Infrastructure.Options
{
    /// <summary>
    /// 发送邮件的设置
    /// </summary>
    public class SendCloudEmailSettings
    {
        public string ApiUser { get; set; }
        public string ApiKey { get; set; }
        public string From { get; set; }
    }
}
