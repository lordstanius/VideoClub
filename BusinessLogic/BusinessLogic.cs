using System;
using Contracts;

namespace BusinessLogic
{
	public class VideoClubRules : IVideoClubRules
	{
		public bool AllowMultipleCopies { get; } = false;

		public bool AllowMultipleRentals { get; } = false;

		public int MovieNumberLimit { get; } = 4;
	}
}
