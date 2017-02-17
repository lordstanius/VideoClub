using System.Runtime.Serialization;

namespace Model
{
	[DataContract]
	public class RentedMovie : IEntity
	{
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		public int MovieId { get; set; }

		[DataMember]
		public int UserId { get; set; }
	}
}
