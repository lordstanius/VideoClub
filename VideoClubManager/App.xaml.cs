﻿using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using MvvmDialogs;
using ServiceProvider;

namespace VideoClubManager
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			SimpleIoc.Default.Register<IDialogService>(() => new DialogService(new DialogLocator()));
			SimpleIoc.Default.Register<IVideoStore>(() => VideoStore.GetService());
		}
	}
}
