using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SmartFamily.Services;
using SmartFamily.ViewModels;
using SmartFamily.Views;

using System.Reflection;

namespace SmartFamily.Extensions;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<IMessageWriter, LoggingMessageWriter>();

        mauiAppBuilder.Services.AddTransient<IAnalytics, AnalyticsDebug>();
        mauiAppBuilder.Services.AddTransient<IAnalytics, AnalyticsAppCenter>();

        mauiAppBuilder.Services.AddSingleton<IService, Service>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterPages(this MauiAppBuilder mauiAppBuilder)
    {
        return mauiAppBuilder
            .AddPage<MainPage, MainPageViewModel>()
            .AddPage<NewDemoPage, NewDemoPageViewModel>()
            .AddPage<AppShell>();
    }

    public static MauiAppBuilder AddPage<TPage, TViewModel>(this MauiAppBuilder mauiAppBuilder) where TPage : Page where TViewModel : ObservableObject
    {
        mauiAppBuilder.Services.AddTransient<TPage>();
        mauiAppBuilder.Services.AddTransient<TViewModel>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder AddPage<TPage>(this MauiAppBuilder mauiAppBuilder) where TPage : Page
    {
        mauiAppBuilder.Services.AddTransient<TPage>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder EnableLogging(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddLogging(configure =>
        {
            // From Microsoft.Extensions.Logging.Debug
            configure.AddDebug();

            // From Microsoft.Extensions.Logging.Console
            configure.AddConsole();

            // Add custom logger
            configure.AddCustomLogging();
        });

        return mauiAppBuilder;
    }

    public static MauiAppBuilder AddAppConfiguration(this MauiAppBuilder mauiAppBuilder)
    {
        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MauiProgram)).Assembly;
        var stream = assembly.GetManifestResourceStream("SmartFamily.appsettings.json");

        // From Microsoft.Extensions.Configuration.Json
        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .AddPreferences(new List<string> { "PreferenceDemo", "PreferenceDemoInt" })
            .AddInMemoryCollection(new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("InMemoryKey1", "Value 1") })
            .Build();

        mauiAppBuilder.Configuration.AddConfiguration(config);

        return mauiAppBuilder;
    }
}