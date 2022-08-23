namespace SmartFamily.Helpers;

public static class NativeDIResolver
{
    public static TService GetService<TService>()
        => Current.GetService<TService>();

    private static IServiceProvider Current
        =>
//#if ANDROID
        MauiApplication.Current.Services;
//#elif IOS || MACCATALYST
//        MauiUIApplicationDelegate.Current.Services;
//#elif WINDOWS
        //MauiWinUIApplication.Current.Services;
//#endif
}