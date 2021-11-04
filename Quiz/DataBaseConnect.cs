using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Quiz
{
    class DataBaseConnect
    {
        private MySqlConnection db;
        private MySqlCommand command;

        public DataBaseConnect()
        {
            var connectionString = ConnectionString.Init("CSTR.json");
            db = new MySqlConnection(connectionString);
            command = new MySqlCommand { Connection = db };
        }

        public void Open() => db.Open();
        public void Close() => db.Close();        

        public bool LogPasCheck(User user)
        {            
                Open();                
                var sql = $"SELECT login, password FROM tab_user WHERE login='{user.Login}' AND password='{user.Password}';";
                command.CommandText = sql;
                var res = command.ExecuteReader();
                if (!res.HasRows)
                {                    
                    Close();
                    return false;
                }                
                Close();
                return true;           

        }
        public bool LoginCheck(User user)
        {
                Open();
                var sql = $"SELECT login FROM tab_user WHERE login='{user.Login}';";
                command.CommandText = sql;
                var res = command.ExecuteReader();
                if (res.HasRows)
                {                    
                    Close();
                    return false;
                }
                Close();
                return true;            
        }

        public void NewUser(User user)
        {
            Open();            
            var sql = @$"INSERT INTO tab_user (login, password, date_of_birth)
                         VALUES ('{user.Login}','{user.Password}','{user.DateOfBirth?.ToString("yyyy-MM-dd")}');";
            command.CommandText = sql;
            command.ExecuteNonQuery();
            Close();
        }

        public void ScoreHistory(User user, string testName, uint score)
        {
            Open();
            var sql = @$"INSERT INTO tab_log(login, date, test_name, score)
                              VALUES('{user.Login}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{testName}',{score});";
            command.CommandText = sql;
            command.ExecuteNonQuery();
            Close();
        }

        public List<QuizLog> ShowScoreHistory(User user)
        {
            Open();
            var list = new List<QuizLog>();
            var sql = $"SELECT date,test_name,score FROM tab_log WHERE login = '{user.Login}'; ";
            command.CommandText = sql;
            var res = command.ExecuteReader();
            if (!res.HasRows) return null;
            while (res.Read())
            {
                var date = res.GetDateTime("date");
                var test_name = res.GetString("test_name");
                var score = res.GetUInt32("score");               
                list.Add(new QuizLog { Date=date , TestName = test_name, Score = score});

            }
            Close();
            return list;

        }

        public List<Top20List> ShowTop20(string TestName)
        {
            Open();
            var list = new List<Top20List>();
            var sql = @$"SELECT login,score FROM tab_log
                            WHERE test_name='{TestName}'
                                ORDER BY score DESC LIMIT 20;";
            command.CommandText = sql;
            var res = command.ExecuteReader();
            if (!res.HasRows) return null;
            while (res.Read())
            {
                var login = res.GetString("login");
                var score = res.GetUInt32("score");              
                list.Add(new Top20List { Login = login, Score = score });

            }
            Close();
            return list;
        }

        public bool LoginChange(User user, string str) 
        {
            
                user.Password = str;
                Open();
                var sql = @$"UPDATE tab_user
                           SET password='{user.Password}'
                             WHERE login='{user.Login}';";
                command.CommandText = sql;
                var num = command.ExecuteNonQuery();
                Close();
                return num != 0;
            
        }
        public bool DateOfBirthChange(User user, string str) 
        {
            user.DateOfBirth = DateTime.Parse(str);            
            Open();
            var sql = @$"UPDATE tab_user
                           SET date_of_birth='{user.DateOfBirth?.ToString("yyyy-MM-dd")}'
                             WHERE login='{user.Login}';";
            command.CommandText = sql;
            var num = command.ExecuteNonQuery();
            Close();
            return num != 0;
        }

    }
}
