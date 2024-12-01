using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevator.Services.CustomExceptions
{
    public class RequestProcessingException : Exception
    {
        public RequestProcessingException(string message) : base(message)
        {
        }
    }
}
