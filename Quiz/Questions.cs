
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Quiz
{  
    
    public class Questions
    {
        public string TestName { get; set; }
        public string Question { get; set; }
        public Dictionary<string, string> Answers { get; set; }
        public uint TrueAnswer { get; set; }

        public List<Questions> QuestionsDeserialization(string path)
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<Questions>));
            FileStream buffer = File.OpenRead($"{path}");
            var item = jsonSerializer.ReadObject(buffer) as List<Questions>;
            buffer.Close();
            return item;
        }
        public string GetTest(Questions questions,string path, out uint score, out string testName) //Убрать консоль
        {
            score = 0;
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
                }
                Console.WriteLine("Укажите правильный ответ:");
                var ant = Convert.ToUInt32(Console.ReadLine());
                if (ant == quest.TrueAnswer) score++;                
            }
             
            return $"Ваш результат: {score} из {questCounter}";
        }
    }
}
