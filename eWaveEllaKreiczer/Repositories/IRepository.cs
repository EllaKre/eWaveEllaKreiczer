using eWaveEllaKreiczer.Models;

namespace eWaveEllaKreiczer.Interfaces
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAllMovies(); // Return all movies
        IEnumerable<Movie> GetMoviesByCategory(string category); //Return movies by category
        Movie AddMovie(Movie movie); // Add a new movie
        void DeleteMovie(string title); // Deleting a movie by name
        void SaveChanges(); // Save to file JSON/XML
    }

}
