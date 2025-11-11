using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UI.ViewModels
{
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string name = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

		protected bool SetField<T>(ref T field, T value, [CallerMemberName] string name = "")
		{
			if (Equals(field, value)) return false;
			field = value;
			OnPropertyChanged(name);
			return true;
		}
	}
}
