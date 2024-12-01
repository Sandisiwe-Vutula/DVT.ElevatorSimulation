# DVT Elevator Simulation Challenge

Welcome to the DVT Elevator Simulation Challenge. This project is a console-based application built with C# that simulates elevator movements in a large building. The system is designed to optimize passenger transportation efficiently while adhering to best practices in Object-Oriented Programming (OOP) principles.

This project demonstrates advanced software engineering techniques, real-time operations, and a well-structured approach to solving complex system challenges.

## Table of Contents
- [Features](#features)
- [Getting Started](#getting-started)
- [Usage Instructions](#usage-instructions)
- [Technical Details](#technical-details)
- [Architecture Overview](#architecture-overview)
- [Testing and Validation](#testing-and-validation)
- [Future Enhancements](#future-enhancements)
- [Contributing](#contributing)

## Features

The application includes the following key functionalities:

- **Real-Time Elevator Status**: Displays the current floor, direction of movement, motion status, and passenger count for each elevator.
- **Interactive Elevator Control**: Allows users to call elevators to specific floors and specify the number of waiting passengers.
- **Multiple Floors and Elevators**: Supports buildings with multiple elevators and floors.
- **Efficient Elevator Dispatching**: Implements algorithms to direct the nearest available elevator efficiently.
- **Passenger Limit Handling**: Prevents overloading and ensures safe elevator operation.
- **Support for Future Elevator Types**: Designed to accommodate different elevator types, such as high-speed and freight elevators.

## Getting Started

To get started with the Elevator Simulation application, follow these steps:

### Prerequisites

Ensure you have the following installed:
- .NET Core SDK
- A code editor (e.g., Visual Studio, VS Code)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Sandisiwe-Vutula/DVT.ElevatorSimulation.git

2. Navigate to the project directory:
   ```bash
   cd DVT.ElevatorSimulation

3. Build the project:
   ```bash
   dotnet build

# Usage Instructions

## Running the Simulation
   To run the application, execute the following command:

   ```bash
   dotnet run

## Interactions

- Select the floor and specify the number of passengers waiting.
- Observe real-time elevator status and movement updates in the console.

## Technical Details
- Language: C#
- Framework: .NET Core, .NET 8.x
- Design Principles: Object-Oriented Programming (OOP), SOLID, Patterns
- Testing: xUnit

## Architecture Overview
This project adheres to Clean Architecture principles to ensure better software design and maintainability. The architecture enables the following:

- Layered Design:
  The solution separates concerns into logical layers: Presentation, Domain, Services, and Infrastructure.
- Modularity: 
  Each layer is modular, making the application easy to extend and maintain.

## Testing and Validation
The solution includes unit tests for the following:

- User input validation.
- Elevator service.
- Building service.

## Future Enhancements
- Additional elevator types (e.g., glass, freight, high-speed).
- Enhanced error handling and logging.

## Contributing
Contributions are to improve this project. To contribute:

- Fork the repository.
- Create a feature branch:
   ```bash
   git checkout -b feature/new-feature

- Commit and push your changes:
   ```bash
   git commit -m "Added new feature"
   git push origin feature/new-feature
-Open a pull request

## Contact Information
Name: Sandisiwe Vutula
Email: sandisiwevutula28@gmail.com
LinkedIn: https://www.linkedin.com/in/sandisiwe-vutula-20421b97/
