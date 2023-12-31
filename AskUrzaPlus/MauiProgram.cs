﻿using AskUrzaPlus.ViewModels;
using AskUrzaPlus.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Storage;

namespace AskUrzaPlus
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
    		builder.Logging.AddDebug();
#endif

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "urza.db3");
            builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<UrzaRepository>(s, dbPath));

            builder.Services.AddSingleton<AskUrzaPage>();
            builder.Services.AddSingleton<AskUrzaViewModel>();

            return builder.Build();
        }
    }
}
