using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MvvmDialogs;
using Contracts;

namespace ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
		private RentalViewModel _currentRental;
		private UserViewModel _currentUser;
		private DateTimeViewModel _currentDueDate;
		private DateTimeViewModel _currentDateOfRental;
		private IEnumerable<Rental> _rentals;
		private readonly IVideoStore _videoStoreService;
		private readonly IDialogService _dialogService;
		private readonly IVideoClubRules _rules;
		private Filter _currentFilter = Filter.NoFilter;

		public MainWindowViewModel(IDialogService dialogService, IVideoStore videoStoreService, IVideoClubRules rules)
		{
			_dialogService = dialogService;
			_videoStoreService = videoStoreService;
			_rules = rules;

			Rentals = new ObservableCollection<RentalViewModel>();
			Users = new ObservableCollection<UserViewModel>();
			DateOfRentCollection = new ObservableCollection<DateTimeViewModel>();
			DueDateCollection = new ObservableCollection<DateTimeViewModel>();

			ResetFilterCommand = new RelayCommand(ResetFilter, () => _currentFilter != Filter.NoFilter);
			ShowRentalDialogCommand = new RelayCommand(ShowRentalDialog);

			_rentals = _videoStoreService.GetRentals().OrderBy(r => r.Movie.Title);

			ReloadRentals();
			ReloadUsers();
			ReloadDatesOfRental();
			ReloadDueDates();
		}

		public RelayCommand ResetFilterCommand { get; private set; }

		public RelayCommand ShowRentalDialogCommand { get; private set; }

		public ObservableCollection<RentalViewModel> Rentals { get; set; }

		public ObservableCollection<UserViewModel> Users { get; set; }

		public ObservableCollection<DateTimeViewModel> DateOfRentCollection { get; set; }

		public ObservableCollection<DateTimeViewModel> DueDateCollection { get; set; }

		public RentalViewModel CurrentRental
		{
			get
			{
				return _currentRental;
			}
			set
			{
				Set(ref _currentRental, value);
			}
		}

		public UserViewModel CurrentUser
		{
			get
			{
				return _currentUser;
			}
			set
			{
				if (Set(ref _currentUser, value) && value != null)
				{
					_currentFilter = Filter.User;
					ResetFilterCommand.RaiseCanExecuteChanged();
					ReloadRentals();
				}
			}
		}

		public DateTimeViewModel CurrentDateOfRental
		{
			get
			{
				return _currentDateOfRental;
			}
			set
			{
				if (Set(ref _currentDateOfRental, value) && value != null)
				{
					_currentFilter = Filter.DateOfRental;
					ResetFilterCommand.RaiseCanExecuteChanged();
					ReloadRentals();
				}
			}
		}

		public DateTimeViewModel CurrentDueDate
		{
			get
			{
				return _currentDueDate;
			}
			set
			{
				if (Set(ref _currentDueDate, value) && value != null)
				{
					_currentFilter = Filter.DueDate;
					ResetFilterCommand.RaiseCanExecuteChanged();
					ReloadRentals();
				}
			}
		}

		private void ReloadRentals()
		{
			Rentals.Clear();

			foreach (var item in _rentals)
			{
				var rental = new RentalViewModel(item);

				switch (_currentFilter)
				{
					case Filter.User:
						if (rental.User.FullName.Equals(_currentUser.FullName))
						{
							Rentals.Add(rental);
						}

						CurrentDateOfRental = null;
						CurrentDueDate = null;
						break;

					case Filter.DueDate:
						if (rental.DueDate.Equals(_currentDueDate))
						{
							Rentals.Add(rental);
						}

						CurrentDateOfRental = null;
						CurrentUser = null;
						break;

					case Filter.DateOfRental:
						if (rental.DateOfRental.Equals(_currentDateOfRental))
						{
							Rentals.Add(rental);
						}

						CurrentDueDate = null;
						CurrentUser = null;
						break;

					default:
						Rentals.Add(rental);
						CurrentDateOfRental = null;
						CurrentDueDate = null;
						CurrentUser = null;
						break;
				}
			}
		}

		private void ReloadUsers()
		{
			Users.Clear();

			var rentals = _rentals.OrderBy(r => r.User.FirstName);

			foreach (var item in rentals)
			{
				var rental = new RentalViewModel(item);

				if (!Users.Contains(rental.User))
					Users.Add(rental.User);
			}
		}

		private void ReloadDatesOfRental()
		{
			DateOfRentCollection.Clear();

			var rentals = _rentals.OrderBy(r => r.DateOfRental);

			foreach (var item in rentals)
			{
				var rental = new RentalViewModel(item);

				if (!DateOfRentCollection.Contains(rental.DateOfRental))
					DateOfRentCollection.Add(rental.DateOfRental);
			}
		}

		private void ReloadDueDates()
		{
			DueDateCollection.Clear();

			var rentals = _rentals.OrderBy(r => r.DueDate);

			foreach (var item in rentals)
			{
				var rental = new RentalViewModel(item);

				if (!DueDateCollection.Contains(rental.DueDate))
					DueDateCollection.Add(rental.DueDate);
			}
		}

		private void ResetFilter()
		{
			_currentFilter = Filter.NoFilter;
			ResetFilterCommand.RaiseCanExecuteChanged();
			ReloadRentals();
		}

		private void ShowRentalDialog()
		{
			var dialogViewModel = new DlgRentalViewModel(_videoStoreService, _rules);

			_dialogService.ShowDialog(this, dialogViewModel);
		}
	}
}
