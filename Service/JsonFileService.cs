using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace MVVM
{
    public class JsonFileService : IFileService
    {
        public List<Account> Open(string filename)
        {
            string myJson = File.ReadAllText(filename);
            List<Account>? accounts = JsonConvert.DeserializeObject<List<Account>>(myJson);
            return accounts;
        }

        public void Save(string filename, List<Account> phonesList)
        {
            File.WriteAllText(filename, JsonConvert.SerializeObject(phonesList, Formatting.Indented));
        }
    }
}
