using ConsoleFileManager.Commands;
using ConsoleFileManager.Models;
using ConsoleFileManager.Options;
using ConsoleFileManager.Grafics;

public static class Program
{
    //Текущая директория
    public static DirectoryClass CurrentDirectory { get; set; }

    //Текущий файл
    public static FileClass CurrentFile { get; set; }

    //Список команд
    public static IReadOnlyDictionary<string, FileManagerCommand> Commands { get; } = CreateCommands();

    private static Dictionary<string, FileManagerCommand> CreateCommands()
    {
        var help_command = new FileManagerCommandHelp();

        FileManagerCommand[] commands =
        {
            help_command,
            new FileManagerCommandEditUser(),
            new FileManagerCommandPrintDrives(),
            new FileManagerCommandChangeDirectory(),
         //   new FileManagerPrintFilesCommand(),
        };

        var result = commands.ToDictionary(cmd => cmd.CommandName);

        return result;
    }

    public static void Main(string[] args)
    {

        // Приветствие
        Console.WriteLine("Добро пожаловать в файловый менеджер!");

        UserParameters userParameters = new UserParameters();

        if (userParameters.LoadUserParameters())
        {
            Console.WriteLine($"Пользователь {userParameters.UserName} успешно загружен.");
        }
        else
        {
            Console.WriteLine($"Создан новый пользователь {userParameters.UserName}.");
            userParameters.SaveUserParameters();
        }

        Console.WriteLine("Нажмите любую клавишу для продолжения");
        Console.ReadKey();
        Console.Clear();

        bool programStatus = true;

        while (programStatus)
        {
            Console.Clear();

            FileManagerCommandPrintCurrentDirectory pcd =  new FileManagerCommandPrintCurrentDirectory();  
            pcd.CommandExecute();

            Console.Write($"{userParameters.UserName}. Для выхода введите exit. Введите команду > ");

            string command_line = Console.ReadLine();

            if (!Commands.TryGetValue(command_line, out var command))
            {
                if (command_line == "exit")
                {
                    userParameters.SaveUserParameters();
                    programStatus = false;
                }
                else
                    Console.WriteLine($"Неизвестная команда {command_line}. Для помощи напишите help");
            }
            else
            {
                command.CommandExecute();
            }

            Console.ReadKey();
        }

        Console.WriteLine("Программа завершена.");
    }
}