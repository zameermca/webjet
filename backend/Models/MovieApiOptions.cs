namespace MoviePriceApp.Models;

public class MovieApiOptions
{
    public string BaseUrl { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public Providers Providers { get; set; } = new();
}

public class Providers
{
    public string CinemaWorld { get; set; } = string.Empty;
    public string FilmWorld { get; set; } = string.Empty;
}
