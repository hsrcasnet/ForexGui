using System;

namespace Forex.Service.Services.Exceptions
{
    public class QuoteRequestException : Exception
    {
        public QuoteRequestException(int status, string error) : base(error)
        {
            this.Status = status;
        }

        public int Status { get; }
    }
}