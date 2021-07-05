using System.IO;
using UnityEngine;

namespace Save
{
    public class JsonSaveAndLoad
    {
        public static void Save<T>(T data, string file)
        {
            var json = JsonUtility.ToJson(data);
            WriteToFile(file, json);
        }
        
        public static void Load<T>(ref T data, string file) where T : new()
        {
            data = new T();
            var json = ReadToFile(file);
            JsonUtility.FromJsonOverwrite(json, data);
        }

        public static string ReadToFile(string fileName)
        {
            var path = GetFilePath(fileName);
            if (File.Exists(path))
            {
                using (var reader = new StreamReader(path))
                {
                    var json = reader.ReadToEnd();
                    return json;
                }
            }
            else
            {
                Debug.LogWarning("File not find!");
            }

            return null;
        }
        
        public static bool Exists(string fileName)
        {
            var path = GetFilePath(fileName);
            return File.Exists(path);
        }

        public static void WriteToFile(string fileName, string json)
        {
            var path = GetFilePath(fileName);
            var fileStream = new FileStream(path, FileMode.Create);

            using (var writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }
        }
        
        public static string GetFilePath(string fileName)
        {
            return Application.persistentDataPath + "/" + fileName;
        }
    }
}