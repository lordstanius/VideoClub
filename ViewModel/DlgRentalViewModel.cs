using System.Linq;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmDialogs;
using ServiceProvider;
using System;

namespace ViewModel
{
	public class DlgRentalViewModel : ViewModelBase, IModalDialogViewModel
	{
		private bool? _dialogResult;
		private UserViewModel _currentUser;
		private MovieViewModel _currentMovie;
		private MovieViewModel _selectedMovie;
		private IVideoStore _videoStoreService;

		public DlgRentalViewModel(IVideoStore videoStoreService)
		{
			_videoStoreService = videoStoreService;


			Movies = new ObservableCollection<MovieViewModel>();
			ChosenMovies = new ObservableCollection<MovieViewModel>();
			Users = new ObservableCollection<UserViewModel>();

			foreach (var item in _videoStoreService.GetMovies())
			{
				Movies.Add(new MovieViewModel(item));
			}

			foreach (var item in _videoStoreService.GetUsers())
			{
				Users.Add(new UserViewModel(item));
			}

			OkCommand = new RelayCommand(CompleteRental, () => IsInputValid);
			AddMovieCommand = new RelayCommand(AddMovie, () => _currentMovie != null && !ChosenMovies.Contains(_currentMovie) && ChosenMovies.Count < 4);
			RemoveMovieCommand = new RelayCommand(RemoveMovie, () => _selectedMovie != null);
		}

		public RelayCommand OkCommand { get; private set; }

		public RelayCommand AddMovieCommand { get; private set; }

		public RelayCommand RemoveMovieCommand { get; private set; }

		public ObservableCollection<MovieViewModel> Movies { get; private set; }

		public ObservableCollection<MovieViewModel> ChosenMovies { get; private set; }

		public ObservableCollection<UserViewModel> Users { get; private set; }

		public bool CanSelectMovie { get { return _currentUser != null; } }

		public bool IsInputValid
		{
			get
			{
				// TODO: Perform bussiness logic here
				return _currentUser != null && ChosenMovies.Count >= 1;
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
				if (Set(ref _currentUser, value))
				{
					RaisePropertyChanged(nameof(Address));
					RaisePropertyChanged(nameof(Phone));
					RaisePropertyChanged(nameof(Email));
					RaisePropertyChanged(nameof(CanSelectMovie));
					OkCommand.RaiseCanExecuteChanged();
				}
			}
		}

		public MovieViewModel CurrentMovie
		{
			get
			{
				return _currentMovie;
			}
			set
			{
				if (Set(ref _currentMovie, value))
				{
					AddMovieCommand.RaiseCanExecuteChanged();
				}
			}
		}

		public MovieViewModel SelectedMovie
		{
			get
			{
				return _selectedMovie;
			}
			set
			{
				if (Set(ref _selectedMovie, value))
				{
					RemoveMovieCommand.RaiseCanExecuteChanged();
				}
			}
		}

		public string Address
		{
			get
			{
				return _currentUser != null ? $"{_currentUser.Model.Street}, {_currentUser.Model.City}" : "";
			}
		}

		public string Phone { get { return _currentUser?.Model.Phone; } }

		public string Email { get { return _currentUser?.Model.Email; } }

		public bool? DialogResult
		{
			get { return _dialogResult; }
			set { Set(ref _dialogResult, value); }
		}

		private void CompleteRental()
		{
			DialogResult = true;
		}

		private void RemoveMovie()
		{
			ChosenMovies.Remove(_selectedMovie);

			OkCommand.RaiseCanExecuteChanged();
			RemoveMovieCommand.RaiseCanExecuteChanged();
			AddMovieCommand.RaiseCanExecuteChanged();
		}

		private void AddMovie()
		{
			ChosenMovies.Add(_currentMovie);

			OkCommand.RaiseCanExecuteChanged();
			AddMovieCommand.RaiseCanExecuteChanged();
		}
	}
}
