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
			AlunoNH aluno = repositorio.GetById(id.Value);
			if (aluno == null)
				return NotFound();
			//return Request.CreateResponse(HttpStatusCode.NotFound);
			return Content(HttpStatusCode.Found, aluno);
		}

		///web api2
		public IHttpActionResult Post([FromBody]AlunoNH aluno)
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

		public IHttpActionResult Put(int? id, [FromBody] AlunoNH aluno)
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
