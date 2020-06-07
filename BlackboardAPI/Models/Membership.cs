using System;

namespace BlackboardAPI
{
	public class Membership
	{
		public string userId { get; set; }

		public string courseId { get; set; }

		public string dataSourceId { get; set; }

		public string created { get; set; }

		public Availability availability { get; set; }

		public string courseRoleId { get; set; }
	}
}

