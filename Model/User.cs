using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Model
{
	[DataContract]
	public class User : IEntity
	{
		[DataMember]
		public int Id { get; set; }

		[MaxLength(30)]
		[DataMember]
		public string FirstName { get; set; }

		[MaxLength(30)]
		[DataMember]
		public string LastName { get; set; }

		[MaxLength(50)]
		[DataMember]
		public string Street { get; set; }

		[MaxLength(20)]
		[DataMember]
		public string City { get; set; }

		[MaxLength(15)]
		[DataMember]
		public string Phone { get; set; }

		[MaxLength(60)]
		[DataMember]
		public string Email { get; set; }
	}
}
