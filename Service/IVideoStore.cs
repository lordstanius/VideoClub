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
		void AddRental(int userId, int movieId);

		[OperationContract]
		void AddRentals(int userId, IEnumerable<int> movieIds);

		[OperationContract]
		void DeleteRental(int rentalId);

		[OperationContract]
		void DeleteRentals(IEnumerable<int> rentalIds);

		[OperationContract]
		Rental GetRental(int id);

		[OperationContract]
		IEnumerable<Rental> GetRentals();
	}
}
