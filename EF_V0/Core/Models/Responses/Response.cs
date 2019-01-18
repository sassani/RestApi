using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;

namespace EF_V0.Core.Models.Responses
{
	public class Response
	{
		[JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
		public object Data { get; private set; } = null;

		[JsonProperty(PropertyName = "errors", NullValueHandling = NullValueHandling.Ignore)]
		public IEnumerable<Error> Errors { get; private set; } = null;

		[JsonProperty(PropertyName = "meta", NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary<string, object> Meta { get; set; } = null;

		[JsonProperty(PropertyName = "links", NullValueHandling = NullValueHandling.Ignore)]
		public object Links { get; set; }

		private readonly HttpStatusCode status;

		public Response(HttpStatusCode status, IEnumerable<Error> errors)
		{
			Errors = errors;
			this.status = status;
		}

		public Response(HttpStatusCode status, object data, object links = null, Dictionary<string, object> meta = null)
		{
			Data = data;
			Meta = meta;
			Links = links;
			this.status = status;
		}

		[OnSerializing]
		internal void OnSerializingMethod(StreamingContext context)
		{
			// Response MUST contain at least one of the following top level members

			if (Data == null && Errors == null && Meta == null)
			{
				// Otherwise we create empty array for data
				Data = new JArray();
			}
		}

		public override string ToString()
		{
			JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Include,
				ContractResolver = new CoreJsonLoaderResolver()
			};

			using (var writer = new StringWriter())
			{
				using (var jsonWriter = new JsonTextWriter(writer))
				{
					jsonWriter.CloseOutput = false;
					jsonWriter.AutoCompleteOnClose = false;

					var jsonSerializer = JsonSerializer.Create(jsonSerializerSettings);
					jsonSerializer.Serialize(jsonWriter, this);
				}

				return writer.ToString();
			}

		}

		public ActionResult ToActionResult()
		{
			return new ContentResult()
			{
				ContentType = "application/json; charset=utf-8",
				Content = ToString(),
				StatusCode = (int)status
			};
		}

	}

	class CoreJsonLoaderResolver : DefaultContractResolver
	{
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			JsonProperty prop = base.CreateProperty(member, memberSerialization);

			if (prop.PropertyName == "LazyLoader")
			{
				prop.Ignored = true;
			}

			prop.PropertyName = Char.ToLower(prop.PropertyName[0]) + prop.PropertyName.Substring(1);
			return prop;
		}
	}
}
