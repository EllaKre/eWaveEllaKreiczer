using eWaveEllaKreiczer.Models;
using eWaveEllaKreiczer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

[Route("api/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    /// <summary>
    ///Return the list of movies after sorting them by rating.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetMovies()
    {
        var movieRepository = new MovieRepository(); // Creating an object
        var movies = movieRepository.GetAllMovies(); 
        return Ok(movies);
    }

    /// <summary>
    /// Add movie to the DB=movies.json file
    /// </summary>
    /// <param name="newMovie"> the movie to add</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult AddMovie([FromBody] Movie newMovie)
    {

        var movieRepository = new MovieRepository(); // Creating an object
        movieRepository.AddMovie(newMovie);
        return Ok(new { Message = "Movie added successfully", Movie = newMovie });
    }

    /// <summary>
    /// Get all the movies according to the category
    /// </summary>
    /// <param name="category">The category filter that we need to find</param>
    /// <returns></returns>
    [HttpGet("category/{category}")]
    public IActionResult GetMoviesByCategory(string category)
    {
        var movieRepository = new MovieRepository(); // Creating an object
        return Ok(movieRepository.GetMoviesByCategory(category));
    }


    //[HttpDelete("{title}")]
    //public IActionResult DeleteMovie(string title)
    //{
    //    var movieRepository = new MovieRepository(); // Creating an object
    //    movieRepository.DeleteMovie(title);
    //    return NoContent();
    //}
}
