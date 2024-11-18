using ApplicationCore.Interfaces;
using ApplicationCore.MailKit;
using ApplicationCore.Repositories;
using ApplicationCore.Services;
using CloudinaryDotNet;
using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Web.Configuration
{
    public static class ServiceConfig
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<CloudinaryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPriceService, PriceService>();
            services.AddScoped<ICMSContentService, CMSContentService>();
            services.AddScoped<IKoiOrderService, KoiOrderService>();
            services.AddScoped<IKoiOrderService, KoiOrderService>();
            services.AddScoped<IOrderDocumentService, OrderDocumentService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IEmailService, ApplicationCore.MailKit.EmailService>();
        }
    }

}
