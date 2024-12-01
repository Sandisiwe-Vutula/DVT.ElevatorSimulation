using DVT.Elevator.Domain.Entities;
using DVT.Elevator.Domain.Enums;
using DVT.Elevator.Services;
using DVT.Elevator.Services.Contracts;
using DVT.Elevator.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton(provider => new Building(10, 20, 10, ElevatorType.Passenger)); //temp
        services.AddTransient<IElevatorService, ElevatorService>();
        services.AddTransient<IBuildingService, BuildingService>();
        services.AddSingleton<ElevatorFactory>();

        services.AddLogging(config =>
        {
            config.AddConsole();
            config.SetMinimumLevel(LogLevel.Information);
        });
    })
    .Build();

var buildingService = host.Services.GetRequiredService<IBuildingService>();
var elevatorService = host.Services.GetRequiredService<IElevatorService>();
var building = host.Services.GetRequiredService<Building>();

await RunElevatorSimulationAsync(elevatorService, buildingService, building);

async Task RunElevatorSimulationAsync(IElevatorService elevatorService, IBuildingService buildingService, Building building)
{
    while (true)
    {
        DisplayStatus(building);

        Console.WriteLine("Enter floor to call elevator or 'exit' to quit:");
        string input = Console.ReadLine()!;

        if (input.ToLower() == "exit") break;

        if (int.TryParse(input, out int floor))
        {
            Console.WriteLine("Enter number of passengers:");
            if (int.TryParse(Console.ReadLine(), out int passengers))
            {
                await elevatorService.CallElevatorAsync(floor, passengers);
            }
        }

        await elevatorService.MoveElevatorsAsync();
        await buildingService.ProcessRequestsAsync();
    }
}

static void DisplayStatus(Building building)
{
    if (building.Elevators == null || !building.Elevators.Any())
    {
        Console.WriteLine("No elevators are currently available.");
        return;
    }

    Console.WriteLine("************************** DVT ELEVATOR CHALLENGE SIMULATION ********************************");
    Console.WriteLine("*********************************************************************************************");
    Console.WriteLine("Elevator Status:");

    foreach (var elevator in building.Elevators)
    {
        Console.WriteLine($"Elevator {elevator.Id} (Type: {elevator.Type}): " +
                          $"Floor {elevator.CurrentFloor}, " +
                          $"Direction {elevator.Direction}, " +
                          $"Passengers {elevator.PassengerCount}, " +
                          $"In Motion: {elevator.IsInMotion}");
    }
    Console.WriteLine();
}

