using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MoviePriceApp.Models;
using MoviePriceApp.Constants;

namespace MoviePriceApp.Services;

public class MovieService : IMovieService
{
    private readonly HttpClient _http;
    private readonly MovieApiOptions _options;

    public MovieService(IHttpClientFactory factory, IOptions<MovieApiOptions> options)
    {
        _http = factory.CreateClient("WebjetClient");
        _options = options.Value;
        _http.DefaultRequestHeaders.Add("x-access-token", _options.AccessToken);
    }

    public async Task<List<MovieSummary>> GetMoviesAsync()
    {
        var cinemaTask = GetList(_options.Providers.CinemaWorld);
        var filmTask = GetList(_options.Providers.FilmWorld);

        await Task.WhenAll(cinemaTask, filmTask);

        var cw = cinemaTask.Result;
        var fw = filmTask.Result;

        var map = new Dictionary<string, MovieSummary>();

        foreach (var m in cw)
        {
            if (!map.ContainsKey(m.Title))
                map[m.Title] = new MovieSummary { Title = m.Title };

            map[m.Title].CinemaWorldId = m.ID;
        }

        foreach (var m in fw)
        {
            if (!map.ContainsKey(m.Title))
                map[m.Title] = new MovieSummary { Title = m.Title };

            map[m.Title].FilmWorldId = m.ID;
        }

        return map.Values.ToList();
    }

    public async Task<List<PriceResult>> GetMoviePricesAsync(string title)
    {
        var list = await GetMoviesAsync();
        var item = list.FirstOrDefault(x => x.Title.Equals(title, System.StringComparison.OrdinalIgnoreCase));
        var prices = new List<PriceResult>();

        var tasks = new List<Task<PriceResult?>>();

        if (!string.IsNullOrEmpty(item?.CinemaWorldId))
        {
            tasks.Add(GetPrice(_options.Providers.CinemaWorld, item.CinemaWorldId, ProviderLabels.CinemaWorld));
        }

        if (!string.IsNullOrEmpty(item?.FilmWorldId))
        {
            tasks.Add(GetPrice(_options.Providers.FilmWorld, item.FilmWorldId, ProviderLabels.FilmWorld));
        }

        var results = await Task.WhenAll(tasks);
        prices.AddRange(results.Where(r => r != null)!);

        return prices;
    }

    private async Task<List<ApiMovie>> GetList(string provider)
    {
        try
        {
            var res = await _http.GetFromJsonAsync<MovieListResponse>($"{_options.BaseUrl}/{provider}/movies");
            return res?.Movies ?? new();
        }
        catch
        {
            return new();
        }
    }

    private async Task<PriceResult?> GetPrice(string provider, string id, string label)
    {
        try
        {
            var res = await _http.GetFromJsonAsync<MovieDetail>($"{_options.BaseUrl}/{provider}/movie/{id}");
            if (res == null) return null;

            return new PriceResult
            {
                Provider = label,
                Price = res.Price
            };
        }
        catch
        {
            return null;
        }
    }
}
