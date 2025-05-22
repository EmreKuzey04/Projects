using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ResponseWrappers
{
    public class Response<T>where T : class
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }

        public Response(T data)
        {
            Succeeded = true;
            Data = data;
            Errors = null;
        }

        public Response(List<string>errors)
        {
            Succeeded= false;
            Data = null;
            Errors = errors;
        }
    }
}
