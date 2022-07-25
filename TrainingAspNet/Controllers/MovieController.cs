using bosvcMovieService;
using mdlMovie;
using svcMovieService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrainingAspNet.Controllers
{
    public class MovieController : Controller
    {
        //private static List<Movie> movieList = new List<Movie>();
        //private static int id = 0;
        private readonly IMovieService _movieService;

        public MovieController()
        {
            //InitData();
            _movieService = new MovieService();
        }

        private void InitData()
        {
            //if (movieList.Count == 0)
            //{
            //    var movie1 = new Movie();
            //    movie1.Id = 1;
            //    movie1.Title = "Rambo 4";
            //    movie1.Actrees = "Silverster Stalone";
            //    movie1.Rating = 8;
            //    movie1.MovieYear = 2000;
            //    movieList.Add(movie1);

            //    var movie2 = new Movie()
            //    {
            //        Id = 2,
            //        Title = "Shark",
            //        Actrees = "James",
            //        Rating = 9
            //    };
            //    movieList.Add(movie2);

            //    movieList.Add(new Movie()
            //    {
            //        Id = 3,
            //        Title = "Batman",
            //        Actrees = "John",
            //        Rating = 8
            //    });

            //    movieList.Add(
            //        new Movie(4, "Robin", "Doe", 8.5)
            //    );

            //    id = 4;
            //}
        }

        public ActionResult Index()
        {
            var data = _movieService.GetAll();
            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Movie movie = new Movie();
            return View(movie);
        }

        [HttpPost]
        public ActionResult Create(FormCollection param)
        {
            Movie movie = new Movie();
            movie = BindData(movie, param);
            _movieService.Create(movie);

            return RedirectToAction("Index");
        }

        private Movie BindData(Movie movie, FormCollection param)
        {
            movie.Title = param["Title"];
            movie.Actrees = param["Actrees"];

            double rating = 0;
            if (double.TryParse(param["Rating"], out rating) == true)
            {
                movie.Rating = rating;
            }
            else
            {
                movie.Rating = 0;
            }

            return movie;
        }

        public ActionResult Details(int id)
        {
            var movie = _movieService.GetById(id);
            if(movie == null)
            {
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        public ActionResult Delete(int id)
        {
            _movieService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var movie = _movieService.GetById(id);
            if (movie == null)
            {
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection param, int id)
        {
            var movie = _movieService.GetById(id);
            if (movie == null)
            {
                return RedirectToAction("Index");
            }
            BindData(movie, param);
            _movieService.Update(movie, id);
            return RedirectToAction("Index");
        }
    }
}