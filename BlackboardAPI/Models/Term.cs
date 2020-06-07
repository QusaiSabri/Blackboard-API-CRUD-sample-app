using System;

namespace BlackboardAPI
{
	public class Term
	{
		public string id { get; set; }

		public string externalId { get; set; }

		public string dataSourceid { get; set; }

		public string name { get; set; }

		public string description { get; set; }

		public Availability availability { get; set; }

	}
}