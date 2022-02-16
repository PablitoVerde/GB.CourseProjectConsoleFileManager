using ConsoleFileManager.Options;

namespace ConsoleFileManager.Commands
{
    public class FileManagerCommandEditUser : FileManagerCommand
    {
        public FileManagerCommandEditUser()
        {
            CommandName = "user";
            CommandDescription = "Изменить данные пользователя";
        }



        public override void CommandExecute()
        {
            Console.WriteLine("Вы вошли в режим редактирования пользователя.");

            UserParameters userParameters = new UserParameters();

            if (userParameters.LoadUserParameters())
            {
                bool showmenu = true;
                while (showmenu)
                {
                    Console.WriteLine(userParameters.ToString());

                    Console.WriteLine("Какие данные будем менять?");
                    Console.WriteLine("1. Имя \n2. Размер страницы \nВведите exit для выхода из меню");

                    string command = Console.ReadLine();

                    switch (command)
                    {
                        case "1":
                            Console.WriteLine("Введите новое имя пользователя: ");
                            userParameters.UserName = Console.ReadLine();
                            break;
                        case "2":
                            Console.WriteLine("Введите новый размер страницы");
                            try
                            {
                                userParameters.FilesAndDirScale = Convert.ToInt32(Console.ReadLine());
                                userParameters.CurrentPage = 1;
                                userParameters.LastPathToDirectory= Directory.GetCurrentDirectory();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Параметр введен неправильно.");
                                userParameters.SaveUserErrors(ex);
                            }
                            break;
                        case "exit":
                            userParameters.SaveUserParameters();
                            showmenu = false;
                            Console.WriteLine("Настройки сохранены. Для их активации перезапустите приложение.");
                            break;
                        default:
                            Console.WriteLine("Введена несуществующая команда. Попробуйте еще раз.");
                            break;
                    }
                }
            }

        }
    }
}
