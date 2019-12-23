using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nicacio.MinhaApi.Api.HATEOAS
{
    public class RestLink
    {
        /// <summary>
        /// Vai conter a intenção do link que foi atribuido, deletar atualizar, etc
        /// </summary>
        public string Rel { get; set; }

        /// <summary>
        /// É a rota, o serviço
        /// </summary>
        public string Href { get; set; }
    }
}