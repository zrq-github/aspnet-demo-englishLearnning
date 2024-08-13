using System;

namespace FluentValidation;

/// <summary>
/// URI验证
/// </summary>
public static class UriValidators
{
    /// <summary>
    /// 不是空的URI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ruleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, Uri> NotEmptyUri<T>(this IRuleBuilder<T, Uri> ruleBuilder)
    {
        return ruleBuilder.Must(p => p == null || !string.IsNullOrWhiteSpace(p.OriginalString))
            .WithMessage("The Uri must not be null nor empty.");
    }

    /// <summary>
    /// 验证URI的长度
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ruleBuilder"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, Uri> Length<T>(this IRuleBuilder<T, Uri> ruleBuilder, int min, int max)
    {
        //为空则跳过检查，因为有专门的NotEmptyUri判断，也许一个东西允许空，但是不为空的时候再限制长度
        return ruleBuilder.Must(p => string.IsNullOrWhiteSpace(p.OriginalString)
            || (p.OriginalString.Length >= min && p.OriginalString.Length <= max))
            .WithMessage($"The length of Uri must not be between {min} and {max}.");
    }
} 