namespace Identity.Repository
{
    /// <summary>
    /// 短信发送接口
    /// </summary>
    public interface ISmsSender
    {
        public Task SendAsync(string phoneNum, params string[] args);
    }
}
