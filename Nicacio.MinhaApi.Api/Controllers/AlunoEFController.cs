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

		/// <summary>
		/// IHttpActionResult faz todo processo de maneira assincrona
		/// o que permite melhor aproveitamento do pool de thread do servidor
		/// e a sintaxe é mais clara
		/// </summary>
		/// <returns></returns>

		//public IEnumerable<Aluno> Get()
		public IHttpActionResult Get()
		{
			var retorno = repositorio.GetAll();
			return Ok(retorno);
		}

		public IHttpActionResult Get(int? id)
		{
			if (!id.HasValue)
				return BadRequest();
			//return Request.CreateResponse(HttpStatusCode.BadRequest);
			Aluno aluno = repositorio.GetById(id.Value);
			if (aluno == null)
				return NotFound();
			//return Request.CreateResponse(HttpStatusCode.NotFound);
			return Content(HttpStatusCode.Found, aluno);
		}

		/// <summary>
		/// FromBody é um decorator indicando que a leitura do objeto será feita apartir do corpo da requisição
		/// o model binder vai fazer com que seja associado com o objeto
		/// </summary>
		/// <param name="aluno"></param>
		/// <returns></returns>
		//public HttpResponseMessage Post([FromBody]Aluno aluno)
		//{
		//	if (aluno == null)
		//		return Request.CreateResponse(HttpStatusCode.BadRequest);
		//	try
		//	{
		//		repositorio.Inserir(aluno);
		//		return Request.CreateResponse(HttpStatusCode.Created);
		//	}
		//	catch (Exception ex)
		//	{

		//		return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
		//	}
		//}

		///web api2
		public IHttpActionResult Post([FromBody]Aluno aluno)
		{
			if (aluno == null)
				return BadRequest();
			try
			{
				repositorio.Inserir(aluno);
				return Created($"{Request.RequestUri}/{aluno.Id}", aluno);
			}
			catch (Exception ex)
			{

				return InternalServerError(ex);
			}
		}

		public IHttpActionResult Put(int? id, [FromBody] Aluno aluno)
		{
			if (aluno == null)
				return BadRequest();
			try
			{
				if (!id.HasValue)
					return BadRequest();

				aluno.Id = id.Value;
				repositorio.Update(aluno);
				return Ok();
			}
			catch (Exception ex)
			{

				return InternalServerError(ex);
			}
		}

		public IHttpActionResult Delete(int? id)
		{
			try
			{
				if (!id.HasValue)
					return BadRequest();

				repositorio.DeleteById(id.Value);
				return Ok();
			}
			catch (Exception ex)
			{

				return InternalServerError(ex);
			}
		}
	}
}
