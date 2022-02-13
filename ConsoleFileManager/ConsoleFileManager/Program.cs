using ConsoleFileManager.Commands;
using ConsoleFileManager.Models;
using ConsoleFileManager.UserParameters;

namespace ConsoleFileManager;

public static class Program
{
    public static DirectoryClass CurrentDirectory { get; set; }
    public static FileClass CurrentFile { get; set; }

    public static IReadOnlyDictionary<string, FileManagerCommand> Commands { get; } = CreateCommands();

    private static Dictionary<string, FileManagerCommand> CreateCommands()
    {
        var help_command = new FileManagerCommandHelp();
        
        FileManagerCommand[] commands =
        {
            help_command,
            new FileManagerPrintDirectoriesCommand(),
            new FileManagerPrintDrivesCommand(),
            new FileManagerPrintFilesCommand(),
        };

        var result = commands.ToDictionary(cmd => cmd.Name);

        return result;
    }

    public static void Main(string[] args)
    {

        // Приветствие
        Console.WriteLine("Добро пожаловать в файловый менеджер!");
        Console.WriteLine("Нажмите любую клавишу для продолжения");
        Console.ReadKey();

        bool showMenu = true;

        while (showMenu)
        {
            Console.Clear();

            Console.Write("Введите команду >");

            string command_line = Console.ReadLine();

            if (!Commands.TryGetValue(command_line, out var command))
            {
                Console.WriteLine("Неизвестная команда {0}. Для помощи напишите help", command_line);
            }
            else
            {
                command.Execute();
            }
        }

        Console.WriteLine("Программа завершена");
    }
}