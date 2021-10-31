using System;
using System.Collections.Generic;


namespace Quiz
{
    class Menu
    {
        
        public delegate void AccOperation();

        public static User AuthRegChoiceMenu(string key, Dictionary<string, Operation> dict)
        {           
            if (!dict.ContainsKey(key))
            {
                Messages.TextErrorChoice();                
                return null;
            }

            return dict[key]();          
        }

        public static User Entrance()
        {
            Messages.AuthorizationText();
            var login = Messages.Login();
            var password = Messages.Password();
            return new User(login,password);
        }
        public static User Registration()
        {
            Messages.RegistrationText();
            var login = Messages.Login();
            var password = Messages.Password();
            var date_of_birth = DateTime.Parse(Messages.Date_of_birth());
            return new User(login, password, date_of_birth);            
        }

        public static string TestChoiceMenu(string key, Dictionary<string, string> dict)
        {
            if (!dict.ContainsKey(key))
            {
                Messages.TextErrorChoice();
                return null;
            }

            return dict[key];
        }

        public static bool ChangesChoiceMenu(string key, Dictionary<string, Changes> dict, User user )
        {
            if (!dict.ContainsKey(key))
            {
                Messages.TextErrorChoice();
                return false;
            }

            return dict[key](user);
        }

        public static void StartQuiz(User user,Dictionary<string,string> testNameMenu, Dictionary<string, string> path, DataBaseConnect db) //Начало викторины
        {
            Questions test = new Questions();
            string str;
            do
            {
                Messages.TestNameMenu();
                var nameTestKey = Console.ReadLine();
                str = TestChoiceMenu(nameTestKey, testNameMenu);

            } while (str == null);
            Console.WriteLine(test.GetTest(test, path[str], out uint score, out string testName));
            db.ScoreHistory(user, testName, score);
            Console.WriteLine();
            Messages.TextNext();
            Console.ReadKey();
        }

        public static void AllQuizResultShow(User user,DataBaseConnect db)
        {
            var storyList = db.ShowScoreHistory(user);
            Console.WriteLine("Результаты моих викторин (Дата, Категория теста, Результат):  ");
            Console.WriteLine();
            foreach (var story in storyList)
            {
                Console.WriteLine($"{story.Date} - {story.TestName} - {story.Score}");
            }
            Console.WriteLine();
            Messages.TextNext();
            Console.ReadKey();
        }

        public static void Top20ResultShow(Dictionary<string, string> testNameMenu, DataBaseConnect db)
        {
            string str;
            do
            {
                Messages.TestNameMenu();
                var nameTestKey = Console.ReadLine();
                str = TestChoiceMenu(nameTestKey, testNameMenu);

            } while (str == null);
            var top20 = db.ShowTop20(str);
            Console.WriteLine($"Топ 20 по викторине {str}:  ");
            Console.WriteLine();
            foreach (var result in top20)
            {
                Console.WriteLine($"{result.Login}-{result.Score}");
            }
            Console.WriteLine();
            Messages.TextNext();
            Console.ReadKey();
        }

        public static void ChangeSettings(User user,Dictionary<string,Changes> changesMenu)
        {
            string key;
            bool choice;
            do
            {
                Messages.ChangesTextMenu();
                key = Console.ReadLine();
                choice = Menu.ChangesChoiceMenu(key, changesMenu, user);
            } while (!choice);
            Console.WriteLine();
            Messages.TextNext();
            Console.ReadKey();            
        }
    }
}
