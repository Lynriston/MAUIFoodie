using MAUIFoodie.Services;
using MAUIFoodie.ViewModels;
using MAUIFoodie.Views;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;

namespace MAUIFoodie
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Services.AddSingleton(AudioManager.Current);
            builder.Services.AddSingleton<DBServices>();
            builder.Services.AddSingleton<ThemeService>();
            builder.Services.AddSingleton<AudioServices>();

            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<SettingsViewModel>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddSingleton<LanguageServices>();
#endif

            return builder.Build();
        }
    }
}
