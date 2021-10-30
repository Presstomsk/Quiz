
using System.IO;
using System.Runtime.Serialization.Json;


namespace Quiz
{
    public class ConnectionString
    {
        public string _Server { get; set; }
        public string _Db { get; set; }
        public string _User { get; set; }
        public string _Password { get; set; }

        public static string Init(string path)
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(ConnectionString));
            FileStream buffer = File.OpenRead($"{path}");
            var obj = jsonSerializer.ReadObject(buffer) as ConnectionString;
            buffer.Close();
            return $"Server={obj._Server};Database={obj._Db};Uid={obj._User};Pwd={obj._Password};";
        }
     
    }
}
