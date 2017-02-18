using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Model;

namespace Service
{
	public class VideoStore : IVideoStore
	{
		public void AddRental(Rental rental)
		{
			using (var videoStore = new VideoStoreContext())
			{
				AddRental(videoStore, rental);

				videoStore.SaveChanges();
			}
		}

		public void AddRentals(IEnumerable<Rental> rentals)
		{
			using (var videoStore = new VideoStoreContext())
			{
				foreach (var item in rentals)
				{
					AddRental(videoStore, item);
				}

				videoStore.SaveChanges();
			}
		}

		public void DeleteRental(Rental rental)
		{
			using (var videoStore = new VideoStoreContext())
			{
				DeleteRental(videoStore, rental);
				videoStore.SaveChanges();
			}
		}

		public void DeleteRentals(IEnumerable<Rental> rentals)
		{
			using (var videoStore = new VideoStoreContext())
			{
				foreach (var item in rentals)
				{
					DeleteRental(videoStore, item);
				}

				videoStore.SaveChanges();
			}
		}

		public Rental GetRental(int id)
		{
			using (var videoStore = new VideoStoreContext())
			{
				var rental = videoStore.Rentals
					.Where(r => r.Id == id)
					.Include(r => r.Movie)
					.Include(r => r.User)
					.FirstOrDefault();

				return rental;
			}
		}

		public IEnumerable<Rental> GetRentals()
		{
			using (var videoStore = new VideoStoreContext())
			{
				return videoStore.Rentals
					.Include(r => r.Movie)
					.Include(r => r.User)
					.ToList();
			}
		}

		public IEnumerable<RentedMovie> GetRentedMoviesByUser(int userId)
		{
			using (var videoStore = new VideoStoreContext())
			{
				return videoStore.RentedMovies
					.Where(r => r.UserId == userId)
					.ToList();
			}
		}

		public IEnumerable<RentedMovie> GetRentedMoviesByMovie(int movieId)
		{
			using (var videoStore = new VideoStoreContext())
			{
				return videoStore.RentedMovies
					.Where(r => r.MovieId == movieId)
					.ToList();
			}
		}

		public Movie GetMovie(int id)
		{
			using (var videoStore = new VideoStoreContext())
			{
				return videoStore.Movies.Find(id);
			}
		}

		public IEnumerable<Movie> GetMovies()
		{
			using (var videoStore = new VideoStoreContext())
			{
				return videoStore.Movies.ToList();
			}
		}

		public User GetUser(int id)
		{
			using (var videoStore = new VideoStoreContext())
			{
				return videoStore.Users.Find(id);
			}
		}

		public IEnumerable<User> GetUsers()
		{
			using (var videoStore = new VideoStoreContext())
			{
				return videoStore.Users.ToList();
			}
		}

		private void AddRental(VideoStoreContext videoStore, Rental rental)
		{
			var rentedMovie = new RentedMovie
			{
				MovieId = rental.MovieId,
				UserId = rental.UserId,
			};

			--rental.Movie.NumOfCopies;
			videoStore.Movies.Attach(rental.Movie);
			videoStore.Entry(rental.Movie).Property(m => m.NumOfCopies).IsModified = true;

			rental.Movie = null;
			rental.User = null;
			videoStore.Rentals.Add(rental);

			videoStore.RentedMovies.Add(rentedMovie);
		}

		private void DeleteRental(VideoStoreContext videoStore, Rental rental)
		{
			++rental.Movie.NumOfCopies;

			videoStore.Movies.Attach(rental.Movie);
			videoStore.Entry(rental.Movie).Property(m => m.NumOfCopies).IsModified = true;

			rental.User = null;
			rental.Movie = null;	

			videoStore.Rentals.Attach(rental);
			videoStore.Rentals.Remove(rental);
		}
	}
}
