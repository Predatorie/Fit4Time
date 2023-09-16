using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace Fit4Time.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    #region Private Fields

    /// <summary>
    /// Backing field for the ISugarWodManager property
    /// </summary>
    private readonly ISugarWodManager sugarWodManager;

    /// <summary>
    /// Backing field for the CancellationTokenSource property
    /// </summary>
    private CancellationTokenSource cancellationTokenSource;

    /// <summary>
    /// Backing field for the CancellationToken property
    /// </summary>
    private CancellationToken cancellationToken;

    /// <summary>
    /// Backing field for the page link
    /// </summary>
    private string link;

    /// <summary>
    /// Backing field for the has next page field
    /// </summary>
    [ObservableProperty]
    private bool hasNextPage;

    /// <summary>
    /// Backing field for the isBusy property
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool isBusy;

    /// <summary>
    /// Backing field for the coaches property
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<Datum> coaches = new();

    /// <summary>
    /// Backing field for the wods property
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<WodDatum> workouts = new();

    /// <summary>
    /// Backing field for the results count property
    /// </summary>
    [ObservableProperty]
    private int resultCount;

    #endregion

    /// <summary>
    /// Defines the MainPageViewModel class.
    /// </summary>
    /// <param name="manager">The ISugarWodManager singleton</param>
    public MainPageViewModel(ISugarWodManager manager)
    {
        // wire up our services
        sugarWodManager = manager;
        
    }

    /// <summary>
    /// Gets the IsNotBusy property
    /// </summary>
    public bool IsNotBusy => !this.IsBusy;

    /// <summary>
    /// Gets a list of athletes
    /// </summary>
    /// <returns>A task</returns>
    [RelayCommand]
    public async Task GetAthletesAsync()
    {
        try
        {
            this.IsBusy = true;

            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            this.ResultCount = 0;

            var result = await sugarWodManager.GetAthletesAsync("athletes", cancellationToken);
            if (result.IsSuccess)
            {
                var athletes = result.Value?.Data;

                if (athletes == null)
                {
                    // TODO: Log error
                    return;
                }

                foreach (var athlete in athletes)
                {
                    this.Coaches.Add(athlete);
                }

                this.ResultCount = this.Coaches.Count;

                link = result.Value.Links?.Next?.ToString();
                this.HasNextPage = !string.IsNullOrWhiteSpace(this.link);
            }
        }
        catch (Exception ex)
        {
            // TODO: Log exception
        }
        finally
        {
            this.IsBusy = false;
            cancellationTokenSource?.Dispose();
        }
    }

    /// <summary>
    /// Gets a list of wods
    /// </summary>
    /// <returns>A task</returns>
    [RelayCommand]
    public async Task GetCoachesAsync()
    {
        try
        {
            this.IsBusy = true;

            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            this.ResultCount = 0;

            var result = await sugarWodManager.GetAthletesAsync("wods", cancellationToken);
            if (result.IsSuccess)
            {
                var data = result.Value?.Data;

                if (data == null)
                {
                    // TODO: Log error
                    return;
                }

                foreach (var coach in data)
                {
                    this.Coaches.Add(coach);
                }

                this.ResultCount = this.Coaches.Count;

                link = result.Value.Links?.Next?.ToString();
                this.HasNextPage = !string.IsNullOrWhiteSpace(this.link);
            }
        }
        catch (Exception ex)
        {
            // TODO: Log exception
        }
        finally
        {
            this.IsBusy = false;
            cancellationTokenSource?.Dispose();
        }
    }

    /// <summary>
    /// Gets a list of owners
    /// </summary>
    /// <returns>A task</returns>
    [RelayCommand]
    public async Task GetOwnersAsync()
    {
        try
        {
            this.IsBusy = true;

            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            this.ResultCount = 0;

            var result = await sugarWodManager.GetAthletesAsync("wods", cancellationToken);
            if (result.IsSuccess)
            {
                var owners = result.Value?.Data;

                if (owners == null)
                {
                    // TODO: Log error
                    return;
                }

                foreach (var owner in owners)
                {
                    this.Coaches.Add(owner);
                }

                this.ResultCount = this.Coaches.Count;

                link = result.Value.Links?.Next?.ToString();
                this.HasNextPage = !string.IsNullOrWhiteSpace(this.link);
            }
        }
        catch (Exception ex)
        {
            // TODO: Log exception
        }
        finally
        {
            this.IsBusy = false;
            cancellationTokenSource?.Dispose();
        }
    }

    /// <summary>
    /// Gets a list of wod's
    /// </summary>
    /// <returns>A task</returns>
    [RelayCommand]
    public async Task GetWorkoutAsync()
    {
        try
        {
            this.IsBusy = true;

            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            this.ResultCount = 0;

            var result = await sugarWodManager.GetWorkoutsAsync(cancellationToken);
            if (result.IsSuccess)
            {
                var wods = result.Value?.Data;

                if (wods == null)
                {
                    // TODO: Log error
                    return;
                }

                foreach (var wod in wods)
                {
                    this.Workouts.Add(wod);
                }

                this.ResultCount = this.Workouts.Count;

                link = result.Value.Links?.Next?.ToString();
                this.HasNextPage = !string.IsNullOrWhiteSpace(this.link);
            }
            else
            {
               var error = result.Error;
            }
        }
        catch (Exception ex)
        {
            // TODO: Log exception
        }
        finally
        {
            this.IsBusy = false;
            cancellationTokenSource?.Dispose();
        }
    }
}


