namespace Fit4Time.Services;

public static class MyServiceProvider
{
    public static TService GetService<TService>()
    {
        return Current.GetService<TService>();
    }

    private static IServiceProvider Current
        =>
#if WINDOWS10_0_17763_0_OR_GREATER
			MauiWinUIApplication.Current.Services;
#elif ANDROID
            MauiApplication.Current.Services;
#elif IOS || MACCATALYST
			MauiUIApplicationDelegate.Current.Services;
#else
			null;
#endif
}
