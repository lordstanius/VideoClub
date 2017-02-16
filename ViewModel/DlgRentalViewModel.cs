using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmDialogs;

namespace ViewModel
{
	public class DlgRentalViewModel : ViewModelBase, IModalDialogViewModel
	{
		private bool? _dialogResult;

		public DlgRentalViewModel()
		{
			OkCommand = new RelayCommand(() =>
			{
				CompleteRental?.Invoke();
				DialogResult = true;
			});
		}

		public ICommand OkCommand { get; private set; }

		public ICommand CancelCommand { get; private set; }

		public Action CompleteRental { get; set; }

		public bool? DialogResult
		{
			get { return _dialogResult; }
			set { Set(ref _dialogResult, value); }
		}
	}
}
