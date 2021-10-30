using System;
using System.Collections.Generic;

namespace Quiz
{
    public delegate User Operation();
    public delegate bool Changes(User user);
    class Program
    {
        
        public static Dictionary<string, Operation> AuthRegMenu;
        public static Dictionary<string, string> TestNameMenu, Path;
        public static Dictionary<string, Changes> ChangesMenu;

        static void Main()
        {
            bool choice, flag;
            string str;
            User user;
            DataBaseConnect db = new DataBaseConnect();

            Messages.AuthRegTextMenu();     
            
            AuthRegMenu = new Dictionary<string, Operation>
            {
             { "1",Menu.Entrance},
             { "2",Menu.Registration}
            };
            TestNameMenu = new Dictionary<string, string>
            {
                {"1","История"},
                {"2","География"}
            };
            Path = new Dictionary<string, string>
            {
                {"История","history.json"},
                {"География","geography.json"}
            };
            ChangesMenu = new Dictionary<string, Changes>
            {

                {"1",db.LoginChange},
                {"2",db.DateOfBirthChange}
            };




            do
            {
                var key = Console.ReadLine();
                user = Menu.AuthRegChoiceMenu(key, AuthRegMenu);//Регистрация или авторизация
            } while (user == null);

            if(user.DateOfBirth==null) choice = db.LogPasCheck(user);
            else choice = db.LoginCheck(user);

            if (!choice)
            {
                do 
                {
                    user = Menu.Registration();  //Регитрация
                    choice = db.LoginCheck(user);
                    if (choice) db.NewUser(user);                
                } while (!choice);
            }
            if (choice)
            {
                flag = false;
                do
                {
                    Messages.AccTextMenu(user);//Личный кабинет пользователя
                    var key = Console.ReadLine();               
               
                    switch (key)
                    {
                        case "1":
                            Questions test = new Questions(); //Начать новую викторину
                            do
                            {
                                Messages.TestNameMenu();
                                var nameTestKey = Console.ReadLine();
                                str = Menu.TestChoiceMenu(nameTestKey, TestNameMenu);

                            } while (str == null);
                            Console.WriteLine(test.GetTest(test,Path[str], out uint score, out string testName));
                            db.ScoreHistory(user, testName, score);
                            Console.WriteLine();
                            Messages.TextNext();
                            Console.ReadKey();
                            break;
                        case "2":  //Посмотреть результаты своих прошлых викторин
                            var storyList = db.ShowScoreHistory(user);
                            Console.WriteLine("Результаты моих викторин (Дата, Категория теста, Результат):  ");
                            Console.WriteLine();
                            foreach (var story in storyList)
                            {
                                Console.WriteLine($"{story.Date}            {story.TestName}            {story.Score}");
                            }
                            Console.WriteLine();
                            Messages.TextNext();
                            Console.ReadKey();
                            break;
                        case "3"://Посмотреть Топ-20 по конкретной викторине
                            do
                            {
                                Messages.TestNameMenu();
                                var nameTestKey = Console.ReadLine();
                                str = Menu.TestChoiceMenu(nameTestKey, TestNameMenu);

                            } while (str == null);
                            var top20 = db.ShowTop20(str);
                            Console.WriteLine($"Топ 20 по викторине {str}:  ");
                            Console.WriteLine();
                            foreach (var result in top20)
                            {
                                Console.WriteLine($"{result.Login}-{result.SumScore}");
                            }
                            Console.WriteLine();
                            Messages.TextNext();
                            Console.ReadKey();
                            break;
                        case "4"://Изменить настройки: дата рождения и пароль
                            do
                            {
                                Messages.ChangesTextMenu();
                                key = Console.ReadLine();                                
                                choice = Menu.ChangesChoiceMenu(key, ChangesMenu, user);
                            } while (!choice);
                            Console.WriteLine();
                            Messages.TextNext();
                            Console.ReadKey();
                            break;
                        case "5"://Выход
                            flag = true;
                            break;
                    }
                } while (!flag);

            }


           

        }
    }
}
