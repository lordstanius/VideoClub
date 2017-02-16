using System;
using System.Runtime.Serialization;

namespace Model
{
	[DataContract]
	public class Rental : IEntity
	{
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		public DateTime DateOfRental { get; set; }

		[DataMember]
		public DateTime DueDate { get; set; }

		[DataMember]
		public int MovieId { get; set; }

		[DataMember]
		public int UserId { get; set; }

		[DataMember]
		public Movie Movie { get; set; }

		[DataMember]
		public User User { get; set; }
	}
}
