using System.Collections.Generic;
using Model;

namespace Contracts
{
	public interface IVideoStore
	{
		Movie GetMovie(int id);

		IEnumerable<Movie> GetMovies();

		User GetUser(int id);

		IEnumerable<User> GetUsers();

		void AddRental(int userId, int movieId);

		void AddRentals(int userId, IEnumerable<int> movieIds);

		void DeleteRental(int rentalId);

		void DeleteRentals(IEnumerable<int> rentalIds);

		Rental GetRental(int id);

		IEnumerable<Rental> GetRentals();

		IEnumerable<RentedMovie> GetRentedMoviesByUser(int userId);

		IEnumerable<RentedMovie> GetRentedMoviesByMovie(int movieId);
	}
}
