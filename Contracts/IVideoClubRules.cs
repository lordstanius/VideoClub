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

	}
}
