using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonFile
{
    public static class JsonHelper
    {

        public static List<T> ReadJson<T>(string filePath)
        {
            var rawJson = File.ReadAllText(filePath);
            var result = JsonSerializer.Deserialize<List<T>>(rawJson);

            return result;
        }
    }
}
