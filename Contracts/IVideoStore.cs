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

		void AddRental(Rental rental);

		void AddRentals(IEnumerable<Rental> rentals);

		void DeleteRental(Rental rental);

		void DeleteRentals(IEnumerable<Rental> rentals);

		Rental GetRental(int id);

		IEnumerable<Rental> GetRentals();

		IEnumerable<RentedMovie> GetRentedMoviesByUser(int userId);

		IEnumerable<RentedMovie> GetRentedMoviesByMovie(int movieId);
	}
}
