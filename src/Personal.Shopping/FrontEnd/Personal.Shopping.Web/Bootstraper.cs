using Personal.Shopping.Web.Services;
using Personal.Shopping.Web.Services.Base;
using Personal.Shopping.Web.Services.Interfaces;
using Personal.Shopping.Web.Services.Interfaces.Base;
using Personal.Shopping.Web.Settings;

namespace Personal.Shopping.Web;

public static class Bootstraper
{
    public static void AddAppServices(this WebApplicationBuilder builder)
    {
        AppSettings.CouponBaseUrl = builder.Configuration.GetValue<string>("ServicesUrls:CouponApi")!;

        builder.Services.AddScoped<IBaseService, BaseService>();
        builder.Services.AddScoped<ICouponService, CouponService>();
    }

    public static void AddHttpConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddHttpClient();
        //builder.Services.AddHttpClient<IBaseService, BaseService>();
        //builder.Services.AddHttpClient<ICouponService, CouponService>();
    }

}
