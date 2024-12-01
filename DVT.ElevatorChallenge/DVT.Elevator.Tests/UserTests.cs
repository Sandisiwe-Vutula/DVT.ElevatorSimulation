using Xunit;
using Moq;
using DVT.Elevator.Services.Implementations;
using DVT.Elevator.Domain.Entities;
using Microsoft.Extensions.Logging;
using DVT.Elevator.Domain.Enums;

namespace DVT.Elevator.Tests
{
    public class UserTests
    {
        private readonly Mock<ILogger<ElevatorService>> _mockLogger;
        private readonly Building _building;

        public UserTests()
        {
            _mockLogger = new Mock<ILogger<ElevatorService>>();
            _building = new Building(10, 2, 10, ElevatorType.Passenger);
        }

        [Fact]
        public void ValidateRequest_ShouldFail_WhenFloorIsBelowOne()
        {
            // Arrange
            var service = new ElevatorService(_building, _mockLogger.Object);

            // Act
            var result = service.ValidateRequest(0, 5);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("out of range", result.ErrorMessage);
        }

        [Fact]
        public void ValidateRequest_ShouldFail_WhenFloorExceedsBuildingFloors()
        {
            // Arrange
            var service = new ElevatorService(_building, _mockLogger.Object);

            // Act
            var result = service.ValidateRequest(11, 5);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("out of range", result.ErrorMessage);
        }

        [Fact]
        public void ValidateRequest_ShouldFail_WhenPassengerCountIsZeroOrLess()
        {
            // Arrange
            var service = new ElevatorService(_building, _mockLogger.Object);

            // Act
            var result = service.ValidateRequest(5, 0);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("must be greater than zero", result.ErrorMessage);
        }

        [Fact]
        public void ValidateRequest_ShouldFail_WhenNoElevatorHasCapacity()
        {
            // Arrange
            _building.Elevators[0].PassengerCount = 10;
            var service = new ElevatorService(_building, _mockLogger.Object);

            // Act
            var result = service.ValidateRequest(5, 15);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("No elevators have enough capacity", result.ErrorMessage);
        }

        [Fact]
        public void ValidateRequest_ShouldPass_WhenInputsAreValid()
        {
            // Arrange
            var service = new ElevatorService(_building, _mockLogger.Object);

            // Act
            var result = service.ValidateRequest(5, 5);

            // Assert
            Assert.True(result.IsValid);
        }
    }

}