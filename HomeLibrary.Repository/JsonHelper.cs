using System.IO;
using Newtonsoft.Json;

namespace HomeLibrary.Repository
{
    public class JsonHelper
    {
        private string jsonPath = @"..\..\..\JsonData\"; //TODO: move to config
        
        public void WriteAsJson<T>(string fileName, T[] items)
        {
            var json = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(Path.Combine(jsonPath, fileName), json);
        }

        public T[] ReadFromJson<T>(string fileName)
        {
            var path = Path.Combine(jsonPath, fileName);
            if (!File.Exists(path))
                return new T[0];

            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T[]>(json) ?? new T[0];
        }
    }
}
