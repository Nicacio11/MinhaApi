using Nicacio.Comum.Repositorio.Interfaces;
using Nicacio.MinhaApi.AcessoDados.EF;
using Nicacio.MinhaApi.Api.AutoMapper;
using Nicacio.MinhaApi.Api.DTO;
using Nicacio.MinhaApi.Api.Filters;
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
            var retorno = AutoMapperManager.Instance.Mapper.Map<List<Aluno>, List<AlunoDTO>>(repositorio.GetAll());
            if (!retorno.Any())
                return NotFound();
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
            AlunoDTO dto = AutoMapperManager.Instance.Mapper.Map<Aluno, AlunoDTO>(aluno);

            return Content(HttpStatusCode.Found, dto);
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

        [ApplyModelValidation]
        public IHttpActionResult Post([FromBody]AlunoDTO dto)
        {
            if (dto == null)
                return BadRequest();
            try
            {
                Aluno aluno = AutoMapperManager.Instance.Mapper.Map<AlunoDTO, Aluno>(dto);
                repositorio.Inserir(aluno);
                return Created($"{Request.RequestUri}/{aluno.Id}", aluno);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }

        }

        [ApplyModelValidation]
        public IHttpActionResult Put(int? id, [FromBody] AlunoDTO dto)
        {
            if (dto == null)
                return BadRequest();
            try
            {
                if (!id.HasValue)
                    return BadRequest();
                Aluno aluno = AutoMapperManager.Instance.Mapper.Map<AlunoDTO, Aluno>(dto);
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
