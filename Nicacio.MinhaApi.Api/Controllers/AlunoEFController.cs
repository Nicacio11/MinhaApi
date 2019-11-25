using Nicacio.Comum.Repositorio.Interfaces;
using Nicacio.MinhaApi.AcessoDados.EF;
using Nicacio.MinhaApi.Dominio;
using Nicacio.MinhaApi.Repositorio.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nicacio.MinhaApi.Api.Controllers
{
    public class AlunoEFController : ApiController
    {
		private IRepositorioMinhaApi<Aluno, int> repositorio = new RepositorioAlunoEF(new Contexto());


		public IEnumerable<Aluno> Get()
		{
			var retorno = repositorio.GetAll();
			return retorno;
		}
		public Aluno Get(int?id)
		{
			return repositorio.GetById(id.Value);
		}

    }
}
