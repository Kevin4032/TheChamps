using System.Text.Json;
using System.Text.Json.Serialization;

namespace Service.FileReader
{
    public class FileController : IJsonReader
    {
        public FileController() { }

		public string ReadJson(string path)
        {
            return File.ReadAllText(path);
        }
    }

}