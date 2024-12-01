using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DVT.Elevator.Domain.Enums;

namespace DVT.Elevator.Services.Contracts
{
    /// <summary>
    /// Interface defining the operations for managing a building with elevators.
    /// </summary>
    public interface IBuildingService
    {
        /// <summary>
        /// Initializes the building with a specified number of elevators, their capacity, and type.
        /// </summary>
        /// <param name="elevatorCount">The number of elevators to initialize.</param>
        /// <param name="maxCapacity">The maximum capacity of each elevator.</param>
        /// <param name="type">The type of elevators to initialize.</param>
        void InitializeBuilding(int elevatorCount, int maxCapacity, ElevatorType type);

        /// <summary>
        /// Processes pending floor requests by assigning them to available elevators.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task ProcessRequestsAsync();
    }
}
