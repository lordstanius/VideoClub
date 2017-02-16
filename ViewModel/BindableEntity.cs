using Model;

namespace ViewModel
{
	public class BindableEntity<T> : BindableBase<T> where T : IEntity
	{
		public BindableEntity(T modelObject):
			base(modelObject)
		{
		}

		public override bool Equals(object obj)
		{
			return obj != null &&
				obj is BindableEntity<T> &&
				(((BindableEntity<T>)obj).Model).Id == Model.Id;
		}

		public override int GetHashCode()
		{
			return Model.Id.GetHashCode();
		}
	}
}
