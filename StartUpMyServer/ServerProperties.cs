using System;
using System.Collections.Generic;
using System.IO;

namespace StartUpMyServer
{
    public static class ServerPropertiesManager
    {
        private static string serverPropertiesPath;

        public static void SetServerPropertiesPath(string path)
        {
            serverPropertiesPath = path;
        }

        public static Dictionary<string, string> LoadServerProperties()
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();

            try
            {
                string[] lines = File.ReadAllLines(serverPropertiesPath);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                        continue;

                    string[] parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        properties[parts[0].Trim()] = parts[1].Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading server properties: {ex.Message}");
            }

            return properties;
        }

        public static void SaveServerProperties(Dictionary<string, string> properties)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(serverPropertiesPath))
                {
                    foreach (var kvp in properties)
                    {
                        writer.WriteLine($"{kvp.Key}={kvp.Value}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving server properties: {ex.Message}");
            }
        }
    }
}
