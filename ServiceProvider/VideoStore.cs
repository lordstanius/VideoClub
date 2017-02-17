using Contracts;

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
