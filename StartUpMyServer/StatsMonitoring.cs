using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace StartUpMyServer
{
    internal class StatsMonitoring
    {
        private PerformanceCounter checkMemory;
        private PerformanceCounter availableMemory;
        public event Action<int> CheckMemoryServer;
        public event Action<int> AvailableMemory;
        string javaPath;
        string javaProcess;

        public StatsMonitoring()
        {
            checkMemory = new PerformanceCounter("Process", "Working Set", "java");
            availableMemory = new PerformanceCounter("Memory", "Available MBytes");
        }

        public void StartMonitoring(string javaPath)
        {
            Process[] javaProcess = Process.GetProcessesByName("java");
            foreach (Process process in javaProcess)
            {
                if (CheckServerPath(process, javaPath))
                {
                    UpdateMemoryUsage();
                }
            }
            UpdateAvalibleMemory();
        }

        private void UpdateMemoryUsage()
        {
            try
            {
                float memoryUsage = checkMemory.NextValue() / (1024 * 1024);
                CheckMemoryServer?.Invoke((int)memoryUsage);
            }
            catch { CheckMemoryServer?.Invoke(0); }
        }

        private void UpdateAvalibleMemory()
        {
            try
            {
                /*float availablePhysicalMemoryMB = SystemInfo.GetTotalPhysicalMemory();*/
                float availableMemoryOut = availableMemory.NextValue();
                AvailableMemory?.Invoke((int)availableMemoryOut);
            }
            catch { AvailableMemory?.Invoke(0); }
        }

        private bool CheckServerPath(Process javaProcess, string javaPath)
        {
            try
            {
                string processPath = javaProcess.MainModule.FileName;
                return processPath.Equals(javaPath, StringComparison.OrdinalIgnoreCase);
            }
            catch {  return false; }
        }

    }
}
