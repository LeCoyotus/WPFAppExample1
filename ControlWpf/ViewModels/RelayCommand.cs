﻿using System;
using System.Windows.Input;

namespace ControlWpf.ViewModels
{
	public class RelayCommand : ICommand
	{
		private readonly Action<object> _execute;
		private readonly Predicate<object> _canExecute;

		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		{
			_execute = execute ?? throw new NullReferenceException("execute");
			_canExecute = canExecute;
		}

		public RelayCommand(Action<object> execute) : this(execute, null) {}

		public event EventHandler CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}
		public bool CanExecute(object parameter)
		{
			return _canExecute?.Invoke(parameter) ?? true;
		}

		public void Execute(object parameter)
		{
			_execute.Invoke(parameter);
		}

	}
}