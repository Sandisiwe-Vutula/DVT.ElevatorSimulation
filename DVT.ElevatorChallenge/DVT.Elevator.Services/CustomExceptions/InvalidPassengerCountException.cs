using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevator.Services.CustomExceptions
{
    public class InvalidPassengerCountException : Exception
    {
        public InvalidPassengerCountException(string message) : base(message)
        {
        }
    }
}
