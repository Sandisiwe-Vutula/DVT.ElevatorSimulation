using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevator.Domain.Entities
{
    public class FloorRequest
    {
        public int RequestedFloor { get; set; }
        public int Passengers { get; set; }

        public FloorRequest(int requestedFloor, int passengers)
        {
            RequestedFloor = requestedFloor;
            Passengers = passengers;
        }
    }
}
