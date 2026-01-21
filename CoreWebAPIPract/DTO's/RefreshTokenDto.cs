namespace CoreWebAPIPract.DTO_s
{
    public class RefreshTokenDto
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; }
        public string UserId { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }
    }
}
