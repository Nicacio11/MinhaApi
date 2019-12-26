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
    /// <summary>
    /// Microsoft.Owin.Host.SystemWeb
    /// Microsoft.AspNet.WebApi.Owin
    /// Microsoft.Owin.Security.OAuth
    /// Microsoft.Owin.Cors
    /// para conseguir o token é preciso conseguir o token em /token passando usuário senha e o grant type
    /// no www-form_urlconcoded
    /// utilizando postman ou yarc
    /// username - 123
    /// password - 123
    /// grant_type - password
    /// pega o token e adiciona novo header com key Authorization e Value o token informado
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Aluno")]
    public class AlunoEFController : ApiController
    {
        private IRepositorioMinhaApi<Aluno, int> repositorio = new RepositorioAlunoEF(new Contexto());

        /// <summary>
        /// IHttpActionResult faz todo processo de maneira assincrona
        /// o que permite melhor aproveitamento do pool de thread do servidor
        /// e a sintaxe é mais clara
        /// </summary>
        /// <returns></returns>
        //fator qualitativo é utilizado para definir qual o formato definido para retorno xml ou json
        //quanto mais perto de 1 mais qualitativo
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

            return Content(HttpStatusCode.OK, dto);
        }

        [Route("por-nome/{nomeAluno}")]
        public IHttpActionResult Get(string nomeAluno)
        {
            List<AlunoDTO> dtos = AutoMapperManager.Instance.Mapper
                .Map<List<Aluno>, List<AlunoDTO>>(repositorio.GetAll(x => x.Nome.ToLower().Contains(nomeAluno)));

            return Ok(dtos);
        }
        /// <summary>
        /// FromBody é um decorator indicando que a leitura do objeto será feita apartir do corpo da requisição
        /// o model binder vai fazer com que seja associado com o objeto
        /// </summary>
        /// <param name="aluno"></param>
        /// <returns></returns>
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
