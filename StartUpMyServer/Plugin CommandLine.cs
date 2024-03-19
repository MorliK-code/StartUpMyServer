using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using StartUpMyServer;

namespace StartUpMyServer
{
    public static class PluginProcess
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int GetProcessId(IntPtr handle);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(int processAccess, bool bInheritHandle, int processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        private const int PROCESS_QUERY_INFORMATION = 0x0400;
        private const int PROCESS_VM_READ = 0x0010;

        public static string GetCommandLine(this Process process)
        {
            int processId = GetProcessId(process.Handle);
            IntPtr processHandle = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, processId);
            if (processHandle != IntPtr.Zero)
            {
                try
                {
                    StringBuilder commandLine = new StringBuilder(1024);
                    int length = NativeMethods.GetCommandLine(processHandle, commandLine, commandLine.Capacity);
                    if (length > 0)
                    {
                        return commandLine.ToString(0, length);
                    }
                }
                finally
                {
                    CloseHandle(processHandle);
                }
            }
            return null;
        }

        private static class NativeMethods
        {
            [DllImport("ntdll.dll")]
            internal static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass, StringBuilder processInformation, int processInformationLength, out int returnLength);

            internal static int GetCommandLine(IntPtr processHandle, StringBuilder commandLine, int capacity)
            {
                int returnLength;
                int status = NtQueryInformationProcess(processHandle, 0 /* ProcessBasicInformation */, commandLine, capacity, out returnLength);
                if (status == 0)
                {
                    return returnLength;
                }
                return 0;
            }
        }
    }
}
