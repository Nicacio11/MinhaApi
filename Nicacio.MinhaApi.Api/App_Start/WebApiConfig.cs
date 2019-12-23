using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Nicacio.MinhaApi.Api.Filters;
using Nicacio.MinhaApi.Api.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Nicacio.MinhaApi.Api
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
            // Web API configuration and services
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings 
            {
                //devolver com as propriedas minusculas
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                //ignorar null
                NullValueHandling = NullValueHandling.Ignore
            };

            //removendo o formatter
            //var xmlFormatter = config.Formatters.XmlFormatter;
            //config.Formatters.Remove(xmlFormatter);
            config.Formatters.Add(new CsvMediaTypeFormatter());
            config.Filters.Add(new FillResponseWithHATEOASAttribute());
			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
