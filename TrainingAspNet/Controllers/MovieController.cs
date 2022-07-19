using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingAspNet.Models;

namespace TrainingAspNet.Controllers
{
    public class MovieController : Controller
    {
        private static List<Movie> movieList = new List<Movie>();
        private static int id = 0;

        public MovieController()
        {
            InitData();
        }

        private void InitData()
        {
            if (movieList.Count == 0)
            {
                var movie1 = new Movie();
                movie1.Id = 1;
                movie1.Title = "Rambo 4";
                movie1.Actrees = "Silverster Stalone";
                movie1.Rating = 8;
                movie1.MovieYear = 2000;
                movieList.Add(movie1);

                var movie2 = new Movie()
                {
                    Id = 2,
                    Title = "Shark",
                    Actrees = "James",
                    Rating = 9
                };
                movieList.Add(movie2);

                movieList.Add(new Movie()
                {
                    Id = 3,
                    Title = "Batman",
                    Actrees = "John",
                    Rating = 8
                });

                movieList.Add(
                    new Movie(4, "Robin", "Doe", 8.5)
                );

                id = 4;
            }
        }

        public ActionResult Index()
        {
            return View(movieList);
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
            id++;
            Movie movie = new Movie();
            movie.Id = id;
            movie.Title = param["Title"];
            movie.Actrees = param["Actrees"];

            double rating = 0;
            if (double.TryParse(param["Rating"], out rating) == true)
            {
                movie.Rating = rating;
            } else
            {
                movie.Rating = 0;
            }

            movieList.Add(movie);

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            if(movieList.Where(e=>e.Id == id).Any() == false)
            {
                return RedirectToAction("Index");
            }

            var data = movieList.Where(e => e.Id == id).First();
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            if (movieList.Where(e => e.Id == id).Any() == false)
            {
                return RedirectToAction("Index");
            }

            var data = movieList.Where(e => e.Id == id).First();
            movieList.Remove(data);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            if (movieList.Where(e => e.Id == id).Any() == false)
            {
                return RedirectToAction("Index");
            }

            var data = movieList.Where(e => e.Id == id).First();
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection param, int id)
        {
            if (movieList.Where(e => e.Id == id).Any() == false)
            {
                return RedirectToAction("Index");
            }

            var data = movieList.Where(e => e.Id == id).First();

            data.Title = param["Title"];
            data.Actrees = param["Actrees"];

            double rating = 0;
            if (double.TryParse(param["Rating"], out rating) == true)
            {
                data.Rating = rating;
            }
            else
            {
                data.Rating = 0;
            }

            return RedirectToAction("Index");
        }
    }
}