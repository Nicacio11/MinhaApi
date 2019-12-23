using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Nicacio.MinhaApi.Api.Filters
{
    /// <summary>
    /// utilizado para fazer a validação do model state
    /// </summary>
    public class ApplyModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request
                    .CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, actionContext.ModelState);
            }
            //base.OnActionExecuting(actionContext);
        }
    }
}