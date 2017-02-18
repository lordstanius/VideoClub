namespace Contracts
{
	public interface IVideoClubRules
	{
		/// <summary>
		/// Maximum allowed number of movies that can be rented by single user.
		/// </summary>
		int MovieNumberLimit { get; }

		/// <summary>
		/// Allowes user to rent two or more copies of the same movie.
		/// </summary>
		bool AllowMultipleCopies { get; }

		/// <summary>
		/// Allowes user to rent a new movie(s) without return of all previously rented.
		/// </summary>
		bool AllowMultipleRentals { get; }

		/// <summary>
		/// Returns how many days user is allowed to keep rented movies.
		/// </summary>
		/// <param name="numOfMovies">Number of rented movies.</param>
		int DuePeriod(int numOfMovies);

		/// <summary>
		/// User is not allowed to rent again movie(s) he had been rented before.
		/// </summary>
		bool DoNotAllowRentalOfPreviouslyRentedMovie { get; }
	}
}
