using Nicacio.Comum.Repositorio.Interfaces;
using Nicacio.MinhaApi.Dominio;
using Nicacio.MinhaApi.Repositorio.NH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nicacio.MinhaApi.Api.Controllers
{
    public class AlunoNHController : ApiController
    {
		private IRepositorioMinhaApi<AlunoNH, int> repositorio = new RepositorioAlunoNH();
		
		public IEnumerable<AlunoNH> Get()
		{
			return repositorio.GetAll();
		}
		public AlunoNH Get(int? id)
		{
			return repositorio.GetById(id.Value);
		}
    }
}
