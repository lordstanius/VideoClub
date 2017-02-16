using Model;

namespace ViewModel
{
	public class UserViewModel : BindableEntity<User>
	{
		public UserViewModel(User user) :
			base(user)
		{
		}

		public string FullName { get { return string.Join(" ", Model.FirstName, Model.LastName); } }

		public override string ToString()
		{
			return FullName;
		}
	}
}
