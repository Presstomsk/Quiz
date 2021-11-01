using System;


namespace Quiz
{   
    
    public static class Messages
    {
        public static void AuthRegTextMenu()
        {
            Console.WriteLine(">>>>>>>>>> ВИКТОРИНА <<<<<<<<<<");
            Console.WriteLine("1 - Авторизация");
            Console.WriteLine("2 - Регистрация");
        }

        public static void AuthorizationText()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("АВТОРИЗАЦИЯ");
            Console.ResetColor();
        }

        public static void RegistrationText()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("РЕГИСТРАЦИЯ");
            Console.ResetColor();
        }

        public static string Login()
        {
            Console.Write("Login : ");
            return Console.ReadLine();
        }

        public static string Password()
        {
            Console.Write("Password : ");
            return Console.ReadLine();
        }
        public static string Date_of_birth()
        {
            Console.Write("Дата рождения: ");
            return Console.ReadLine();
        }
        public static void MainTextAcc(User user)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"ЛИЧНЫЙ КАБИНЕТ ПОЛЬЗОВАТЕЛЯ {user.Login}");
            Console.ResetColor();
        }
        public static void AccTextMenu(User user)
        {
            MainTextAcc(user);
            Console.WriteLine("1 - Начать новую викторину");
            Console.WriteLine("2 - Посмотреть результаты своих прошлых викторин");
            Console.WriteLine("3 - Посмотреть Топ-20 по конкретной викторине");
            Console.WriteLine("4 - Изменить настройки");
            Console.WriteLine("5 - Выход");
        }

        public static void TestNameMenu()
        {
            Console.WriteLine("Выберите тематику теста:");
            Console.WriteLine("1 - История");
            Console.WriteLine("2 - География");            
        }

        public static void ChangesTextMenu()
        {
            Console.WriteLine("Выберите изменяемый параметр:");
            Console.WriteLine("1 - Пароль");
            Console.WriteLine("2 - Дата рождения");
        }


        public static void TextErrorChoice()
        {
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.WriteLine("Вы ввели неправильное значение! Введите корректное значение.");
            Console.ResetColor();
        }
        public static void TextErrorLogin()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("У вас нет аккаунта! Пройдите регистрацию");
            Console.ResetColor();
            Messages.TextNext();
            Console.ReadKey();
        }

        public static void TextErrorRegistration()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Введённый вами логин уже зарегистрирован! Пройдите регистрацию повторно");
            Console.ResetColor();
            Messages.TextNext();
            Console.ReadKey();
        }
        public static void TextNext()=> Console.WriteLine("Для продолжения нажмите любую клавишу.");

     
          
    }
}
