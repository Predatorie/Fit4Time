namespace Fit4Time.Services;

/// <summary>
/// Describes the ISugarWodManager interface.
/// </summary>
public interface ISugarWodManager
{
    /// <summary>
    /// Gets a list of athlete types, coach, owner, etc.
    /// </summary>
    /// <param name="role">The role, for example, coaches</param>
    /// <param name="cancellationToken">The cancellation token instance</param>
    /// <returns>A list of athletes on success, an error of failure.</returns>
    Task<Result<Athlete>> GetAthletesAsync(string role, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a list of athletes.
    /// </summary>
    /// <param name="page">Link to the next page of results</param>
    /// <param name="cancellationToken">The cancellation token instance</param>
    /// <returns>A list of athletes on success, an error of failure.</returns>
    Task<Result<Athlete>> GetAthletesNextAsync(string page, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a list of workouts.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token instance</param>
    /// <returns>A list of workouts on success, an error of failure.</returns>
    Task<Result<Workout>> GetWorkoutsAsync(CancellationToken cancellationToken);
}


