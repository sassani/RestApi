using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EF_V0.Core.Models.Responses
{
	public class Error
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string Id { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string Code { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string Title { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string Detail { get; set; }
	}
}
