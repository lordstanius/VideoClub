namespace ViewModel
{
	public class BindableBase<T> : GalaSoft.MvvmLight.ViewModelBase
	{
		private T _model;

		public BindableBase(T modelObject)
		{
			_model = modelObject;
		}

		public T Model { get { return _model; } }

		public override bool Equals(object obj)
		{
			return obj != null &&
				obj is BindableBase<T> &&
				((BindableBase<T>)obj).Equals(_model);
		}

		public override int GetHashCode()
		{
			return Model.GetHashCode();
		}
	}
}
