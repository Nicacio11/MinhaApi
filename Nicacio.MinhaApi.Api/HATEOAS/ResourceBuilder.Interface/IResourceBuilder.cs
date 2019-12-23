using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nicacio.MinhaApi.Api.HATEOAS.ResourceBuilder.Interface
{
    public interface IResourceBuilder
    {
        void BuildResource(object resource, HttpRequestMessage request);
    }
}
