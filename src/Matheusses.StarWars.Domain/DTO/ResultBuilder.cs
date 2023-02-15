using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Serilog;

namespace Matheusses.StarWars.Domain.DTO
{
    public sealed partial class Result<T> 
    {
        public bool Success { get; private set; }
        public List<string> Errors { get; private set;}
        public HttpStatusCode HttpStatusCode { get; private set; }
        public string Message { get; private set; }
        public T Data{ get; private set; }

        internal Result()
        {
            Errors = new List<string>(1);
        }

        private void AddErrors(
            string mensagem, 
            List<string> errors, 
            HttpStatusCode statusCode)
        {
            Message = mensagem;
            Log.Error(mensagem);
            HttpStatusCode = statusCode;
            Success = false;
            if (errors != null)
                Errors.AddRange(errors);
            else
                Errors.Add(mensagem);
        }

        public Result<T> WithSuccess(T data)
        {
            Data = data;
            Success = true;
            HttpStatusCode = HttpStatusCode.OK;
            return this;
        }

        public Result<T>  WithErrors(
            string mensagem,
            List <string> errors, 
            HttpStatusCode statusCode = HttpStatusCode.BadRequest
            )
        {
            AddErrors(mensagem, errors, statusCode);
            return this;
        }


        public Result<T> WithError(
            string errorMessage, 
            HttpStatusCode statusCode = HttpStatusCode.BadRequest
        )
        {
            AddErrors(errorMessage, null, statusCode);            
            return this;
        }

        public Result<T>  WithException(string errorMessage)
        {
            AddErrors(errorMessage, null, HttpStatusCode.InternalServerError);
            return this;
        }

    }

    public sealed partial class ResultFactory<T>{        
	    public static Result<T> Create()  => new Result<T>();
    }
}