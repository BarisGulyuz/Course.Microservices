using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FreeCourse.Shared
{
    /// <summary>
    /// Use for retun info from API
    /// Use Success or Fail Factory Methods for create return new Response obj
    /// </summary>
    public class Response<T>
    {
        private Response()
        {

        }
        private Response(T data, int statusCode, List<string> messages, bool isSuccess = true)
        {
            Data = data;
            StatusCode = statusCode;
            Messages = messages;
            IsSucess = isSuccess;
        }
        public T Data { get; private set; }
        [JsonIgnore]
        public int StatusCode { get; private set; }
        public bool IsSucess { get; private set; }

        public List<string> Messages { get; private set; }

        public static Response<T> Success(int statusCode)
        {
            return new Response<T>(default(T), statusCode, null);
        }
        public static Response<T> Success(int statusCode, string message)
        {
            return new Response<T>(default(T), statusCode, new List<string> { message });
        }
        public static Response<T> Success(int statusCode, List<string> messages)
        {
            return new Response<T>(default(T), statusCode, messages);
        }

        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T>(data, statusCode, null);
        }

        public static Response<T> Success(T data, int statusCode, string message = null)
        {
            return new Response<T>(data, statusCode, new List<string> { message });
        }

        public static Response<T> Success(T data, int statusCode, List<string> messages = null)
        {
            return new Response<T>(data, statusCode, messages);
        }

        public static Response<T> Fail(List<string> errors, int statusCode)
        {
            return new Response<T>(default(T), statusCode, errors, false);
        }

        public static Response<T> Fail(string error, int statusCode)
        {
            return new Response<T>(default(T), statusCode, new List<string> { error }, false);
        }

    }
}
