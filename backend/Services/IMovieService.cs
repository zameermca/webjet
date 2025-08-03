using System.Collections.Generic;
using System.Threading.Tasks;
using MoviePriceApp.Models;

namespace MoviePriceApp.Services;

public interface IMovieService
{
    Task<List<MovieSummary>> GetMoviesAsync();
    Task<List<PriceResult>> GetMoviePricesAsync(string title);
}
