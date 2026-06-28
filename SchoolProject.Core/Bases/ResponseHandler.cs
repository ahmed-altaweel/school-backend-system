using System.Net;

namespace SchoolProject.Core.Bases
{
    public class ResponseHandler
    {
        public ResponseHandler() { }
        public Response<T> Deleted<T>()
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = "Deleted Successfuly"

            };
        }

        public Response<T> Success<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = "Successfuly",
                Meta = Meta
            };
        }

        public Response<T> Unauthorized<T>()
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Succeeded = false,
                Message = "UnAuthorized"
            };
        }

        public Response<T> BadRequest<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = message == null ? "Bad Reqeust" : message
            };
        }

        public Response<T> UnprocessableEntity<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = message == null ? "Bad Reqeust" : message
            };
        }

        public Response<T> NotFound<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message == null ? "Not Found" : message
            };
        }


        public Response<T> Created<T>(T entity, object meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = HttpStatusCode.Created,
                Succeeded = true,
                Message = "Created",
                Meta = meta
            };
        }

    }
}
