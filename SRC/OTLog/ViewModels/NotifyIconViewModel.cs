﻿using OTLog.Core.Constants;
using OTLog.Core.Events;
using OTLog.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Windows;
using System.Windows.Input;

namespace OTLog.ViewModels
{
    public class NotifyIconViewModel
    {
        #region private
        private IEventAggregator _eventAggregator;
        #endregion
        public ICommand ShowWindowCommand { get; }
        private void ShowWindow()
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
                Application.Current.MainWindow.WindowState = WindowState.Normal;

            Application.Current.MainWindow.Show();
        }
        private bool CanShowWindow() => 
            Application.Current.MainWindow.WindowState == WindowState.Minimized || 
            !Application.Current.MainWindow.IsVisible;

        public ICommand HideWindowCommand { get; }
        private void HideWindow()
        {
            Application.Current.MainWindow.Hide();
        }
        private bool CanHideWindow() => Application.Current.MainWindow.IsVisible;

        public ICommand ExitApplicationCommand { get; }
        private void ExitApplication() => Application.Current.Shutdown();

        public NotifyIconViewModel()
        {
            ExitApplicationCommand = new DelegateCommand(ExitApplication);

            _eventAggregator = (Application.Current as PrismApplication).Container.Resolve(typeof(IEventAggregator)) as IEventAggregator;
            _eventAggregator.GetEvent<NewNoticeEvent>().Subscribe(ShowWindow);
        }
    }

    //public class DelegateCommand : ICommand
    //{
    //    public DelegateCommand(Action commandAction, Func<bool> canExecuteFunc)
    //        => (CommandAction, CanExecuteFunc) = (commandAction, canExecuteFunc);

    //    public Action CommandAction { get; set; }
    //    public Func<bool> CanExecuteFunc { get; set; }

    //    public void Execute(object parameter)
    //        => CommandAction();

    //    public bool CanExecute(object parameter)
    //        => CanExecuteFunc?.Invoke() ?? true;

    //    public event EventHandler CanExecuteChanged
    //    {
    //        add => CommandManager.RequerySuggested += value; 
    //        remove => CommandManager.RequerySuggested -= value; 
    //    }
    //}
}
