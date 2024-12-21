using System.Diagnostics;
using eWaveEllaKreiczer.Interfaces;
using eWaveEllaKreiczer.Models;
using eWaveEllaKreiczer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eWaveEllaKreiczer.Controllers
{
    
    public class MoviesController : Controller
    {
        private readonly IMovieRepository _repository;

        /// <summary>
        /// Creating the movies repository
        /// </summary>
        public MoviesController()
        {
            _repository = new MovieRepository();
        }


        /// <summary>
        /// The default page is "get all the movies" 
        /// </summary>
        /// <returns> Return movies list</returns>
        public IActionResult Index()
        {
            if (_repository == null)
            {
               // If the movie list is empty or null, return a page with an error message.
                return View("Error");
               
            }
            return View(_repository.GetAllMovies()); // We will pass the list of movies after sorting them by rating.
        }


        /// <summary>
        /// Return the error page - this is the default page from asp.net project
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Return movies 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllMovies()
        {
            return Ok(_repository.GetAllMovies()); // We will pass the list of movies after sorting them by rating.
        }

        /// <summary>
        /// Get all the movies according to the category
        /// </summary>
        /// <param name="category">The category filter that we need to find</param>
        /// <returns></returns>
        public IActionResult GetMoviesByCategory(string category)
        {
            return Ok(_repository.GetMoviesByCategory(category));
        }

        /// <summary>
        /// Add movie to the DB=movies.json file
        /// </summary>
        /// <param name="newMovie"> the movie to add</param>
        /// <returns></returns>
        public IActionResult AddMovie([FromBody] Movie newMovie)
        {
            try
            {
                var addedMovie = _repository.AddMovie(newMovie);
                return Ok(addedMovie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete the movie by movie title
        /// </summary>
        /// <param name="title">the movie title to delete</param>
        /// <returns></returns>
        public IActionResult DeleteMovie(string title)
        {
            _repository.DeleteMovie(title);
            return NoContent();
        }

    }
}
