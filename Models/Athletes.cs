namespace Fit4Time.Models;

public partial class Athlete
{
    [JsonProperty("data")]
    public Datum[] Data { get; set; }

    [JsonProperty("links")]
    public AthleteLinks Links { get; set; }
}

public partial class Datum
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("attributes")]
    public Attributes Attributes { get; set; }

    [JsonProperty("links")]
    public DatumLinks Links { get; set; }
}

public partial class Attributes
{
    [JsonProperty("created_at")]
    public string CreatedAt { get; set; }

    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("gender")]
    public string Gender { get; set; }

    [JsonProperty("profile_image_url")]
    public Uri ProfileImageUrl { get; set; }

    [JsonProperty("last_login")]
    public DateTimeOffset LastLogin { get; set; }
}

public partial class DatumLinks
{
    [JsonProperty("ui_athlete")]
    public Uri UiAthlete { get; set; }

    [JsonProperty("remove_from_affiliate")]
    public Uri RemoveFromAffiliate { get; set; }
}

public partial class AthleteLinks
{
    [JsonProperty("self")]
    public Uri Self { get; set; }

    [JsonProperty("next")]
    public Uri Next { get; set; }
} 
