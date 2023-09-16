namespace Fit4Time.Models;

public partial class Workout
{
    [JsonProperty("data")]
    public WodDatum[] Data { get; set; }

    [JsonProperty("links")]
    public WorkoutLinks Links { get; set; }
}

public partial class WodDatum
{
    [JsonProperty("type")]
    public string WodType { get; set; }

    [JsonProperty("id")]
    public string WodId { get; set; }

    [JsonProperty("attributes")]
    public WodAttributes WodAttributes { get; set; }

    [JsonProperty("links")]
    public DatumLinks WodLinks { get; set; }
}

public partial class WodAttributes
{
    [JsonProperty("created_at")]
    public string CreatedAtTime { get; set; }

    [JsonProperty("scheduled_date_int")]
    public long ScheduledDateInt { get; set; }

    [JsonProperty("scheduled_date")]
    public string ScheduledDate { get; set; }

    [JsonProperty("track")]
    public Track Track { get; set; }

    [JsonProperty("display_order")]
    public double DisplayOrder { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score_type")]
    public string ScoreType { get; set; }

    [JsonProperty("publish_at")]
    public string PublishAt { get; set; }

    [JsonProperty("is_published")]
    public bool IsPublished { get; set; }

    [JsonProperty("movement_ids")]
    public string[] MovementIds { get; set; }
}

public partial class Track
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("attributes_for")]
    public WodAttributesFor AttributesFor { get; set; }
}

public partial class WodAttributesFor
{
    [JsonProperty("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
}

public partial class DatumLinks
{
    [JsonProperty("ui_results")]
    public Uri UiResults { get; set; }
}

public partial class WorkoutLinks
{
    [JsonProperty("self")]
    public Uri Self { get; set; }

    [JsonProperty("ui_calendar")]
    public Uri UiCalendar { get; set; }

    [JsonProperty("next")]
    public Uri Next { get; set; }
}


