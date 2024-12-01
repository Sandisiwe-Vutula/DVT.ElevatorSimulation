using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVT.Elevator.Domain.Enums;

namespace DVT.Elevator.Domain.Entities
{
    public class Elevator
    {
        public int Id { get; }
        public ElevatorType Type { get; }
        public int CurrentFloor { get; set; }
        public int MaxCapacity { get; }
        public int PassengerCount { get; set; }
        public bool IsInMotion { get; set; }
        public Queue<int> DestinationFloors { get; } = new Queue<int>();

        public ElevatorDirection Direction { get; set; } = ElevatorDirection.Stationary; //Default

        public Elevator(int id, ElevatorType type, int maxCapacity)
        {
            Id = id;
            Type = type;
            MaxCapacity = maxCapacity;
            CurrentFloor = 1; //Default starting floor
            PassengerCount = 0;
            IsInMotion = false;
        }
    }
}


