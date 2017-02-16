using System;

namespace ViewModel
{
	public class DateTimeViewModel : BindableBase<DateTime>
	{
		public DateTimeViewModel(DateTime dateTime) :
			base(dateTime)
		{
		}

		public override bool Equals(object obj)
		{
			return obj != null &&
				obj is DateTimeViewModel &&
				((DateTimeViewModel)obj).Model.ToShortDateString().Equals(Model.ToShortDateString());
		}

		public override int GetHashCode()
		{
			return Model.GetHashCode();
		}

		public override string ToString()
		{
			return Model.ToShortDateString();
		}
	}
}
