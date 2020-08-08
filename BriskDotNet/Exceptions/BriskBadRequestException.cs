using System;

namespace BriskDotNet.Exceptions
{
    public class BriskBadRequestException : Exception
    {
        public BriskBadRequestException(string possibleErrors) : base("The server has reject the request : " + possibleErrors) { }
    }
}