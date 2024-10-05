namespace System
{
    public static class StringExtensions
    {
        public static bool EqualsIgnoreCase(this string s1, string s2)
        {
            return string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 截取字符串s1最多前maxLen个字符
        /// </summary>
        /// <param name="s1">字符串对象</param>
        /// <param name="maxLen">截取字符数量</param>
        /// <returns></returns>
        public static string Cut(this string s1, int maxLen)
        {
            int len = s1.Length <= maxLen ? s1.Length : maxLen; //不能超过字符串的最大大小
            return s1[0..len];
        }
    }
}
