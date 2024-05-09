using StartUpMyServer.Properties;
using System.Diagnostics;

namespace StartUpMyServer
{

    public class JavaServer
    {
        Process[] javaProcess;

        private readonly object lockObject = new object();
        private Process serverProcess;

        public event Action<string> ServerOutputReceived;
        
        public void Start(string javaPath, string jarPath, string jarFolder)
        {
            lock (lockObject)
            {
                if (IsServerRunning(javaPath))
                {
                    ServerOutputReceived?.Invoke("Сервер уже запущен или запускается.");
                    return;
                }

                serverProcess = new Process();
                serverProcess.StartInfo.FileName = javaPath;
                serverProcess.StartInfo.Arguments = $"{Settings.Default.startUpMinGb} {Settings.Default.startUpMaxGb} -jar \"{jarPath}\" nogui" ;
                serverProcess.StartInfo.UseShellExecute = false;
                serverProcess.StartInfo.CreateNoWindow = true;
                serverProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName($"{jarFolder}");
                serverProcess.StartInfo.RedirectStandardOutput = true;
                serverProcess.StartInfo.RedirectStandardInput = true;
                serverProcess.EnableRaisingEvents = true;
                serverProcess.OutputDataReceived += ServerProcess_OutputDataReceived;

                try
                {
                    serverProcess.Start();
                    serverProcess.BeginOutputReadLine();
                }
                catch (Exception ex)
                {
                    ServerOutputReceived?.Invoke($"Ошибка запуска сервера: {ex.Message}");
                    serverProcess = null;
                }
            }
        }

        public void Stop(string javaPath)
        {
            lock (lockObject)
            {
                javaProcess = Process.GetProcessesByName("java");
                foreach (Process jarProcess in javaProcess)
                {
                    if (CheckServerPath(jarProcess, javaPath))
                        serverProcess.StandardInput.WriteLine("stop");
                }
            }
        }

        public void Restart(string javaPath)
        {
            lock (lockObject)
            {
                javaProcess = Process.GetProcessesByName("java");
                foreach (Process jarProcess in javaProcess)
                {
                    if (CheckServerPath(jarProcess, javaPath))
                        serverProcess.StandardInput.WriteLine("restart");
                }
            }
        }
        public void sendCommand(string javaPath, string command)
        {
            lock (lockObject)
            {
                javaProcess = Process.GetProcessesByName("java");
                foreach (Process jarProcess in javaProcess)
                {
                    if (CheckServerPath(jarProcess, javaPath))
                        serverProcess.StandardInput.WriteLine($"{command}");
                }
            }
        }

        public void Kill(string javaPath)
        {
            lock(lockObject)
            {
                javaProcess = Process.GetProcessesByName("java");
                foreach (Process jarProcess in javaProcess)
                {
                    if (CheckServerPath(jarProcess, javaPath))
                        jarProcess.Kill();
                }
            }
        }

        private void ServerProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                ServerOutputReceived?.Invoke(e.Data);
            }
        }
        public bool IsServerRunning(string javaPath)
        {
            javaProcess = Process.GetProcessesByName("java");
            foreach (Process process in javaProcess)
            {
                if (CheckServerPath(process, javaPath))
                return true;
            }
            return false;
        }

        private bool CheckServerPath(Process jarProcess, string javaPath)
        {
            try
            {
                string serverargs = jarProcess.MainModule.FileName;
                return serverargs.Equals(javaPath, StringComparison.OrdinalIgnoreCase);
            }
            catch { return false; }
            }

        public void KillIsProcessRunning(string javaPath)
        {
            javaProcess = Process.GetProcessesByName("java");
            foreach (Process jarProcess in javaProcess)
            {
                if (CheckServerPath(jarProcess, javaPath))
                    jarProcess.Kill();
            }
        }

        public void KillAllJava()
        {
            javaProcess = Process.GetProcessesByName("java");
            foreach (Process javaProcesses in javaProcess)
            {
                javaProcesses.Kill();
            }
        }
    }
}
