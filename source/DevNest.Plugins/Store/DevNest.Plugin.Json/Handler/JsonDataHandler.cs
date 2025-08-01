﻿#region using directives
using DevNest.Common.Base.Constants;
using DevNest.Common.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
#endregion using directives

namespace DevNest.Store.Plugin.Json.Handler
{
    /// <summary>
    /// Represents the class instance for handling JSON data operations.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="JsonDataHandler{T}"/> class with the specified parameters.
    /// </remarks>
    /// <param name="Parameters"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public class JsonDataHandler<T>(Dictionary<string, object> Parameters, IAppLogger<JsonStorePlugin> logger) where T : class
    {
        private readonly IAppLogger<JsonStorePlugin> _Logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
        private readonly long _DefaultMaxFileSizeBytes = Parameters.TryGetValue(ConnectionParamConstants.MaxFileSizeBytes, out var maxFileSize) && maxFileSize is long size ? size : 10485760;
        private readonly Dictionary<string, object> _Parameters = Parameters ?? throw new ArgumentNullException(nameof(Parameters), "Parameters cannot be null.");
        private readonly string _BaseFileName = Parameters.TryGetValue(ConnectionParamConstants.BaseFileName, out var baseFileName) && baseFileName is string name ? name : "data";
        private readonly string _DataDirectory = Parameters.TryGetValue(ConnectionParamConstants.DataDirectory, out var dataDirectory) && dataDirectory is string dir ? dir : string.Empty;

        /// <summary>
        /// Reads a list of items of type T from a JSON file at the specified file path.
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public List<T> ReadAsList(string fileExtension)
        {
            _Logger.LogDebug($"{nameof(JsonDataHandler<T>)} => Reading JSON files from directory.", new { Directory = _DataDirectory });
            var result = new List<T>();

            if (!Directory.Exists(_DataDirectory)) return result;

            string[] files = Directory.GetFiles(_DataDirectory, fileExtension);
            foreach (var file in files)
            {
                var json = File.ReadAllText(file);
                var items = JsonConvert.DeserializeObject<List<T>>(json);
                if (items != null)
                    result.AddRange(items);
            }

            _Logger.LogDebug($"{nameof(JsonDataHandler<T>)} => Json files read successfully.", new { Directory = _DataDirectory, FileCount = files.Length });
            return result;
        }

        /// <summary>
        /// Reads a list of items of type T from JSON files in the specified directory with the given file extension.
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public T ReadAsObject(string fileExtension)
        {
            _Logger.LogDebug($"{nameof(JsonDataHandler<T>)} => Reading JSON files from directory.", new { Directory = _DataDirectory });

            if (!Directory.Exists(_DataDirectory)) 
                return null;

            string[] files = Directory.GetFiles(_DataDirectory, fileExtension);
            foreach (var file in files)
            {
                var json = File.ReadAllText(file);
                var items = JsonConvert.DeserializeObject<T>(json);
                if (items != null)
                    return items as T;
            }

            _Logger.LogDebug($"{nameof(JsonDataHandler<T>)} => Json files read successfully.", new { Directory = _DataDirectory, FileCount = files.Length });
            return default;
        }

        /// <summary>
        /// Writes a list of items of type T to a JSON file at the specified file path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="items"></param>
        public void WriteAsList(List<T> items, string fileExtension)
        {
            _Logger.LogDebug($"{nameof(JsonDataHandler<T>)} => Writing items to JSON file.", new { DataDirectory = _DataDirectory, ItemCount = items.Count });
            if (!Directory.Exists(_DataDirectory))
                Directory.CreateDirectory(_DataDirectory);

            var existingFiles = Directory.GetFiles(_DataDirectory, $"{_BaseFileName}{fileExtension}")
                                         .OrderByDescending(File.GetCreationTime)
                                         .ToList();

            string? currentFilePath = existingFiles.FirstOrDefault();

            if (currentFilePath == null || new FileInfo(currentFilePath).Length >= _DefaultMaxFileSizeBytes)
            {
                var timestamp = DateTime.Now.ToString(ConnectionParamConstants.JsonDateFileFormat);
                currentFilePath = Path.Combine(_DataDirectory, $"{_BaseFileName}_{timestamp}{fileExtension}");
            }

            var json = JsonConvert.SerializeObject(items, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(currentFilePath, json);
            _Logger.LogDebug($"{nameof(JsonDataHandler<T>)} => Items written to JSON file successfully.", new { FilePath = currentFilePath, ItemCount = items.Count });
        }

        /// <summary>
        /// Writes a single item of type T to a JSON file at the specified file path.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="fileExtension"></param>
        public void WriteAsObject(T items, string fileExtension)
        {

            _Logger.LogDebug($"{nameof(JsonDataHandler<T>)} => Writing items to JSON file.", new { DataDirectory = _DataDirectory, Items = items });
            if (!Directory.Exists(_DataDirectory))
                Directory.CreateDirectory(_DataDirectory);

            var existingFiles = Directory.GetFiles(_DataDirectory, $"{fileExtension}")
                                         .OrderByDescending(File.GetCreationTime)
                                         .ToList();

            string? currentFilePath = existingFiles.FirstOrDefault();

            if (currentFilePath == null || new FileInfo(currentFilePath).Length >= _DefaultMaxFileSizeBytes)
            {
                var timestamp = DateTime.Now.ToString(ConnectionParamConstants.JsonDateFileFormat);
                currentFilePath = Path.Combine(_DataDirectory, $"{_BaseFileName}_{timestamp}{fileExtension}");
            }

            var json = JsonConvert.SerializeObject(items, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(currentFilePath, json);
            _Logger.LogDebug($"{nameof(JsonDataHandler<T>)} => Items written to JSON file successfully.", new { FilePath = currentFilePath, Items = items });
        }
    }
}
