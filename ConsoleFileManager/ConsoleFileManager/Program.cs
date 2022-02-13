using ConsoleFileManager.Commands;
using ConsoleFileManager.Models;
using ConsoleFileManager.UserParameters;
using System.Text;
using System.Text.Json;

public static class Program
{
    //Дефолтные установки приложения.
    static UserParameters userparam = new UserParameters();

    //Пути до пользовательского каталога с настройками и ошибками
    static StringBuilder pathToErrors = new StringBuilder();
    static StringBuilder pathToSettings = new StringBuilder();

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
          //  new FileManagerPrintDirectoriesCommand(),
          //  new FileManagerPrintDrivesCommand(),
         //   new FileManagerPrintFilesCommand(),
        };

        var result = commands.ToDictionary(cmd => cmd.CommandName);

        return result;
    }

    public static void Main(string[] args)
    {

        // Приветствие
        Console.WriteLine("Добро пожаловать в файловый менеджер!");
        Console.WriteLine("Нажмите любую клавишу для продолжения");
        Console.ReadKey();

        //Построение пути до пользовательских настроек     
        pathToSettings.Append(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
        pathToSettings.Append(@"\ConsoleFileManager\Settings");
        string filePath = pathToSettings.ToString() + @"\appsettings.json";

        pathToErrors.Append(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
        pathToErrors.Append(@"\ConsoleFileManager\Errors");
        Directory.CreateDirectory(pathToErrors.ToString());

        if (File.Exists(filePath))
        {
            //Проверка на "битые" настройки. В случае их неисправности, файл удаляется, настройки создаются "дефолтными"
            try
            {
                //если пользовательский файл существует, то считываем настройки из него и заменяем дефолтные
                string str = File.ReadAllText(filePath);
                UserParameters userparamFromFile = JsonSerializer.Deserialize<UserParameters>(str);
                userparam.UserName = userparamFromFile.UserName;
                userparam.LastPathToDirectory = userparamFromFile.LastPathToDirectory;
                userparam.FilesAndDirScale = userparamFromFile.FilesAndDirScale;
                userparam.CurrentPage = userparamFromFile.CurrentPage;
            }
            catch (Exception ex)
            {
                SaveErrors(ex);
                File.Delete(filePath);
                userparam = new UserParameters();
            }
        }
        else
        {
            //если пользовательского файла не существует, дефолтные сохраняем в файл.
            Directory.CreateDirectory(pathToSettings.ToString());
            File.Create(filePath).Close();
            string jsonser = JsonSerializer.Serialize(userparam);
            File.WriteAllText(filePath, jsonser);
        }

        //Отрисовка файлового менеджера с каталогами
        bool programStatus = true;
        while (programStatus)
        {
            //Очитска консоли перед выводом актуальных данных
            Console.Clear();

            // вывод на экран текущей директории
            Console.WriteLine(userparam.LastPathToDirectory + @"\");
            // MenuDrawings.DrawHorizontalLine();

            //получение списка файлов и папок, включая вложенные, на основании параметров пользователя
            //    string[] listOfFilesDir = FilesAndDirectories.GetDirectories(userparam.LastPathToDirectory);

            //вывод страницы каталога
            //  FilesAndDirectories.ShowPage(userparam.FilesAndDirScale, userparam.CurrentPage, listOfFilesDir);

            //   MenuDrawings.DrawHorizontalLine();

            Console.Write("Введите команду: ");

            string userCommand = Console.ReadLine();

            // ParseUserCommand(userCommand);

            //Сохранение пользовательских настроек после каждой отрисовки поля
            SaveUserOptions(filePath);
        }

        bool showMenu = true;

        while (showMenu)
        {
            Console.Write("Введите команду > ");

            string command_line = Console.ReadLine();

            if (!Commands.TryGetValue(command_line, out var command))
            {
                Console.WriteLine("Неизвестная команда {0}. Для помощи напишите help", command_line);
            }
            else
            {
                command.CommandExecute();
            }
        }

        Console.WriteLine("Программа завершена");
    }

    //Метод для сохранения пользовательских настроек в файл
    public static void SaveUserOptions(string _filePath)
    {
        string jsonser = JsonSerializer.Serialize(userparam);
        File.WriteAllText(_filePath, jsonser);
    }

    //Метод сохранения отловленных ошибок. В названии - тип ошибки и дата, в файле - текст ошибки
    public static void SaveErrors(Exception e)
    {
        string errorName = $"{pathToErrors}\\{e.GetType().ToString()}-{DateTime.Today.ToShortTimeString()}.txt";
        File.Create(errorName).Close();
        File.WriteAllText(errorName, e.Message.ToString());
    }

}