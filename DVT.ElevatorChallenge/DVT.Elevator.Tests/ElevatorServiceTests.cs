using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVT.Elevator.Domain.Entities;
using DVT.Elevator.Domain.Enums;
using DVT.Elevator.Services.Implementations;
using Microsoft.Extensions.Logging;
using Moq;

namespace DVT.Elevator.Tests
{
    public class ElevatorServiceTests
    {
        private readonly Mock<ILogger<ElevatorService>> _mockLogger;
        private readonly Building _building;

        public ElevatorServiceTests()
        {
            _mockLogger = new Mock<ILogger<ElevatorService>>();
            _building = new Building(10, 2, 10, ElevatorType.Passenger);
        }

        [Fact]
        public async Task MoveElevator_ShouldMoveToTargetFloor()
        {
            // Arrange
            var service = new ElevatorService(_building, _mockLogger.Object);
            var elevator = _building.Elevators[0];
            elevator.CurrentFloor = 1;
            elevator.DestinationFloors.Enqueue(5);

            // Act
            await service.MoveElevatorsAsync();

            // Assert
            Assert.Equal(5, elevator.CurrentFloor);
            Assert.Empty(elevator.DestinationFloors);
        }
    }

}
