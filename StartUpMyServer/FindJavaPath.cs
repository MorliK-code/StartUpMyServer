using System;
using System.IO;

namespace StartUpMyServer
{
    public class JavaPathFinder
    {
        public static string FindJavaPath()
        {
            string[] standardJavaPaths = {
                @"C:\Program Files\Java",
                @"C:\Program Files\AdoptOpenJDK",
                @"C:\Program Files\Eclipse Adoptium",
                @"C:\Program Files\Amazon Corretto",
                @"C:\Program Files\Zulu"
            };

            foreach (string path in standardJavaPaths)
            {
                string javaPath = FindJavaInDirectory(path);
                if (javaPath != null)
                {
                    return javaPath;
                }
            }

            // Поиск в реестре Windows
            string javaPathFromRegistry = FindJavaPathFromRegistry();
            if (javaPathFromRegistry != null)
            {
                return javaPathFromRegistry;
            }

            // Поиск переменной среды JAVA_HOME
            string javaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
            if (javaHome != null)
            {
                string javaExecutablePath = Path.Combine(javaHome, "bin", "java.exe");
                if (File.Exists(javaExecutablePath))
                {
                    return javaExecutablePath;
                }
            }

            // Если ничего не найдено, возвращаем null
            return null;
        }

        private static string FindJavaInDirectory(string directory)
        {
            if (Directory.Exists(directory))
            {
                string[] subdirectories = Directory.GetDirectories(directory);
                foreach (string subdirectory in subdirectories)
                {
                    string javaPath = Path.Combine(subdirectory, "bin", "java.exe");
                    if (File.Exists(javaPath))
                    {
                        return javaPath;
                    }
                }
            }
            return null;
        }

        private static string FindJavaPathFromRegistry()
        {
            string[] registryKeys = {
                @"SOFTWARE\JavaSoft\Java Runtime Environment",
                @"SOFTWARE\Wow6432Node\JavaSoft\Java Runtime Environment"
            };

            foreach (string key in registryKeys)
            {
                using (Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(key))
                {
                    if (rk != null)
                    {
                        string currentVersion = rk.GetValue("CurrentVersion")?.ToString();
                        if (currentVersion != null)
                        {
                            using (Microsoft.Win32.RegistryKey subKey = rk.OpenSubKey(currentVersion))
                            {
                                if (subKey != null)
                                {
                                    string javaHome = subKey.GetValue("JavaHome")?.ToString();
                                    if (javaHome != null)
                                    {
                                        string javaPath = Path.Combine(javaHome, "bin", "java.exe");
                                        if (File.Exists(javaPath))
                                        {
                                            return javaPath;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
