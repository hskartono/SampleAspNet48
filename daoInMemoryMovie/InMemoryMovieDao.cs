using mdlMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daoInMemoryMovie
{
    public class InMemoryMovieDao : IMovieDao
    {

        private static List<Movie> movieList = new List<Movie>();
        private static int id = 0;

        public InMemoryMovieDao()
        {

        }

        public bool Delete(int id)
        {
            if (!movieList.Any(e => e.Id == id))
                return false;

            var data = movieList.Where(e => e.Id == id).First();
            movieList.Remove(data);
            return true;
        }

        public List<Movie> GetAll()
        {
            return movieList;
        }

        public Movie GetById(int id)
        {
            if (!movieList.Any(e => e.Id == id))
                return null;

            return movieList.Where(e => e.Id == id).First();
        }

        public long GetCount()
        {
            return movieList.Count;
        }

        public Movie Insert(Movie newMovie)
        {
            id++;
            newMovie.Id = id;
            movieList.Add(newMovie);
            return newMovie;
        }

        public bool Update(Movie movie, int id)
        {
            var data = movieList.Where(e => e.Id == id).First();
            if (data == null) return false;
            data.MovieYear = movie.MovieYear;
            data.Actrees = movie.Actrees;
            data.Rating = movie.Rating;
            data.Title = movie.Title;
            return true;
        }
    }
}
