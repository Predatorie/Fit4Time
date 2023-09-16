namespace Fit4Time;

public static class MauiProgram
{
    #region Private Fields

    /// <summary>
    ///     429 Too Many Requests
    /// </summary>
    private const int TooManyRequests = 429;

    /// <summary>
    ///     Max allowed retry requests
    /// </summary>
    private const int MaxRetryAttempts = 4;

    /// <summary>
    ///     Max number of attempts before breaking
    /// </summary>
    private const int MaxAttemptsBeforeBreaking = 2;

    /// <summary>
    ///     List of retry codes for a give http static code
    /// </summary>
    private static readonly HttpStatusCode[] HttpStatusCodesWorthRetrying =
    {
        HttpStatusCode.RequestTimeout, // 408
        HttpStatusCode.InternalServerError, // 500
        HttpStatusCode.BadGateway, // 502
        HttpStatusCode.ServiceUnavailable, // 503
        HttpStatusCode.GatewayTimeout // 504
    };

    #endregion
    
    public static MauiApp CreateMauiApp()
    {
        var retryPolicy = Policy
            .HandleResult<HttpResponseMessage>(r => (int)r.StatusCode == TooManyRequests)
            .OrResult(r => HttpStatusCodesWorthRetrying.Contains(r.StatusCode))
            .WaitAndRetryAsync(MaxRetryAttempts, retries => TimeSpan.FromMilliseconds(0.1 * Math.Pow(2, retries)));

        var circuitBreakerPolicy = Policy
            .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
            .CircuitBreakerAsync(MaxAttemptsBeforeBreaking, TimeSpan.FromMinutes(1));

        var httpClientTimeoutPolicy = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(1, retries => TimeSpan.FromMinutes(retries),
            (e, t) =>
            {
                Debug.Print(e.Message);
            });
        
        var retryWithTimeoutWithCircuitBreakerPolicy = circuitBreakerPolicy
            .WrapAsync(retryPolicy)
            .WrapAsync(httpClientTimeoutPolicy);
        
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            // Initialize the .NET MAUI Community Toolkit by adding the below line of code
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        // register our http client + auth
        builder.Services.AddHttpClient("SugarWOD", client =>
        {
            client.BaseAddress = new Uri("https://api.sugarwod.com");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"{Auth.Key}");
        }).AddPolicyHandler(retryWithTimeoutWithCircuitBreakerPolicy);


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
