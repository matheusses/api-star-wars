using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Matheusses.StarWars.WebApi.Filters
{
    public class HttpResultActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var objectResult = (ObjectResult)context.Result;
            dynamic responseValue = objectResult;

            if (responseValue == null) return;

            if (responseValue.StatusCode != null && responseValue.Value != null){

                switch (responseValue.Value.HttpStatusCode){
                    case HttpStatusCode.BadRequest:
                        context.Result = new BadRequestObjectResult(responseValue.Value);
                        break;
                    case HttpStatusCode.NotFound:
                        context.Result = new NotFoundObjectResult(responseValue.Value);
                        break;
                    default:
                        context.Result = new ObjectResult(responseValue.Value)
                                            {
                                                StatusCode = (int?)responseValue.Value.HttpStatusCode
                                            };
                        break;
                }
                return;
            }
        }
    }
}
