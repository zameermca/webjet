using Microsoft.AspNetCore.Mvc;
using MoviePriceApp.Services;
using MoviePriceApp.Models;
using System.Threading.Tasks;

namespace MoviePriceApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMovies()
    {
        var result = await _movieService.GetMoviesAsync();
        return Ok(result);
    }

    [HttpGet("{title}")]
    public async Task<IActionResult> Compare(string title)
    {
        var result = await _movieService.GetMoviePricesAsync(title);
        return Ok(result);
    }
}
