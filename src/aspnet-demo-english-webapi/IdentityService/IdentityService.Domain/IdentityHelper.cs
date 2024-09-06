namespace Microsoft.AspNetCore.Identity;
public static class IdentityHelper
{
    /// <summary>
    /// 用户登录错误计数
    /// </summary>
    /// <returns></returns>
    public static string SumErrors(this IEnumerable<IdentityError> errors)
    {
        var strs = errors.Select(e => $"code={e.Code},message={e.Description}");
        return string.Join('\n', strs);
    }
}
