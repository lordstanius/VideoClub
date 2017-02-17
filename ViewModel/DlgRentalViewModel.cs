﻿using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Contracts;
using MvvmDialogs;

namespace ViewModel
{
	public class DlgRentalViewModel : ViewModelBase, IModalDialogViewModel
	{
		private bool? _dialogResult;
		private UserViewModel _currentUser;
		private MovieViewModel _currentMovie;
		private MovieViewModel _selectedMovie;
		private IVideoStore _videoStoreService;
		private IVideoClubRules _rules;

		public DlgRentalViewModel(IVideoStore videoStoreService, IVideoClubRules rules)
		{
			_videoStoreService = videoStoreService;

			_rules = rules;

			Movies = new ObservableCollection<MovieViewModel>();
			ChosenMovies = new ObservableCollection<MovieViewModel>();
			Users = new ObservableCollection<UserViewModel>();

			foreach (var item in _videoStoreService.GetMovies())
			{
				if (item.NumOfCopies > 0)
				{
					Movies.Add(new MovieViewModel(item));
				}
			}

			foreach (var item in _videoStoreService.GetUsers())
			{
				Users.Add(new UserViewModel(item));
			}

			OkCommand = new RelayCommand(CompleteRental, () => IsInputValid);
			AddMovieCommand = new RelayCommand(AddMovie, () => CanAddMovie && IsRentingAllowed);
			RemoveMovieCommand = new RelayCommand(RemoveMovie, () => _selectedMovie != null);

			WarningVisibility = Visibility.Hidden;
		}

		public RelayCommand OkCommand { get; private set; }

		public RelayCommand AddMovieCommand { get; private set; }

		public RelayCommand RemoveMovieCommand { get; private set; }

		public ObservableCollection<MovieViewModel> Movies { get; private set; }

		public ObservableCollection<MovieViewModel> ChosenMovies { get; private set; }

		public ObservableCollection<UserViewModel> Users { get; private set; }

		public bool CanSelectMovie { get { return _currentUser != null && IsRentingAllowed; } }

		public Visibility WarningVisibility { get; private set; }

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
					WarningVisibility = IsRentingAllowed ? Visibility.Hidden : Visibility.Visible;

					RaisePropertyChanged(nameof(Address));
					RaisePropertyChanged(nameof(Phone));
					RaisePropertyChanged(nameof(Email));
					RaisePropertyChanged(nameof(CanSelectMovie));
					RaisePropertyChanged(nameof(WarningVisibility));
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

		public bool IsInputValid
		{
			get
			{
				return _currentUser != null && ChosenMovies.Count >= 1;
			}
		}

		private bool IsRentingAllowed
		{
			get
			{
				if (!_rules.AllowMultipleRentals)
				{
					var user = _videoStoreService.GetRentals().FirstOrDefault(r => r.UserId == _currentUser.Model.Id);
					if (user != null)
					{
						// Found: user has some movies already rented
						return false;
					}
				}

				return true;
			}
		}

		private bool CanAddMovie
		{
			get
			{
				if (_currentMovie == null)
					return false;

				if (!_rules.AllowMultipleCopies && ChosenMovies.Contains(_currentMovie))
					return false;

				if (ChosenMovies.Count >= _rules.MovieNumberLimit)
					return false;

				return true;
			}
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
