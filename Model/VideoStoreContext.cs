using System.Data.Entity;

namespace Model
{
	public class VideoStoreContext : DbContext
	{
		public VideoStoreContext() :
			base("VideoStore")
		{
		}

		public DbSet<Movie> Movies { get; set; }

		public DbSet<User> Users { get; set; }

		public DbSet<Rental> Rentals { get; set; }

		public DbSet<RentedMovie> RentedMovies { get; set; }
	}
}
