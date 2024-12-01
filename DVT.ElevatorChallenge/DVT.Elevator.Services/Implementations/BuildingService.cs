using System;
using System.Linq;
using System.Threading.Tasks;
using DVT.Elevator.Domain.Entities;
using DVT.Elevator.Domain.Enums;
using DVT.Elevator.Services.Contracts;
using Microsoft.Extensions.Logging;

namespace DVT.Elevator.Services.Implementations
{
    public class BuildingService : IBuildingService
    {
        #region Fields
        private readonly Building _building;
        private readonly IElevatorService _elevatorService;
        private readonly ILogger<BuildingService> _logger;
        private readonly ElevatorFactory _elevatorFactory;
        #endregion

        #region Constructor
        public BuildingService(Building building, IElevatorService elevatorService, ILogger<BuildingService> logger, ElevatorFactory elevatorFactory)
        {
            _building = building ?? throw new ArgumentNullException(nameof(building));
            _elevatorService = elevatorService ?? throw new ArgumentNullException(nameof(elevatorService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _elevatorFactory = elevatorFactory ?? throw new ArgumentNullException(nameof(elevatorFactory));
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the building by creating and adding elevators.
        /// </summary>
        /// <param name="elevatorCount">Number of elevators to create.</param>
        /// <param name="maxCapacity">Maximum capacity of each elevator.</param>
        /// <param name="type">Type of elevator (e.g., Passenger, Freight).</param>
        public void InitializeBuilding(int elevatorCount, int maxCapacity, ElevatorType type)
        {
            for (int i = 1; i <= elevatorCount; i++)
            {
                var elevator = _elevatorFactory.CreateElevator(i, type, maxCapacity);
                _building.Elevators.Add(elevator);
                _logger.LogInformation($"Elevator {elevator.Id} added to the building.");
            }

            _logger.LogInformation("Building initialized successfully.");
        }

        /// <summary>
        /// Processes queued floor requests by assigning them to elevators.
        /// </summary>
        public async Task ProcessRequestsAsync()
        {
            while (_building.FloorRequests.Any())
            {
                var request = _building.FloorRequests.Dequeue();
                _logger.LogInformation($"Processing request for floor {request.RequestedFloor} with {request.Passengers} passengers.");

                await _elevatorService.CallElevatorAsync(request.RequestedFloor, request.Passengers);
            }
        }

        #endregion
    }
}
