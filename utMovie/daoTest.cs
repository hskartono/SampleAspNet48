using daoInMemoryMovie;
using mdlMovie;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace utMovie
{
    [TestClass]
    public class daoTest
    {
        private void AssertValues(Movie expected, Movie actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Actrees, actual.Actrees);
            Assert.AreEqual(expected.MovieYear, actual.MovieYear);
            Assert.AreEqual(expected.Rating, actual.Rating);
            Assert.AreEqual(expected.Title, actual.Title);
        }

        private Movie InitData()
        {
            var data = new Movie()
            {
                Actrees = "Actrees",
                MovieYear = 1990,
                Rating = 3,
                Title = "Title"
            };
            return data;
        }

        [TestMethod]
        public void InsertTest()
        {
            var expected = InitData();

            IMovieDao dao = new InMemoryMovieDao();
            var result = dao.Insert(expected);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id > 0);

            var actual = dao.GetById(result.Id);
            Assert.IsNotNull(actual);
            AssertValues(expected, actual);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var expected = new Movie()
            {
                Actrees = "ActreesEdited",
                MovieYear = 2000,
                Rating = 5,
                Title = "TitleEdited"
            };

            var init = InitData();

            IMovieDao dao = new InMemoryMovieDao();
            var actual = dao.Insert(init);

            actual.Actrees = expected.Actrees;
            actual.MovieYear = expected.MovieYear;
            actual.Rating = expected.Rating;
            actual.Title = expected.Title;

            var result = dao.Update(actual, actual.Id);
            Assert.IsTrue(result);

            actual = dao.GetById(actual.Id);
            expected.Id = actual.Id;
            AssertValues(expected, actual);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var init = InitData();
            IMovieDao dao = new InMemoryMovieDao();
            var result = dao.Insert(init);
            Assert.IsNotNull(result);

            var insertedCount = dao.GetCount();
            Assert.IsTrue(insertedCount > 0);

            var delResult = dao.Delete(result.Id);
            Assert.IsTrue(delResult);

            var actual = dao.GetCount();
            Assert.AreEqual(insertedCount - 1, actual);
        }
    }
}
