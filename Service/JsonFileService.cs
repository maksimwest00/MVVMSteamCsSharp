using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace MVVM
{
    public class JsonFileService : IFileService
    {
        public List<T> Open<T>(string filename)
        {
            string myJson = File.ReadAllText(filename);
            List<T>? accounts = JsonConvert.DeserializeObject<List<T>>(myJson);
            return accounts;
        }

        public void Save<T>(string filename, List<T> accountsList)
        {
            File.WriteAllText(filename, JsonConvert.SerializeObject(accountsList, Formatting.Indented));
        }
    }
}
