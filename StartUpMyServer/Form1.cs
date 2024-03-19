using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace StartUpMyServer
{
    public partial class Form1 : Form
    {
#pragma warning disable CS8618
        private Process serverProcess;
        string jarFilePath, fileName;
        private enum ServerStatus
        {
            Stopped,
            Starting,
            Running
        }
        private void UpdateServerStatus()
        {
            switch (serverStatus)
            {
                case ServerStatus.Stopped:
                    statusLabel.Text = "Отключён";
                    break;
                case ServerStatus.Starting:
                    statusLabel.Text = "Запускается";
                    break;
                case ServerStatus.Running:
                    statusLabel.Text = "Работает";
                    break;
                default:
                    statusLabel.Text = "Отключён";
                    break;
            }
        }

        private ServerStatus serverStatus = ServerStatus.Stopped;
        public Form1()
        {
            InitializeComponent();
            UpdateServerStatus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(jarFilePath))
            {
                MessageBox.Show("Пожалуйста, выберите файл JAR перед запуском сервера.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            serverStatus = ServerStatus.Starting;
            UpdateServerStatus();

            serverProcess = new Process();
            serverProcess.StartInfo.FileName = "java";
            serverProcess.StartInfo.Arguments = "-jar \"" + jarFilePath + "\" nogui";
            serverProcess.StartInfo.UseShellExecute = false;
            serverProcess.StartInfo.CreateNoWindow = true;
            serverProcess.StartInfo.RedirectStandardOutput = true;
            serverProcess.EnableRaisingEvents = true;
            serverProcess.StartInfo.RedirectStandardInput = true;
            serverProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(jarFilePath);
            serverProcess.OutputDataReceived += ServerProcess_OutputDataReceived;
            serverProcess.EnableRaisingEvents = true;
            serverProcess.Exited += (s, evt) =>
            {
                if (!serverProcess.HasExited)
                {
                    serverStatus = ServerStatus.Running;
                    this.Invoke(new Action(UpdateServerStatus));
                }
            };
            serverProcess.BeginOutputReadLine();
        }

        private void ServerProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
                return;

            Invoke(new Action(() =>
            {
                consoleWrite.Items.Add(e.Data);
                consoleWrite.SelectedIndex = consoleWrite.Items.Count - 1;
                consoleWrite.ClearSelected();
            }));
            serverProcess.Exited += (s, evt) =>
            {
                serverStatus = ServerStatus.Stopped;
                this.Invoke(new Action(UpdateServerStatus));
            };
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (serverProcess != null && !serverProcess.HasExited)
                {
                    serverProcess.Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            serverStatus = ServerStatus.Stopped;
            UpdateServerStatus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                jarFilePath = openFileDialog1.FileName;
                fileName = Path.GetFileName(jarFilePath);
                comboBox1.Items.Add(fileName);
                comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serverProcess != null && !serverProcess.HasExited)
            {
                serverProcess.StandardInput.WriteLine("stop");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serverProcess != null && !serverProcess.HasExited)
            {
                serverProcess.Kill();
            }
        }
    }
}
