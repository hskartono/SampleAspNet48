using daoInMemoryMovie;
using mdlMovie;
using svcMovieService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bosvcMovieService
{
    public class MovieService : IMovieService
    {
        private readonly IMovieDao _movieDao;

        public MovieService()
        {
            _movieDao = new InMemoryMovieDao();
        }
        public Movie Create(Movie newMovie)
        {
            return _movieDao.Insert(newMovie);
        }

        public bool Delete(int id)
        {
            if (_movieDao.GetById(id) == null) return false;
            return _movieDao.Delete(id);
        }

        public List<Movie> GetAll()
        {
            return _movieDao.GetAll();
        }

        public Movie GetById(int id)
        {
            return _movieDao.GetById(id);
        }

        public long GetCount()
        {
            return _movieDao.GetCount();
        }

        public bool Update(Movie movie, int id)
        {
            if (_movieDao.GetById(id) == null) return false;
            return _movieDao.Update(movie, id);
        }
    }
}
