using StartUpMyServer.Properties;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace StartUpMyServer
{
    public partial class MainServer : Form
    {
        private JavaServer server;
        private JavaPathFinder javaPathFinder;
        private ProgressServer progress;
        private StatsMonitoring monitoring;

        private string javaPath, selectedJarFile, selectedJarName, selectedFolder, startupFolder, selectedAssemblyName, customStartupPath;
        private int ramMemory, WeUsedRam;

        List<string> allMessage = new List<string>();
        List<string> infoMessages = new List<string>();
        List<string> errorMessages = new List<string>();
        List<string> warnMessages = new List<string>();

        public MainServer()
        {
            InitializeComponent();
            FirstLoad();
        }

        private void FirstLoad()
        {
            server = new JavaServer();
            javaPathFinder = new JavaPathFinder();
            progress = new ProgressServer(progressStartUp, this);
            javaPath = JavaPathFinder.FindJavaPath();
            monitoring = new StatsMonitoring();

            UpdateSelectAssembly();
            EnableServerButtons(true);

            selectLogLevel.SelectedItem = selectLogLevel.SelectedIndex = 0;
            selectAssembly.SelectedItem = Settings.Default.selectAssembly;
            server.ServerOutputReceived += ServerOutput;
            monitoring.RamMemory += UpdateRamMemory;
            monitoring.CheckMemory += UpdateUsingMemory;
            monitoring.CpuUsage += UpdateCpuUsage;
            monitoring.WeUsageRam += WeUsignRam;

            statusServer.BackColor = Color.Transparent;
        }

        private void UpdateRamMemory(int checkRamMemory)
        {
            ramMemory = checkRamMemory;
        }
        private void WeUsignRam(int WeUsed)
        {
            WeUsedRam = WeUsed;
        }
        private void UpdateUsingMemory(int memoryUsage)
        {
            memoryUsageLabel.Invoke((MethodInvoker)(() => memoryUsageLabel.Text = ($"{WeUsedRam} Мб / {ramMemory} Мб")));
            memoryUsageBar.Invoke((MethodInvoker)(() => memoryUsageBar.Maximum = ramMemory));
            memoryUsageBar.Invoke((MethodInvoker)(() => memoryUsageBar.Value = WeUsedRam));
            memoryUsageServerLabel.Invoke((MethodInvoker)(() => memoryUsageServerLabel.Text = ($"{memoryUsage} Мб / 4000 Мб")));
            memoryUsageServerBar.Invoke((MethodInvoker)(() => memoryUsageServerBar.Value = memoryUsage));
        }

        private void UpdateCpuUsage(int cpuPercent)
        {
            cpuUsageLabel.Invoke((MethodInvoker)(() => cpuUsageLabel.Text = ($"{cpuPercent} % / 100 %")));
            cpuUsageBar1.Invoke((MethodInvoker)(() => cpuUsageBar1.Value = cpuPercent));
        }

        private void ServerOutput(string output)
        {
            progress.LoadingStartUp(output);

            if (!string.IsNullOrEmpty(output))
            {
                try
                {
                    Invoke(new Action(() =>
                    {
                        switch (selectLogLevel.Text)
                        {
                            case "Весь вывод":
                                consoleWrite.Items.Add(RemoveAnsiEscapeCodes(output));
                                break;
                            case "Только INFO":
                                if (output.Contains("INFO"))
                                    consoleWrite.Items.Add(RemoveAnsiEscapeCodes(output));
                                break;
                            case "Только WARN":
                                if (output.Contains("WARN"))
                                    consoleWrite.Items.Add(RemoveAnsiEscapeCodes(output));
                                break;
                            case "Только ERROR":
                                if (output.Contains("ERROR"))
                                    consoleWrite.Items.Add(RemoveAnsiEscapeCodes(output));
                                break;
                        }

                        allMessage.Add(RemoveAnsiEscapeCodes(output));
                        if (output.Contains("INFO"))
                        {
                            infoMessages.Add(RemoveAnsiEscapeCodes(output));
                        }
                        if (output.Contains("WARN"))
                        {
                            warnMessages.Add(RemoveAnsiEscapeCodes(output));
                        }
                        if (output.Contains("ERROR"))
                        {
                            errorMessages.Add(RemoveAnsiEscapeCodes(output));
                        }

                        consoleWrite.SelectedIndex = consoleWrite.Items.Count - 1;
                        consoleWrite.ClearSelected();
                    }));
                }
                catch { }
            }
        }
        private string RemoveAnsiEscapeCodes(string input)
        {
            return Regex.Replace(input, @"\x1B\[([0-9]{1,2}(;[0-9]{1,2})?)?[m|K]", "");
        }
        private void startServer_Click(object sender, EventArgs e)
        {
            ClearForStart();

            if (!string.IsNullOrEmpty(selectedJarFile) && !string.IsNullOrEmpty(startupFolder))
            {
                server.Start(javaPath, selectedJarFile, selectedFolder);
            }
            else
            {
                consoleWrite.Items.Add("Ошибка запуска сервера. Проверьте запускаемый JAR файл или папку запуска.");
            }
        }

        private void stopServer_Click(object sender, EventArgs e)
        {
            server.Stop(javaPath);
        }

        private void restartServer_Click(object sender, EventArgs e)
        {
            server.Restart(javaPath);
        }

        private void killServer_Click(object sender, EventArgs e)
        {
            server.Kill(javaPath);
        }

        private async void killandrestartServer_Click(object sender, EventArgs e)
        {
            server.Kill(javaPath);
            await Task.Delay(2000);
            server.Start(javaPath, selectedJarFile, selectedFolder);
        }

        private void addAssembly_Click(object sender, EventArgs e)
        {
            string selectedName = null;
            if (selectAssembly.SelectedItem != null)
            {
                selectedName = selectAssembly.SelectedItem.ToString();
            }

            if (string.IsNullOrEmpty(selectedName))
                AddNewAssembly();
            else
                EditAssembly(selectedName);
        }

        private void AddNewAssembly()
        {
            using (var addAssemblyForm = new AddAssemblyForm(null, null, null))
            {
                if (addAssemblyForm.ShowDialog() == DialogResult.OK)
                {
                    selectedAssemblyName = addAssemblyForm.AssemblyName;
                    selectedJarFile = addAssemblyForm.JarFileName;
                    customStartupPath = addAssemblyForm.selectedFolder;

                    selectAssembly.Items.Add(selectedAssemblyName);
                    selectAssembly.SelectedItem = selectedAssemblyName;
                }
            }
        }

        private void ClearForStart()
        {
            consoleWrite.Items.Clear();
            progressStartUp.Value = 0;
            allMessage.Clear();
            infoMessages.Clear();
            warnMessages.Clear();
            errorMessages.Clear();
        }

        private void EditAssembly(string selectedName)
        {
            var assemblies = JarFileManager.Load();
            var selected = assemblies.Find(jar => jar.Name == selectedName);

            if (selected != null)
            {
                using (var addAssemblyForm = new AddAssemblyForm(selected.Name, selected.JarFilePath, selected.Folder))
                {
                    if (addAssemblyForm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateSelectAssembly();
                    }
                }
            }
        }

        private void UpdateStatus()
        {
            bool isServerOpen = server.IsServerRunning(javaPath);
            if (isServerOpen)
            {
                statusServer.Text = "Запущен";
                EnableServerButtons(false);
            }
            else
            {
                progressStartUp.Value = 0;
                statusServer.Text = "Остановлен";
                EnableServerButtons(true);
            }
        }

        private void UpdateSelectAssembly()
        {
            selectAssembly.Items.Clear();
            foreach (var jarFile in JarFileManager.Load())
            {
                selectAssembly.Items.Add(jarFile.Name);
            }
        }

        private void MainServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.KillIsProcessRunning(javaPath);
        }

        private void selectAssembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectAssembly.SelectedItem != null)
            {
                selectedAssemblyName = selectAssembly.SelectedItem.ToString();

                if (!string.IsNullOrEmpty(selectedAssemblyName))
                {
                    var assemblies = JarFileManager.Load();
                    var selected = assemblies.Find(jar => jar.Name == selectedAssemblyName);

                    if (selected != null)
                    {
                        selectedJarName = selected.ToString();
                        selectedJarFile = selected.JarFilePath;
                        startupFolder = selected.Folder;
                        selectedFolder = $"{startupFolder}\\{selectedJarName}";

                        consoleWrite.Items.Clear();
                        consoleWrite.Items.Add($"Название сборки: {selectedJarName}");
                        consoleWrite.Items.Add($"Название JAR файла: {selectedJarName}");
                        consoleWrite.Items.Add($"Путь JAR файла: {selectedJarFile}");
                        consoleWrite.Items.Add($"Папка запуска: {startupFolder}");
                        consoleWrite.SelectedIndex = consoleWrite.Items.Count - 1;
                        consoleWrite.ClearSelected();

                        Settings.Default.selectAssembly = selectedAssemblyName;
                        Settings.Default.Save();
                    }
                }
            }
        }

        private void deleteAssembly_Click(object sender, EventArgs e)
        {
            selectedAssemblyName = selectAssembly.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(selectedAssemblyName))
            {
                var assemblies = JarFileManager.Load();
                var selected = assemblies.Find(jar => jar.Name == selectedAssemblyName);

                if (selected != null)
                {
                    assemblies.Remove(selected);
                    selectAssembly.Text = null;
                    JarFileManager.Save(assemblies);
                    selectAssembly.SelectedIndex = 0;

                    MessageBox.Show("Сборка успешно удалена.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Не выбрана сборка.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            UpdateSelectAssembly();
        }

        private void EnableServerButtons(bool enabled)
        {
            startServer.Enabled = enabled;
            stopServer.Enabled = !enabled;
            restartServer.Enabled = !enabled;
            selectAssembly.Enabled = enabled;
            addAssembly.Enabled = enabled;
            deleteAssembly.Enabled = enabled;
        }
        
        private void selectLogLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (selectLogLevel.Text)
            {
                case "Весь вывод":
                    consoleWrite.Items.Clear();
                    foreach (var message in allMessage)
                    {
                        consoleWrite.Items.Add(message);
                    }
                    break;
                case "Только INFO":
                    consoleWrite.Items.Clear();
                    foreach (var message in infoMessages)
                    {
                        consoleWrite.Items.Add(message);
                    }
                    break;
                case "Только WARN":
                    consoleWrite.Items.Clear();
                    foreach (var message in warnMessages)
                    {
                        consoleWrite.Items.Add(message);
                    }
                    break;
                case "Только ERROR":
                    consoleWrite.Items.Clear();
                    foreach (var message in errorMessages)
                    {
                        consoleWrite.Items.Add(message);
                    }
                    break;
            }
        }

        private void killAllJava_Click(object sender, EventArgs e)
        {
            server.KillAllJava();
            EnableServerButtons(true);
        }

        private void MainServer_FormClosed(object sender, FormClosedEventArgs e)
        {
            server.KillIsProcessRunning(javaPath);
        }

        private void timerProcess_Tick(object sender, EventArgs e)
        {
            UpdateStatus();
            monitoring.MonitoringServerRam(javaPath);
            monitoring.CheckMonitoring();
        }

        private void exportLog_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.Title = "Сохранение логов";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        foreach (var item in consoleWrite.Items)
                        {
                            writer.WriteLine(item.ToString());
                        }
                    }

                    MessageBox.Show("Log file saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving log file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
