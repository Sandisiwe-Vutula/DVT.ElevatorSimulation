using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVT.Elevator.Domain.Enums;

namespace DVT.Elevator.Domain.Entities
{
    public class Building
    {
        public int NumberOfFloors { get; set; }
        public List<Elevator> Elevators { get; set; }
        public Queue<FloorRequest> FloorRequests { get; set; }


        public Building(int totalFloors, int numberOfElevators, int maxCapacity, ElevatorType elevatorType)
        {
            NumberOfFloors = totalFloors;
            Elevators = new List<Elevator>();

            for (int i = 0; i < numberOfElevators; i++)
            {
                Elevators.Add(new Elevator(i + 1, elevatorType, maxCapacity));
            }

            FloorRequests = new Queue<FloorRequest>();
        }
    }
}
