using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace StartUpMyServer
{
    public static class JarFileManager
    {
        private static readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "JarFiles.json");

        public static List<JarFile> Load()
        {
            List<JarFile> jarFiles = new List<JarFile>();

            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    jarFiles = JsonConvert.DeserializeObject<List<JarFile>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading jar files: {ex.Message}");
            }

            return jarFiles;
        }

        public static void Save(List<JarFile> jarFiles)
        {
            try
            {
                string json = JsonConvert.SerializeObject(jarFiles, Formatting.Indented);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving jar files: {ex.Message}");
            }
        }
        public static void EditJarFile(JarFile editedJarFile)
        {
            List<JarFile> jarFiles = Load();
            int index = jarFiles.FindIndex(jarFile => jarFile.Id == editedJarFile.Id);
            if (index != -1)
            {
                jarFiles[index] = editedJarFile;
                Save(jarFiles);
            }
        }

    }
}
