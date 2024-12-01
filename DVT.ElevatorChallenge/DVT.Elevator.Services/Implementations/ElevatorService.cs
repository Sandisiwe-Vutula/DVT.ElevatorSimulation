﻿using System;
using System.Linq;
using System.Threading.Tasks;
using DVT.Elevator.Domain.Entities;
using DVT.Elevator.Domain.Enums;
using DVT.Elevator.Services.Contracts;
using DVT.Elevator.Services.Validations;
using Microsoft.Extensions.Logging;

namespace DVT.Elevator.Services.Implementations
{
    public class ElevatorService : IElevatorService
    {
        private readonly Building _building;
        private readonly ILogger<ElevatorService> _logger;

        #region Constructor
        public ElevatorService(Building building, ILogger<ElevatorService> logger)
        {
            _building = building ?? throw new ArgumentNullException(nameof(building));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Validates the elevator request based on floor and passenger constraints.
        /// </summary>
        public ValidationResult ValidateRequest(int floor, int passengers)
        {
            if (floor < 1 || floor > _building.NumberOfFloors)
            {
                return ValidationResult.Failure($"Floor {floor} is out of range. Valid range is 1 to {_building.NumberOfFloors}.");
            }

            if (passengers <= 0)
            {
                return ValidationResult.Failure("Number of passengers must be greater than zero.");
            }

            if (!_building.Elevators.Any(e => e.PassengerCount + passengers <= e.MaxCapacity))
            {
                return ValidationResult.Failure("No elevators have enough capacity for the requested passengers.");
            }

            return ValidationResult.Success();
        }

        /// <summary>
        /// Handles the process of calling an elevator to a specific floor for a set of passengers.
        /// </summary>
        public async Task CallElevatorAsync(int floor, int passengers)
        {
            var validationResult = ValidateRequest(floor, passengers);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning($"Validation failed: {validationResult.ErrorMessage}");
                return;
            }

            var bestElevator = GetBestAvailableElevator(floor, passengers);
            if (bestElevator != null)
            {
                AssignElevatorToRequest(bestElevator, floor, passengers);
                await MoveElevatorAsync(bestElevator);
            }
            else
            {
                _logger.LogInformation($"No elevator available for floor {floor}. Request added to queue.");
                _building.FloorRequests.Enqueue(new FloorRequest(floor, passengers));
            }
        }

        /// <summary>
        /// Processes all elevators to handle queued destinations asynchronously.
        /// </summary>
        public async Task MoveElevatorsAsync()
        {
            var tasks = _building.Elevators
                .Where(e => e.DestinationFloors.Any())
                .Select(MoveElevatorAsync);

            await Task.WhenAll(tasks);
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Moves a specific elevator to its queued destinations one at a time.
        /// </summary>
        private async Task MoveElevatorAsync(Domain.Entities.Elevator elevator)
        {
            while (elevator.DestinationFloors.Any())
            {
                elevator.IsInMotion = true;
                var targetFloor = elevator.DestinationFloors.Dequeue();

                _logger.LogInformation($"Elevator {elevator.Id} starting to move from floor {elevator.CurrentFloor} to floor {targetFloor}.");

                while (elevator.CurrentFloor != targetFloor)
                {
                    elevator.Direction = elevator.CurrentFloor < targetFloor ? ElevatorDirection.Up : ElevatorDirection.Down;

                    // Moves elevator by one floor
                    elevator.CurrentFloor += elevator.CurrentFloor < targetFloor ? 1 : -1;

                    _logger.LogInformation($"Elevator {elevator.Id} is now at floor {elevator.CurrentFloor}.");
                    await Task.Delay(1000);
                }

                _logger.LogInformation($"Elevator {elevator.Id} reached destination floor {elevator.CurrentFloor}.");
            }

            elevator.IsInMotion = false;
        }

        /// <summary>
        /// Finds the best available elevator for a given request based on proximity, capacity, and motion status.
        /// </summary>
        private Domain.Entities.Elevator? GetBestAvailableElevator(int floor, int passengers)
        {
            return _building.Elevators
                .Where(e => e.PassengerCount + passengers <= e.MaxCapacity)
                .OrderBy(e => e.IsInMotion)
                .ThenBy(e => Math.Abs(e.CurrentFloor - floor)) // Nearest elevators have priority
                .ThenBy(e => e.Direction == ElevatorDirection.Stationary ? 0 : 1) 
                .ThenBy(e => e.PassengerCount) 
                .FirstOrDefault();
        }


        /// <summary>
        /// Assigns an elevator to handle a request by adding the destination floor and updating its status.
        /// </summary>
        private void AssignElevatorToRequest(Domain.Entities.Elevator elevator, int floor, int passengers)
        {
            if (!elevator.DestinationFloors.Contains(floor))
            {
                elevator.DestinationFloors.Enqueue(floor);
            }

            elevator.PassengerCount += passengers;
            elevator.IsInMotion = true;

            _logger.LogInformation($"Assigned elevator {elevator.Id} to floor {floor} for {passengers} passengers.");
        }
        #endregion
    }
}
