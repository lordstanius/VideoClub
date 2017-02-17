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

		public void AddRental(int userId, int movieId)
		{
			Channel.AddRental(userId, movieId);
		}

		public void AddRentals(int userId, int[] movieIds)
		{
			Channel.AddRentals(userId, movieIds);
		}

		public void DeleteRental(int rentalId)
		{
			Channel.DeleteRental(rentalId);
		}

		public void DeleteRentals(int[] rentalIds)
		{
			Channel.DeleteRentals(rentalIds);
		}

		public Rental GetRental(int id)
		{
			return Channel.GetRental(id);
		}

		public IEnumerable<Rental> GetRentals()
		{
			return Channel.GetRentals();
		}

		public void AddRentals(int userId, IEnumerable<int> movieIds)
		{
			Channel.AddRentals(userId, movieIds);
		}

		public void DeleteRentals(IEnumerable<int> rentalIds)
		{
			Channel.DeleteRentals(rentalIds);
		}
	}
}
