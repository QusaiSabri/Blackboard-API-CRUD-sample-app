using System;

namespace BlackboardAPI
{
	public class User
	{
		public string id { get; set; }

		public string uuid { get; set; }

		public string externalId { get; set; }

		public string dataSourceId { get; set; }

		public string userName { get; set; }

		public string password { get; set; }

		public string studentId { get; set; }

		public string educationLevel { get; set; }

		public string gender { get; set; }

		public string birthDate { get; set; }

		public string created { get; set; }

		public string lastLogin { get; set; }

		public string[] systemRoleIds { get; set; }

		public Availability availability { get; set; }

		public Name name { get; set; }

		public Job job { get; set; }

		public Contact contact { get; set; }

		public Address address { get; set; }

		public Locale locale { get; set; }
	}
}

