using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.VisualBasic.Devices;
using System.Management;

namespace StartUpMyServer
{
    internal class StatsMonitoring
    {
        private Thread updateAll;
        
        private ComputerInfo computerInfo;

        private PerformanceCounter checkServerRam;
        private PerformanceCounter checkAvailableRam;
        private PerformanceCounter cpuUsageTotal;
        
        private ManagementObjectSearcher search;
        private ManagementObjectSearcher searchObj;

        public event Action<int> CheckMemory;
        public event Action<int> RamMemory;
        public event Action<int> CpuUsage;
        public event Action<int> WeUsageRam;
        
        string javaPath, javaProcess;
        float valueRamStatic;

        public StatsMonitoring()
        {
            checkServerRam = new PerformanceCounter("Process", "Working Set", "java");
            checkAvailableRam = new PerformanceCounter("Memory", "Available MBytes");
            computerInfo = new ComputerInfo();
            cpuUsageTotal = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            search = new ManagementObjectSearcher("select * from Win32_Processor");
        }

        public void CheckMonitoring()
        {
            updateAll = new Thread(UpdateAll);
            updateAll.IsBackground = true;
            updateAll.Start();
        }

        public void MonitoringServerRam(string javaPath)
        {
            try
            {
                Process[] javaProcess = Process.GetProcessesByName("java");
                foreach (Process process in javaProcess)
                {
                    if (CheckServerPath(process, javaPath))
                    {
                        UpdateMemoryUsage();
                    }
                }
            }
            catch { }
        }

        private void UpdateAll()
        {
            CpuCheckUsage();
            UpdateMemoryUsage();
            CheckRamMemory();
            AvailableRam();
        }

        private void AvailableRam()
        {
            try
            {
                float ramUsed = checkAvailableRam.NextValue();
                float WeUsedRam = valueRamStatic - ramUsed;
                WeUsageRam?.Invoke((int)WeUsedRam);
            }
            catch { }
        }

        private void CpuCheckUsage()
        {
            try
            {
                float currentCpuUsage = cpuUsageTotal.NextValue();
                CpuUsage?.Invoke((int)currentCpuUsage);
            }
            catch { CpuUsage?.Invoke(0); }
        }

        private void UpdateMemoryUsage()
        {
            try
            {
                float memoryUsage = checkServerRam.NextValue() / (1024 * 1024);
                CheckMemory?.Invoke((int)memoryUsage);
            }
            catch { CheckMemory?.Invoke(0); }
        }

        private void CheckRamMemory()
        {
            try
            {
                float memoryRam = computerInfo.TotalPhysicalMemory / (1024 * 1024);
                valueRamStatic = memoryRam;
                RamMemory?.Invoke((int)memoryRam);
            }
            catch { RamMemory?.Invoke(0);}
            
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
