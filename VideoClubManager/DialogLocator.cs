using System;
using System.ComponentModel;
using MvvmDialogs.DialogTypeLocators;
using ViewModel;

namespace VideoClubManager
{
	class DialogLocator : IDialogTypeLocator
	{
		public Type Locate(INotifyPropertyChanged viewModel)
		{
			if (viewModel is DlgRentalViewModel)
				return typeof(UI.DlgRental);

			throw new ArgumentException("No dialog is found matching the view-model type of " + viewModel.GetType());
		}
	}
}
