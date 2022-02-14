using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;


//Класс для создания и хранения пользовательских данных
namespace ConsoleFileManager.Options
{
    public class UserParameters
    {
        static public string PathToSettings = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\ConsoleFileManager\Settings\appsettings.json";
        static public string PathToErrors = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\ConsoleFileManager\Errors";

        //Свойства класса
        public string UserName { get; set; }

        public string LastPathToDirectory { get; set; }

        public int FilesAndDirScale { get; set; }

        public int CurrentPage { get; set; }

        /// <summary>
        /// Конструктор пользовательских настроек
        /// </summary>
        /// <param Имя пользователя="_userName"></param>
        /// <param Последний путь до каталога="_lastPathToDirectory"></param>
        /// <param Количество строк для вывода на экран="_filesAndDirScale"></param>
        /// <param Текущая страница="_currentPage"></param>
        public UserParameters(string _userName, string _lastPathToDirectory, int _filesAndDirScale, int _currentPage)
        {
            UserName = _userName;
            LastPathToDirectory = _lastPathToDirectory;
            FilesAndDirScale = _filesAndDirScale;
            CurrentPage = _currentPage;
        }

        /// <summary>
        /// Конструктор пользовательских настроек по дефолту
        /// </summary>
        public UserParameters()
        {
            UserName = Environment.MachineName;
            LastPathToDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            FilesAndDirScale = 30;
            CurrentPage = 1;
        }

        /// <summary>
        /// Сохраняет отловленные ошибки в ходе работы пользователя с приложением. В названии - тип ошибки и дата, в файле - текст ошибки
        /// </summary>
        /// <param Исключение="e"></param>
        public void SaveUserErrors(Exception e)
        {
            if (!File.Exists(PathToErrors))
                Directory.CreateDirectory(PathToErrors);

            string errorName = $"{PathToErrors}\\{e.GetType().ToString()}-{DateTime.Today.ToShortTimeString()}.txt";
            File.Create(errorName).Close();
            File.WriteAllText(errorName, e.Message.ToString());
        }

        /// <summary>
        /// Сохраняет настройки пользователя в файл.
        /// </summary>
        /// <param name="_filePath"></param>
        public void SaveUserParameters()
        {
            if (!File.Exists(PathToSettings))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\ConsoleFileManager\Settings");
                File.Create(PathToSettings).Close();
            }

            string jsonser = JsonSerializer.Serialize(this);
            File.WriteAllText(PathToSettings, jsonser);
        }

        /// <summary>
        /// Загружает пользовательские настройки из файла
        /// </summary>
        /// <returns></returns>
        public bool LoadUserParameters()
        {
            if (File.Exists(PathToSettings))
            {
                //Проверка на "битые" настройки. В случае их неисправности, файл удаляется.
                try
                {
                    //если пользовательский файл существует, то считываем настройки из него
                    string str = File.ReadAllText(PathToSettings);
                    UserParameters userparamFromFile = JsonSerializer.Deserialize<UserParameters>(str);
                    UserName = userparamFromFile.UserName;
                    LastPathToDirectory = userparamFromFile.LastPathToDirectory;
                    FilesAndDirScale = userparamFromFile.FilesAndDirScale;
                    CurrentPage = userparamFromFile.CurrentPage;
                    return true;
                }
                catch (Exception ex)
                {
                    SaveUserErrors(ex);
                    File.Delete(PathToSettings);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"Пользователь: {UserName} \nТекущая директория: {LastPathToDirectory} \nРазмер страницы {FilesAndDirScale} \nТекущая страница {CurrentPage}";
        }       
    }
}