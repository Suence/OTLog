﻿using OTLog.Views;
using System;
using System.Windows;
using System.Windows.Input;

namespace OTLog.ViewModels
{
    public class NotifyIconViewModel
    {
        public ICommand ShowWindowCommand { get; }
        private void ShowWindow()
        {
            Application.Current.MainWindow.Show();
        }
        private bool CanShowWindow() => !Application.Current.MainWindow.IsVisible;

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
            ShowWindowCommand = new DelegateCommand(ShowWindow, CanShowWindow);
            HideWindowCommand = new DelegateCommand(HideWindow, CanHideWindow);
            ExitApplicationCommand = new DelegateCommand(ExitApplication, null);
        }
    }

    /// <summary>
    /// Simplistic delegate command for the demo.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        public DelegateCommand(Action commandAction, Func<bool> canExecuteFunc)
            => (CommandAction, CanExecuteFunc) = (commandAction, canExecuteFunc);

        public Action CommandAction { get; set; }
        public Func<bool> CanExecuteFunc { get; set; }

        public void Execute(object parameter)
            => CommandAction();

        public bool CanExecute(object parameter)
            => CanExecuteFunc?.Invoke() ?? true;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value; 
            remove => CommandManager.RequerySuggested -= value; 
        }
    }
}
