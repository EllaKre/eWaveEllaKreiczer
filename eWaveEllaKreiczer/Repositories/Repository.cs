using eWaveEllaKreiczer.Interfaces;
using eWaveEllaKreiczer.Models;
using System.Xml;
using Newtonsoft.Json;

namespace eWaveEllaKreiczer.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly string _filePath = "Data//movies.json";
        private List<Movie> _movies;
        /// <summary>
        /// Creating the movies repository by loading the movies from the file when creating the class
        /// </summary>
        public MovieRepository()
        {   
            _movies = LoadMoviesFromFile();
        }


        /// <summary>
        /// Return movies list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Movie> GetAllMovies()
        {
            return _movies.OrderByDescending(m => m.Rating).Take(10).ToList();
        }


        /// <summary>
        /// Return movies by category
        /// </summary>
        /// <param name="category">The category filter that we need to find</param>
        /// <returns></returns>
        public IEnumerable<Movie> GetMoviesByCategory(string category)
        {
            return _movies.Where(m => m.Categories.Contains(category)).Take(10).ToList();
        }


        /// <summary>
        /// Add a new movie
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Movie AddMovie(Movie movie)
        {
            if (_movies.Any(m => m.Title.Equals(movie.Title, StringComparison.OrdinalIgnoreCase)))
                throw new Exception("Movie already exists!");

            _movies.Add(movie);
            _movies = _movies.OrderByDescending(m => m.Rating).Take(10).ToList(); // We will pass the list of movies after sorting them by rating.
            SaveChanges();

            return movie;
        }


        /// <summary>
        /// Deleting a movie by name
        /// </summary>
        /// <param name="title"></param>
        public void DeleteMovie(string title)
        {
            var movieToRemove = _movies.FirstOrDefault(m => m.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (movieToRemove != null)
            {
                _movies.Remove(movieToRemove);
                SaveChanges();
            }
        }


        /// <summary>
        /// Save to file JSON/XML
        /// </summary>
        public void SaveChanges()
        {
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(_movies, Newtonsoft.Json.Formatting.Indented));
        }


        /// <summary>
        /// Load the movies from the file
        /// </summary>
        /// <returns></returns>
        private List<Movie> LoadMoviesFromFile()
        {
            if (!File.Exists(_filePath)) return new List<Movie>();
            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Movie>>(json) ?? new List<Movie>();
        }
    }

}
