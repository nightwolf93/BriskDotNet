using System;

namespace BriskDotNet.Exceptions
{
    public class BriskUnauthorizedException : Exception
    {
        public BriskUnauthorizedException() : base("Wrong credentials given or your pair is not allowed for this") { }
    }
}