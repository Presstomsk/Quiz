using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

        public static bool ChangesChoiceMenu(string key, Dictionary<string, Changes> dict, Dictionary<string,string> changeText, User user )
        {
            if (!dict.ContainsKey(key))
            {
                Messages.TextErrorChoice();
                return false;
            }
            Console.Write($"{changeText[key]}");
            var str = Console.ReadLine();
            return dict[key](user, str);
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
            Console.WriteLine(GetTest(test, path[str], out uint score, out string testName));
            db.ScoreHistory(user, testName, score);
            Console.WriteLine();
            Messages.TextNext();
            Console.ReadKey();
        }

        public static string GetTest(Questions questions, string path, out uint score, out string testName)
        {
            score = 0;
            uint numberOfQuestions = 0;            
            testName = null;
            var questCounter = 0;
            var deserializedQuestions = questions.QuestionsDeserialization($"{path}");
            foreach (var quest in deserializedQuestions)
            {
                questCounter++;
                testName = quest.TestName;
                Console.WriteLine(quest.Question);
                foreach (var ans in quest.Answers)
                {
                    Console.WriteLine($"{ans.Key} - {ans.Value}");
                    numberOfQuestions++;
                }
                
                    Console.Write("Укажите правильный ответ:");
                   var answer = Console.ReadLine();
                do
                {

                    if (StringCheck(answer, RegexAnswerPattern(numberOfQuestions)))
                    {
                        var ant = Convert.ToUInt32(answer);
                        if (ant == quest.TrueAnswer) score++;
                    }
                    else
                    { 
                        Messages.TextErrorChoice();
                        Console.Write("Укажите правильный ответ:");
                        answer = Console.ReadLine();                        
                    }
                } while (!StringCheck(answer, RegexAnswerPattern(numberOfQuestions)));
                
            }

            return $"Ваш результат: {score} из {questCounter}";
        }

        public static string RegexAnswerPattern(uint numberOfQuestions)
        {
            return $"[1-{numberOfQuestions}]"+"{1}";
        }

        public static bool StringCheck(string info, string pattern)
        {
            if (Regex.IsMatch(info, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
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

        public static void ChangeSettings(User user,Dictionary<string,Changes> changesMenu, Dictionary<string,string> changeText)
        {
            string key;
            bool choice;
            do
            {
                Messages.ChangesTextMenu();
                key = Console.ReadLine();
                choice = ChangesChoiceMenu(key, changesMenu, changeText,  user);
            } while (!choice);
            Console.WriteLine();
            Messages.TextNext();
            Console.ReadKey();            
        }
    }
}
