using System.Collections.Generic;
using System.ServiceModel;
using Model;

namespace Service
{
	[ServiceContract]
	public interface IVideoStore
	{
		[OperationContract]
		Movie GetMovie(int id);

		[OperationContract]
		IEnumerable<Movie> GetMovies();

		[OperationContract]
		User GetUser(int id);

		[OperationContract]
		IEnumerable<User> GetUsers();

		[OperationContract]
		void AddRental(Rental rental);

		[OperationContract]
		void AddRentals(IEnumerable<Rental> rentals);

		[OperationContract]
		void DeleteRental(Rental rental);

		[OperationContract]
		void DeleteRentals(IEnumerable<Rental> rentals);

		[OperationContract]
		Rental GetRental(int id);

		[OperationContract]
		IEnumerable<Rental> GetRentals();

		[OperationContract]
		IEnumerable<RentedMovie> GetRentedMoviesByUser(int userId);

		[OperationContract]
		IEnumerable<RentedMovie> GetRentedMoviesByMovie(int movieId);
	}
}
