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
        internal Exception Exception { get; private set; }
        public T Data{ get; private set; }

        internal Result()
        {
            Errors = new List<string>(1);
        }

        private void AddErrors(
            string mensagem, 
            List<string> errors, 
            Exception ex, 
            HttpStatusCode statusCode)
        {
            Message = mensagem;
            Log.Error(mensagem);
            Exception = ex;
            if (ex != null)
                Log.Error(ex.ToString());
            HttpStatusCode = statusCode;
            Success = false;
            if (errors != null)
                Errors.AddRange(errors);
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
            Exception ex = null,
            HttpStatusCode statusCode = HttpStatusCode.BadRequest
            )
        {
            AddErrors(mensagem, errors, ex, statusCode);
            return this;
        }


        public Result<T> WithError(
            string errorMessage, 
            Exception ex = null, 
            HttpStatusCode statusCode = HttpStatusCode.BadRequest
        )
        {
            AddErrors(errorMessage, null, ex, statusCode);            
            return this;
        }

        public Result<T>  WithException(string errorMessage, Exception ex = null)
        {
            AddErrors(errorMessage, new List<string>{errorMessage}, ex, HttpStatusCode.InternalServerError);
            return this;
        }

    }

    public sealed partial class ResultFactory<T>{        
	    public static Result<T> Create()  => new Result<T>();
    }
}