using System.Collections.Generic;
using System.ServiceModel;
using Model;
using Contracts;

namespace ServiceProvider
{
	public class VideoStoreProvider : ClientBase<Service.IVideoStore>, IVideoStore
	{
		public Movie GetMovie(int id)
		{
			return Channel.GetMovie(id);
		}

		public IEnumerable<Movie> GetMovies()
		{
			return Channel.GetMovies();
		}

		public User GetUser(int id)
		{
			return Channel.GetUser(id);
		}

		public IEnumerable<User> GetUsers()
		{
			return Channel.GetUsers();
		}

		public void AddRental(Rental rental)
		{
			Channel.AddRental(rental);
		}

		public void AddRentals(IEnumerable<Rental> rentals)
		{
			Channel.AddRentals(rentals);
		}

		public void DeleteRental(Rental rental)
		{
			Channel.DeleteRental(rental);
		}

		public void DeleteRentals(IEnumerable<Rental> rentals)
		{
			Channel.DeleteRentals(rentals);
		}

		public Rental GetRental(int id)
		{
			return Channel.GetRental(id);
		}

		public IEnumerable<Rental> GetRentals()
		{
			return Channel.GetRentals();
		}

		public IEnumerable<RentedMovie> GetRentedMoviesByUser(int userId)
		{
			return Channel.GetRentedMoviesByUser(userId);
		}

		public IEnumerable<RentedMovie> GetRentedMoviesByMovie(int movieId)
		{
			return Channel.GetRentedMoviesByMovie(movieId);
		}
	}
}
