using System;
using Contracts;

namespace BusinessLogic
{
	public class VideoClubRules : IVideoClubRules
	{
		public bool AllowMultipleCopies { get; } = false;

		public bool AllowMultipleRentals { get; } = false;

		public bool DoNotAllowRentalOfPreviouslyRentedMovie { get; } = false;

		public int MovieNumberLimit { get; } = 4;

		public int DuePeriod(int numOfMovies)
		{
			switch (numOfMovies)
			{
				case 1:
					return 2;

				case 2:
				case 3:
					return 4;

				default:
					return 6;
			}
		}
	}
}
