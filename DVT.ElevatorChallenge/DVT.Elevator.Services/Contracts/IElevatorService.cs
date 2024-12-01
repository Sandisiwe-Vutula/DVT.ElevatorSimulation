using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVT.Elevator.Services.Validations;

namespace DVT.Elevator.Services.Contracts
{
    /// <summary>
    /// Interface defining the operations for managing elevator functionalities.
    /// </summary>
    public interface IElevatorService
    {
        /// <summary>
        /// Validates a request for calling an elevator to a specific floor with a number of passengers.
        /// </summary>
        /// <param name="floor">The floor to which the elevator is being requested.</param>
        /// <param name="passengers">The number of passengers for the request.</param>
        /// <returns>
        /// A <see cref="ValidationResult"/> indicating whether the request is valid 
        /// and, if not, providing the reason for failure.
        /// </returns>
        ValidationResult ValidateRequest(int floor, int passengers);

        /// <summary>
        /// Assigns the best available elevator to the specified floor and passengers.
        /// </summary>
        /// <param name="floor">The floor to which the elevator is being called.</param>
        /// <param name="passengers">The number of passengers making the request.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CallElevatorAsync(int floor, int passengers);

        /// <summary>
        /// Moves all elevators to their respective destination floors.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task MoveElevatorsAsync();
    }
}
