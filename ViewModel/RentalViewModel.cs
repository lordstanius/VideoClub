using Model;

namespace ViewModel
{
	public class RentalViewModel : BindableEntity<Rental>
	{
		public RentalViewModel(Rental rental) :
			base(rental)
		{
		}

		public bool IsSelected { get; set; }

		public DateTimeViewModel DateOfRental
		{
			get { return new DateTimeViewModel(Model.DateOfRental); }
		}

		public DateTimeViewModel DueDate
		{
			get { return new DateTimeViewModel(Model.DueDate); }
		}

		public MovieViewModel Movie
		{
			get { return new MovieViewModel(Model.Movie); }
		}

		public UserViewModel User
		{
			get { return new UserViewModel(Model.User); }
		}

		public string Phone
		{
			get { return Model.User.Phone; }
		}

		public string Email
		{
			get { return Model.User.Email; }
		}
	}
}
