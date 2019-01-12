using System;

namespace Business.CustomExceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message)
        {
        }
    }
}
