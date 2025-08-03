using System.Collections.Generic;

namespace MoviePriceApp.Models;

public class MovieListResponse
{
    public List<ApiMovie> Movies { get; set; } = new();
}

public class ApiMovie
{
    public string ID { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}
