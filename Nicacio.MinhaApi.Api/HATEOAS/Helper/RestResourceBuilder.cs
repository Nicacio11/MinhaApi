using Nicacio.MinhaApi.Api.HATEOAS.ResourceBuilder.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;

namespace Nicacio.MinhaApi.Api.HATEOAS.Helper
{
    public class RestResourceBuilder
    {
        public static void BuildResource(object resource, HttpRequestMessage request)
        {
            IEnumerable enumerable = resource as IEnumerable;
            Type dtoType;

            if (enumerable == null)
                dtoType = resource.GetType();
            else
                dtoType = resource.GetType().GetGenericArguments()[0];
            if(dtoType.BaseType != typeof(RestResource))
            {
                throw new ArgumentNullException($"era esperado um RestResource, porém, foi enviado um {resource.GetType().Name}");
            }

            Assembly currentArrembly = Assembly.GetExecutingAssembly();
            IResourceBuilder resourceBuilder
                = (IResourceBuilder)Activator.CreateInstance(currentArrembly
                .GetType($"Nicacio.MinhaApi.Api.HATEOAS.ResourceBuilder.Impl.{dtoType.Name}ResourceBuilder"));
            if (enumerable == null)
                resourceBuilder.BuildResource(resource, request);
            else
                foreach(var item in enumerable)
                    resourceBuilder.BuildResource(item, request);

        }
    }
}