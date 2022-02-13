using System;
using System.Collections.Generic;
using System.Text;


//Класс для создания и хранения пользовательских данных
namespace ConsoleFileManager.UserParameters
{
    public class UserParameters
    {
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
    }
}