namespace EssenceShop.Models
{
    public class TokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }


        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryDate { get; set; }
    }
}
