namespace Fit4Time.Services;

public class SugarWodManager : ISugarWodManager
{
    private readonly ISugarWodApiService api;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="api">The ISugarWodAPIService singleton</param>
    public SugarWodManager(ISugarWodApiService api)
    {
        this.api = api;
    }
    
     /// <summary>
    ///     Gets a list of athletes by role, all, coaches, owners
    /// </summary>
    /// <param name="role">The athletes role</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A list of athletes on success, an error message otherwise</returns>
    public async Task<Result<Athlete>> GetAthletesAsync(string role, CancellationToken cancellationToken)
    {
        var result = await api.Get<Athlete>($"/v2/athletes?role={role}&page[limit]=50", cancellationToken);
        return result.IsSuccess ? Result.Ok(result.Value) : Result.Fail<Athlete>(result.Error);
    }

    /// <summary>
    ///     Gets the next list of athletes from pagination link
    /// </summary>
    /// <param name="page">The page url to the next list of athletes</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A list of athletes on success, an error message otherwise</returns>
    public async Task<Result<Athlete>> GetAthletesNextAsync(string page, CancellationToken cancellationToken)
    {
        // We need to remove the leading base url from the link because
        // our httpClient already has this assigned. We just need the pagination link.
        // From: https://api.sugarwod.com/v2/athletes?page%5Blimit%5D=50&page%5Bskip%5D=50
        // To:   /v2/athletes?page%5Blimit%5D=50&page%5Bskip%5D=50
        var link = ExtractNextPageUrl(page);
        if (link.IsFailure) return Result.Fail<Athlete>(link.Error);

        var result = await api.Get<Athlete>(link.Value, cancellationToken);
        return result.IsSuccess ? Result.Ok(result.Value) : Result.Fail<Athlete>(result.Error);
    }

    /// <summary>
    ///     Gets a list of workouts
    /// </summary>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A list of workouts on success, an error message otherwise</returns>
    public async Task<Result<Workout>> GetWorkoutsAsync(CancellationToken cancellationToken)
    {
        var result = await api.Get<Workout>("/v2/workouts", cancellationToken);
        return result.IsSuccess ? Result.Ok(result.Value) : Result.Fail<Workout>(result.Error);
    }

    /// <summary>
    ///     Extracts the pagination part of the full page link
    /// </summary>
    /// <param name="link">The full page url</param>
    /// <returns>Part path that represents the pagination link</returns>
    private static Result<string> ExtractNextPageUrl(string link)
    {
        // We need to remove the leading base url from the link because
        // our httpClient already has this assigned. We just need the pagination link.
        // From: https://api.sugarwod.com/v2/athletes?page%5Blimit%5D=50&page%5Bskip%5D=50
        // To:   /v2/athletes?page%5Blimit%5D=50&page%5Bskip%5D=50

        try
        {
            var index = link.LastIndexOf('/');
            if (index == -1) return Result.Fail<string>("Invalid link to next page");

            var pagination = link[index..];
            return Result.Ok(pagination);
        }
        catch
        {
            return Result.Fail<string>("Invalid link to next page");
        }
    }
}