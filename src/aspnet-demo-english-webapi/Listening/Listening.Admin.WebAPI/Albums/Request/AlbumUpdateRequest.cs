
using FluentValidation;

namespace Listening.Admin.WebAPI.Albums;
public record AlbumUpdateRequest(MultilingualString Name);

/// <summary>
/// 专辑更新请求验证
/// </summary>
public class AlbumUpdateRequestValidator : AbstractValidator<AlbumUpdateRequest>
{
    public AlbumUpdateRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name.Chinese).NotNull().Length(1, 200);
        RuleFor(x => x.Name.English).NotNull().Length(1, 200);
    }
}