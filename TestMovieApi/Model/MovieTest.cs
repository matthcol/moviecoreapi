using System;
using MovieApiRest.Model;
using Xunit;

namespace TestMovieApi
{
    public class MovieTest
    {
        [Fact]
        public void TestConstructor()
        {
            var title = "Nobody";
            short year = 2021;
            var movie = new Movie
            {
                Title = title,
                Year = year
            };
            Assert.NotNull(movie.Title);
            Assert.Equal(title, movie.Title);
            Assert.Equal(year, movie.Year);
            Assert.Null(movie.Id);
        }

        [Theory]
        [InlineData(1900)]
        [InlineData(1950)]
        [InlineData(2022)]
        public void TestConstructor_Color(short year) 
        {
            var movie = new Movie
            {
                Title = "Star Wars",
                Year = year
            };
            Assert.Equal(movie.Year, year);
        }

    }
}
