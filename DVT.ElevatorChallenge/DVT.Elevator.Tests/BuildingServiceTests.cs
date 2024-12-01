using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVT.Elevator.Domain.Entities;
using DVT.Elevator.Services.Contracts;
using DVT.Elevator.Services.Implementations;
using DVT.Elevator.Services;
using Microsoft.Extensions.Logging;
using Moq;
using DVT.Elevator.Domain.Enums;

namespace DVT.Elevator.Tests
{
    public class BuildingServiceTests
    {
        private readonly Mock<ILogger<BuildingService>> _mockLogger;
        private readonly Mock<IElevatorService> _mockElevatorService;
        private readonly ElevatorFactory _factory;
        private readonly Building _building;

        public BuildingServiceTests()
        {
            _mockLogger = new Mock<ILogger<BuildingService>>();
            _mockElevatorService = new Mock<IElevatorService>();
            _factory = new ElevatorFactory();
            _building = new Building(10, 0, 10, ElevatorType.Passenger);
        }

        [Fact]
        public void InitializeBuilding_ShouldAddElevatorsToBuilding()
        {
            // Arrange
            var service = new BuildingService(_building, _mockElevatorService.Object, _mockLogger.Object, _factory);

            // Act
            service.InitializeBuilding(2, 10, ElevatorType.Passenger);

            // Assert
            Assert.Equal(2, _building.Elevators.Count);
            Assert.All(_building.Elevators, e => Assert.Equal(10, e.MaxCapacity));
        }

        [Fact]
        public async Task ProcessRequests_ShouldInvokeElevatorService()
        {
            // Arrange
            var service = new BuildingService(_building, _mockElevatorService.Object, _mockLogger.Object, _factory);
            _building.FloorRequests.Enqueue(new FloorRequest(5, 5));

            // Act
            await service.ProcessRequestsAsync();

            // Assert
            _mockElevatorService.Verify(es => es.CallElevatorAsync(5, 5), Times.Once);
        }
    }

}
