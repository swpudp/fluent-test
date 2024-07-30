namespace FluentTest.Identity.Request
{
    public class LoginRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }
        public string TwoFactorCode { get; set; }
        public string TwoFactorRecoveryCode { get; set; }
    }
}