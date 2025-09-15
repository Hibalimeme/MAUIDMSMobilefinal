using AutoMapper;
using CommunityToolkit.Maui;
using LocalizationResourceManager.Maui;
using MAUIDMSMobile.Mapper;
using MAUIDMSMobile.Mvvm.ViewModels;
using MAUIDMSMobile.Resx;
using MAUIDMSMobile.Services;
using MAUIDMSMobile.Services.Implementations;
using MAUIDMSMobile.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Mopups.Hosting;
using Mopups.Interfaces;
using Mopups.Services;
using Syncfusion.Maui.Toolkit.Hosting;
using YourApp.Services;

namespace MAUIDMSMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            var mapper = AutoMapperConfig.AutoMapperInit();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionToolkit()
                .ConfigureMopups()
                .UseLocalizationResourceManager(settings =>
                {
                    settings.AddResource(AppResources.ResourceManager);
                    //settings.RestoreLatestCulture(true);
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
                });

            // ✅ HttpClient with BaseAddress
            builder.Services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri("https://api.businesscentral.dynamics.com/v2.0/6db6dfc0-0dec-4ea8-8818-5d7d08b50ca4/Sales/ODataV4/"),
                Timeout = TimeSpan.FromSeconds(30)
            });

            // 💡 Register core services
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IVehicleService, VehicleService>();
            builder.Services.AddTransient<IServiceRendezVous, ServiceRendezVous>();
            builder.Services.AddTransient<IserviceDevis, ServiceDevis>();

            // 💡 Register your Methods service (Http helper)
            builder.Services.AddTransient<Methods>();

            // 💡 Global Mapper & Popup
            builder.Services.AddSingleton<IMapper>(mapper);
            builder.Services.AddSingleton<IPopupNavigation>(MopupService.Instance);

            return builder.Build();
        }
    }
}