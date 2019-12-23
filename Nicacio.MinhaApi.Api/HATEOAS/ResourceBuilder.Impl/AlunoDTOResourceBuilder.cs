using Nicacio.MinhaApi.Api.DTO;
using Nicacio.MinhaApi.Api.HATEOAS.ResourceBuilder.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace Nicacio.MinhaApi.Api.HATEOAS.ResourceBuilder.Impl
{
    public class AlunoDTOResourceBuilder : IResourceBuilder
    {
        public void BuildResource(object resource, HttpRequestMessage request)
        {
            AlunoDTO dto = resource as AlunoDTO;

            if (dto == null)
                throw new ArgumentNullException($"era esperado um AlunoDTO, porém, foi enviado um {resource.GetType().Name}");

            UrlHelper urlHelper = new UrlHelper(request);

            string alunoDtoRoute = urlHelper.Link("DefaultApi", new { controller = "Aluno", id = dto.Id });

            dto.Links.Add(new RestLink
            {
                Rel = "get",
                Href = alunoDtoRoute
            });
            dto.Links.Add(new RestLink
            {
                Rel = "put",
                Href = alunoDtoRoute
            });
            dto.Links.Add(new RestLink
            {
                Rel = "delete",
                Href = alunoDtoRoute
            });
        }
    }
}