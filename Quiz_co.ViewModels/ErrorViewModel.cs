using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Quiz_co.ViewModels
{
    public class ErrorViewModel
    {
        public string Message { get; }
        public int StatusCode { get; }

        public ErrorViewModel(string message, HttpStatusCode statusCode)
        {
            Message = message;
            StatusCode = (int)statusCode;
        }
    }
}
