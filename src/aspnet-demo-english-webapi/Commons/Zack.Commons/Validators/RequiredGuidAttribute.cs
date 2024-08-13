// ReSharper disable once CheckNamespace
namespace System.ComponentModel.DataAnnotations;

/// <summary>
/// Value of Guid is not null nor Guid.Empty.
/// On asp.net core, if there is a parameter of Guid type, and there is no value for it, the value is Guid.Empty, but [Required] doesn't take Guid.Empty as invalid,
/// so please add  RequiredGuidAttribute to a parameter, property or field.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class RequiredGuidAttribute : ValidationAttribute
{
    public const string DefaultErrorMessage = "The {0} field is requird and not Guid.Empty";
    public RequiredGuidAttribute() : base(DefaultErrorMessage) { }

    public override bool IsValid(object? value)
    {
        switch (value)
        {
            case null:
                return false;
            case Guid guid:
            {
                return guid != Guid.Empty;
            }
            default:
                return false;
        }
    }
}