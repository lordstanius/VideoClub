using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider
{
	public static class VideoStore
	{
		public static IVideoStore GetService()
		{
			return new VideoStoreProvider();
		}
	}
}
