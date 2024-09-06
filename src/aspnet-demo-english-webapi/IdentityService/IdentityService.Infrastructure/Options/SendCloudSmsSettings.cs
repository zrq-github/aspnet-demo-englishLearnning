namespace IdentityService.Infrastructure.Options
{
    /// <summary>
    /// 发送短信的设置
    /// </summary>
    public class SendCloudSmsSettings
    {
        public string SmsUser { get; set; }
        public string SmsKey { get; set; }
    }
}
