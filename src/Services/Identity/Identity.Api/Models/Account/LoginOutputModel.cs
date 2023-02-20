namespace Identity.Api.Models.Account
{
    public record LoginOutputModel
    {
        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }
    }
}
