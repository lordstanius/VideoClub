using Model;

namespace ViewModel
{
	public class MovieViewModel : BindableEntity<Movie>
	{
		public MovieViewModel(Movie movie) :
			base(movie)
		{
		}

		public override string ToString()
		{
			return Model.Title;
		}
	}
}
