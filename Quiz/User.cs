using System;


namespace Quiz
{
   public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public User()
        {
            Login = null;
            Password = null;
            DateOfBirth = null;
        }

        public User(string login, string password)
        {
            Login = login;
            Password = password;
            DateOfBirth = null;
        }
        public User(string login, string password, DateTime? dateOfBirth)
        {
            Login = login;
            Password = password;
            DateOfBirth = dateOfBirth;
        }

    }
}
