using System.ComponentModel.DataAnnotations;

namespace Identity.Api.Models.Account
{
    public sealed class LoginInputModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; init; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string? Password { get; init; }
    }
}
