using AutoMapper;
using Nicacio.MinhaApi.Api.DTO;
using Nicacio.MinhaApi.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nicacio.MinhaApi.Api.AutoMapper
{
    public class AutoMapperManager
    {
        /// <summary>
        /// Lazy carregamento preguiçoso, será carregado apenas quando for chamado,
        /// ao ser invocado a primeira vez, na segunda ~verifica que ja foi chamado e retorno a instancia que foi criada
        /// </summary>
        private static readonly Lazy<AutoMapperManager> _instance =
            new Lazy<AutoMapperManager>(() =>
            {
                return new AutoMapperManager();
            });
        public static AutoMapperManager Instance => _instance.Value;

        private MapperConfiguration _config;
        /// <summary>
        /// interface que utilizará as configurações definidas
        /// </summary>
        public IMapper Mapper => _config.CreateMapper();
        private AutoMapperManager()
        {
            _config = new MapperConfiguration((cfg) => 
            {
                cfg.CreateMap<Aluno, AlunoDTO>();
                cfg.CreateMap<AlunoDTO, Aluno>();
            });
        }

    }
}