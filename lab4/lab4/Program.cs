using System;
using System.Collections.Generic;

// Інтерфейс команди
public interface ICommand
{
    void Execute(Drone drone);
}

// Конкретна команда переміщення
public class MovementCommand : ICommand
{
    private char direction;
    private int distance;

    public MovementCommand(char direction, int distance)
    {
        this.direction = direction;
        this.distance = distance;
    }

    public void Execute(Drone drone)
    {
        drone.Move(direction, distance);
    }
}

// Клас дрона
public class Drone
{
    private int x, y, z;

    public Drone()
    {
        x = y = z = 0;
    }

    public void Move(char direction, int distance)
    {
        switch (direction)
        {
            case 'U':
                z += distance;
                break;
            case 'D':
                z -= distance;
                break;
            case 'F':
                y += distance;
                break;
            case 'B':
                y -= distance;
                break;
            case 'L':
                x -= distance;
                break;
            case 'R':
                x += distance;
                break;
            default:
                throw new ArgumentException("Невідома команда");
        }
    }

    public void PrintPosition()
    {
        Console.WriteLine($"Поточні координати: ({x}, {y}, {z})");
    }
}

// Клас керування дроном
public class DroneController
{
    private List<ICommand> commands = new List<ICommand>();

    public void AddCommand(ICommand command)
    {
        commands.Add(command);
    }

    public void ExecuteCommands(Drone drone)
    {
        foreach (var command in commands)
        {
            command.Execute(drone);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Створення дрона та контролера
        Drone drone = new Drone();
        DroneController controller = new DroneController();

        // Додавання команд
        controller.AddCommand(new MovementCommand('U', 1));
        controller.AddCommand(new MovementCommand('F', 3));
        controller.AddCommand(new MovementCommand('D', 1));

        // Виконання команд
        controller.ExecuteCommands(drone);

        // Виведення позиції дрона після виконання команд
        drone.PrintPosition();
    }
}
