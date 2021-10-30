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

    }
}
