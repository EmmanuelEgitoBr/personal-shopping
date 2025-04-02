namespace Personal.Shopping.Web.Utils
{
    public class AppSettings
    {
        private readonly IConfiguration _configuration;

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public string? CouponApiBase = _configuration.GetConnectionString("");
    }
}
