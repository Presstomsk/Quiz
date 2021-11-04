using System;
using System.Collections.Generic;

namespace Quiz
{
    public delegate User Operation();
    public delegate bool Changes(User user,string str);
    class Program
    {
        
        public static Dictionary<string, Operation> AuthRegMenu;
        public static Dictionary<string, string> TestNameMenu, Path, changeText;
        public static Dictionary<string, Changes> ChangesMenu;

        static void Main()
        {
            bool choice, flag;            
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

            changeText = new Dictionary<string, string>
            {
                {"1","Новый пароль: "},
                {"2","Новая дата рождения: "}
            };



            do
            {
                var key = Console.ReadLine();
                user = Menu.AuthRegChoiceMenu(key, AuthRegMenu);//Регистрация или авторизация
            } while (user == null);

            if (user.DateOfBirth == null) //Проверка данных пользователя
            {
                choice = db.LogPasCheck(user);
                if (!choice) Messages.TextErrorLogin();               
            }
            else
            { 
                choice = db.LoginCheck(user);
                if (!choice) Messages.TextErrorRegistration();
                else db.NewUser(user);
                
            }

            if (!choice)
            {
                do 
                {
                    user = Menu.Registration();  //Регитрация
                    choice = db.LoginCheck(user);
                    if (choice) db.NewUser(user);
                    else Messages.TextErrorRegistration();                    
                } while (!choice);
            }
            if (choice)
            {
                flag = false;
                
                do
                {
                    Messages.AccTextMenu(user);//Личный кабинет пользователя
                    string key;
                    key = Console.ReadLine();
               
                    switch (key)
                    {
                        case "1"://Начало викторины
                            Menu.StartQuiz(user,TestNameMenu,Path,db);
                            break;
                        case "2":  //Посмотреть результаты своих прошлых викторин
                            Menu.AllQuizResultShow(user, db);
                            break;
                        case "3"://Посмотреть Топ-20 по конкретной викторине
                            Menu.Top20ResultShow(TestNameMenu, db);
                            break;
                        case "4"://Изменить настройки: дата рождения и пароль
                            Menu.ChangeSettings(user, ChangesMenu, changeText);
                            break;
                        case "5"://Выход
                            flag = true;
                            break;
                        default:
                            Messages.TextErrorChoice();
                            Messages.TextNext();
                            Console.ReadKey();                           
                            break;
                    }
                } while (!flag);

            }


           

        }
    }
}
