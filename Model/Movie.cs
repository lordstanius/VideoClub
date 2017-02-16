using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Model
{
	[DataContract]
	public class Movie : IEntity
	{
		[DataMember]
		public int Id { get; set; }

		[MaxLength(50)]
		[DataMember]
		public string Title { get; set; }

		[DataMember]
		public int NumOfCopies { get; set; }
	}
}
