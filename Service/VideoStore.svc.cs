using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Model;

namespace Service
{
	public class VideoStore : IVideoStore
	{
		public void AddRental(int userId, int movieId)
		{
			using (var videoStore = new VideoStoreContext())
			{
				AddRental(videoStore, userId, movieId);
				
				videoStore.SaveChanges();
			}
		}

		public void AddRentals(int userId, IEnumerable<int> movieIds)
		{
			using (var videoStore = new VideoStoreContext())
			{
				foreach (int movieId in movieIds)
				{
					AddRental(videoStore, userId, movieId);
				}

				videoStore.SaveChanges();
			}
		}

		public void DeleteRental(int id)
		{
			using (var videoStore = new VideoStoreContext())
			{
				DeleteRental(videoStore, id);
				videoStore.SaveChanges();
			}
		}

		public void DeleteRentals(IEnumerable<int> rentalIds)
		{
			using (var videoStore = new VideoStoreContext())
			{
				foreach (int rentalId in rentalIds)
				{
					DeleteRental(videoStore, rentalId);
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

		private void AddRental(VideoStoreContext videoStore, int userId, int movieId)
		{
			var rental = new Rental
			{
				DateOfRental = DateTime.Now,
				DueDate = DateTime.Now.AddDays(4),
				MovieId = movieId,
				UserId = userId
			};

			var rentedMovie = new RentedMovie
			{
				MovieId = movieId,
				UserId = userId,
			};

			videoStore.Rentals.Add(rental);
			videoStore.RentedMovies.Add(rentedMovie);
		}

		private void DeleteRental(VideoStoreContext videoStore, int id)
		{
			var rental = new Rental { Id = id };
			videoStore.Rentals.Attach(rental);
			videoStore.Rentals.Remove(rental);
		}
	}
}
