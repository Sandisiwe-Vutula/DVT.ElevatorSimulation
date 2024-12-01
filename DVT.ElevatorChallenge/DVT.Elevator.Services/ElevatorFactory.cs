using System;
using DVT.Elevator.Domain.Enums;

namespace DVT.Elevator.Services
{
    /// <summary>
    /// Factory class responsible for creating Elevator instances based on specified parameters.
    /// </summary>
    public class ElevatorFactory
    {
        #region Public Methods

        /// <summary>
        /// Creates an elevator based on the provided type, ID, and maximum capacity.
        /// </summary>
        /// <param name="id">The unique identifier of the elevator.</param>
        /// <param name="type">The type of elevator to create (e.g., Passenger, Freight).</param>
        /// <param name="maxCapacity">The maximum passenger capacity of the elevator.</param>
        /// <returns>A new instance of the <see cref="Domain.Entities.Elevator"/> class.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when an unknown elevator type is specified.</exception>
        public Domain.Entities.Elevator CreateElevator(int id, ElevatorType type, int maxCapacity)
        {
            return type switch
            {
                ElevatorType.Passenger => new Domain.Entities.Elevator(id, ElevatorType.Passenger, maxCapacity),
                ElevatorType.Freight => new Domain.Entities.Elevator(id, ElevatorType.Freight, maxCapacity),
                ElevatorType.HighSpeed => new Domain.Entities.Elevator(id, ElevatorType.HighSpeed, maxCapacity),
                ElevatorType.Glass => new Domain.Entities.Elevator(id, ElevatorType.Glass, maxCapacity),
                _ => throw new ArgumentOutOfRangeException(nameof(type), "Unknown elevator type")
            };
        }

        #endregion
    }
}
