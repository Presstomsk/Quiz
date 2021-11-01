

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
       
    }
}
