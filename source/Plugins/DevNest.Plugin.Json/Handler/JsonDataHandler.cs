#region using directives
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
#endregion using directives

namespace DevNest.Plugin.Json.Handler
{
    /// <summary>
    /// Represents the class instance for handling JSON data operations.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonDataHandler<T> where T : class
    {
        private readonly long _DefaultMaxFileSizeBytes;
        private readonly Dictionary<string, object> _Parameters;
        private readonly string _BaseFileName;
        private readonly string _DataDirectory;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDataHandler{T}"/> class with the specified parameters.
        /// </summary>
        /// <param name="Parameters"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public JsonDataHandler(Dictionary<string, object> Parameters)
        {
            _Parameters = Parameters ?? throw new ArgumentNullException(nameof(Parameters), "Parameters cannot be null.");
            _DefaultMaxFileSizeBytes = Parameters.TryGetValue("maxFileSizeBytes", out var maxFileSize) && maxFileSize is long size ? size : 10485760; // Default to 10 MB
            _BaseFileName = Parameters.TryGetValue("baseFileName", out var baseFileName) && baseFileName is string name ? name : "credential"; // Default base file name
            _DataDirectory = Parameters.TryGetValue("dataDirectory", out var dataDirectory) && dataDirectory is string dir ? dir : "Credential-Manager"; // Default data directory
        }

        /// <summary>
        /// Reads a list of items of type T from a JSON file at the specified file path.
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public List<T> Read()
        {
            var result = new List<T>();

            if (!Directory.Exists(_DataDirectory)) return result;

            var files = Directory.GetFiles(_DataDirectory, $"{_BaseFileName}*.json");

            foreach (var file in files)
            {
                var json = File.ReadAllText(file);
                var items = JsonConvert.DeserializeObject<List<T>>(json);
                if (items != null)
                    result.AddRange(items);
            }

            return result;
        }

        /// <summary>
        /// Writes a list of items of type T to a JSON file at the specified file path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="items"></param>
        public void Write(List<T> items)
        {
            if (!Directory.Exists(_DataDirectory))
                Directory.CreateDirectory(_DataDirectory);

            var existingFiles = Directory.GetFiles(_DataDirectory, $"{_BaseFileName}*.json")
                                         .OrderByDescending(File.GetCreationTime)
                                         .ToList();

            string? currentFilePath = existingFiles.FirstOrDefault();

            if (currentFilePath == null || new FileInfo(currentFilePath).Length >= _DefaultMaxFileSizeBytes)
            {
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                currentFilePath = Path.Combine(_DataDirectory, $"{_BaseFileName}_{timestamp}.json");
            }

            var json = JsonConvert.SerializeObject(items, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(currentFilePath, json);
        }
    }
}
