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

        private string javaPath, selectedJarFile, selectedJarName, selectedFolder, selectedAssemblyName, customStartupPath;
        private int ramMemory, WeUsedRam;
        public string startupFolder;

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

            Dictionary<string, string> serverProperties = ServerPropertiesManager.LoadServerProperties();
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
            memoryUsageServerLabel.Invoke((MethodInvoker)(() => memoryUsageServerLabel.Text = $"{memoryUsage} Мб / {(Settings.Default.isGbMax ? Settings.Default.selectMaxValueRAM * 1000 : Settings.Default.selectMaxValueRAM)} Мб"));
            memoryUsageServerBar.Invoke((MethodInvoker)(() => memoryUsageServerBar.Maximum = Settings.Default.isGbMax ? Settings.Default.selectMaxValueRAM * 1000 : Settings.Default.selectMaxValueRAM));
            if (memoryUsage < memoryUsageServerBar.Maximum)
            {
                memoryUsageServerBar.Invoke((MethodInvoker)(() => memoryUsageServerBar.Value = memoryUsage));
            }
            else
            {
                memoryUsageServerBar.Invoke((MethodInvoker)(() => memoryUsageServerBar.Value = Settings.Default.isGbMax ? Settings.Default.selectMaxValueRAM * 1000 : Settings.Default.selectMaxValueRAM));
            }
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
            if (selectAssembly.SelectedItem != null)
            {
                selectAssembly.SelectedItem = selectAssembly.Items.Count - 1;
            }
            else
            {
                selectAssembly.SelectedItem = 0;
            }
            
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
                    selectAssembly.SelectedItem = selectAssembly.Items.Count;
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
                        selectedAssemblyName = selected.ToString();
                        selectedJarName = selected.JarFileName;
                        selectedJarFile = selected.JarFilePath;
                        startupFolder = selected.Folder;
                        selectedFolder = $"{startupFolder}\\{selectedJarName}";
                        

                        consoleWrite.Items.Clear();
                        consoleWrite.Items.Add($"Название сборки: {selectedAssemblyName}");
                        consoleWrite.Items.Add($"Название JAR файла: {selectedJarName}");
                        consoleWrite.Items.Add($"Путь JAR файла: {selectedJarFile}");
                        consoleWrite.Items.Add($"Папка запуска: {startupFolder}");
                        consoleWrite.SelectedItem = consoleWrite.Items.Count - 1;
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

                    JarFileManager.Save(assemblies);


                    MessageBox.Show("Сборка успешно удалена.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Не выбрана сборка.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            UpdateSelectAssembly();
            if (selectAssembly.Items.Count > 0)
            {
                selectAssembly.SelectedItem = 0;
            }

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

        private void LoadServerProperties()
        {
            if (startupFolder != null)
            {
                ServerPropertiesManager.SetServerPropertiesPath(Path.Combine(startupFolder, "server.properties"));
            }

            Dictionary<string, string> properties = ServerPropertiesManager.LoadServerProperties();

            // настройки сервера
            if (properties.ContainsKey("level-name")) { levelNameTextBox.Text = properties["level-name"]; }

            if (properties.ContainsKey("motd")) { motdTextBox.Text = properties["motd"]; }

            if (properties.ContainsKey("debug")) { debugComboBox.Text = properties["debug"]; }

            if (properties.ContainsKey("broadcast-console-to-ops")) { broadcastCComboBox.Text = properties["broadcast-console-to-ops"]; }

            if (properties.ContainsKey("broadcast-rcon-to-ops")) { broadcastRconComboBox.Text = properties["broadcast-rcon-to-ops"]; }

            if (properties.ContainsKey("enable-command-block")) { commandBlockComboBox.Text = properties["enable-command-block"]; }

            if (properties.ContainsKey("enable-jmx-monitoring")) { jmxComboBox.Text = properties["enable-jmx-monitoring"]; }

            if (properties.ContainsKey("enable-query")) { queryComboBox.Text = properties["enable-query"]; }

            if (properties.ContainsKey("enable-rcon")) { rconComboBox.Text = properties["enable-rcon"]; }

            if (properties.ContainsKey("enable-status")) { statusComboBox.Text = properties["enable-status"]; }

            if (properties.ContainsKey("enforce-whitelist")) { EnforceWLComboBox.Text = properties["enforce-whitelist"]; }

            if (properties.ContainsKey("entity-broadcast-range-percentage")) { rangePercentageTextBox.Text = properties["entity-broadcast-range-percentage"]; }

            if (properties.ContainsKey("function-permission-level")) { permissionLevelTextBox.Text = properties["function-permission-level"]; }

            if (properties.ContainsKey("max-players")) { maxPlayerTextBox.Text = properties["max-players"]; }

            if (properties.ContainsKey("max-tick-time")) { tickTimeTextBox.Text = properties["max-tick-time"]; }

            if (properties.ContainsKey("network-compression-threshold")) { networkCompressionTextBox.Text = properties["network-compression-threshold"]; }

            if (properties.ContainsKey("online-mode")) { onlineModeComboBox.Text = properties["online-mode"]; }

            if (properties.ContainsKey("op-permission-level")) { opLevelComboBox.Text = properties["op-permission-level"]; }

            if (properties.ContainsKey("player-idle-timeout")) { playerTimeoutTextBox.Text = properties["player-idle-timeout"]; }

            if (properties.ContainsKey("max-world-size")) { worldSizeTextBox.Text = properties["max-world-size"]; }

            if (properties.ContainsKey("prevent-proxy-connections")) { proxyConnectionsComboBox.Text = properties["prevent-proxy-connections"]; }

            if (properties.ContainsKey("query.port")) { queryPortTextBox.Text = properties["query.port"]; }

            if (properties.ContainsKey("rate-limit")) { rateLimitTextBox.Text = properties["rate-limit"]; }

            if (properties.ContainsKey("rcon.password")) { rconPasswordTextBox.Text = properties["rcon.password"]; }

            if (properties.ContainsKey("rcon.port")) { rconPortTextBox.Text = properties["rcon.port"]; }

            if (properties.ContainsKey("require-resource-pack")) { requireResourcePComboBox.Text = properties["require-resource-pack"]; }

            if (properties.ContainsKey("resource-pack")) { resourcePackTextBox.Text = properties["resource-pack"]; }

            if (properties.ContainsKey("resource-pack-prompt")) { resourcePackPromptTextBox.Text = properties["resource-pack-prompt"]; }

            if (properties.ContainsKey("recource-pack-sha1")) { resourcePackShaTextBox.Text = properties["recource-pack-sha1"]; }

            if (properties.ContainsKey("server-ip")) { serverIPTextBox.Text = properties["server-ip"]; }

            if (properties.ContainsKey("server-port")) { serverPortTextBox.Text = properties["server-port"]; }

            if (properties.ContainsKey("white-list")) { whiteListComboBox.Text = properties["white-list"]; }

            //настройки мира
            if (properties.ContainsKey("allow-flight")) { allowflightComboBox.Text = properties["allow-flight"]; }

            if (properties.ContainsKey("allow-nether")) { allowNetherComboBox.Text = properties["allow-nether"]; }

            if (properties.ContainsKey("difficulty")) { difficultyComboBox.Text = properties["difficulty"]; }

            if (properties.ContainsKey("force-gamemode")) { forceGMComboBox.Text = properties["force-gamemode"]; }

            if (properties.ContainsKey("gamemode")) { gmComboBox.Text = properties["gamemode"]; }

            if (properties.ContainsKey("generate-structures")) { generateStructuresComboBox.Text = properties["generate-structures"]; }

            if (properties.ContainsKey("hardcore")) { hardcoreComboBox.Text = properties["hardcore"]; }

            if (properties.ContainsKey("level-seed")) { levelSeedTextBox.Text = properties["level-seed"]; }

            if (properties.ContainsKey("level-type")) { levelTypeComboBox.Text = properties["level-type"]; }

            if (properties.ContainsKey("pvp")) { pvpComboBox.Text = properties["pvp"]; }

            if (properties.ContainsKey("simulation-distance")) { simulationDistanceTextBox.Text = properties["simulation-distance"]; }

            if (properties.ContainsKey("spawn-animals")) { spawnAnimalsComboBox.Text = properties["spawn-animals"]; }

            if (properties.ContainsKey("spawn-monsters")) { spawnMonstersComboBox.Text = properties["spawn-monsters"]; }

            if (properties.ContainsKey("spawn-npcs")) { SpawnNPCsComboBox.Text = properties["spawn-npcs"]; }

            if (properties.ContainsKey("spawn-protection")) { spawnProtectionComboBox.Text = properties["spawn-protection"]; }

            if (properties.ContainsKey("sync-chunk-writes")) { syncChunkComboBox.Text = properties["sync-chunk-writes"]; }

            if (properties.ContainsKey("text-filtering-config")) { textFilteringTextBox.Text = properties["text-filtering-config"]; }

            if (properties.ContainsKey("use-native-transport")) { useNativeTransportComboBox.Text = properties["use-native-transport"]; }

            if (properties.ContainsKey("view-distance")) { viewDistanceTextBox.Text = properties["view-distance"]; }

            // Настройки приложения
            valueMaxRamNumeric.Value = Settings.Default.selectMaxValueRAM;
            selectMaxGBOrMBComboBox.Text = Settings.Default.selectMaxGBorMB;

            valueMinRamNumeric.Value = Settings.Default.selectMinValueRAM;
            selectMinGbOrMbComboBox.Text = Settings.Default.selectMinGBorMB;
        }

        private void SaveServerProperties()
        {
            Dictionary<string, string> existingProperties = ServerPropertiesManager.LoadServerProperties();

            // Настройки сервера
            existingProperties["level-name"] = levelNameTextBox.Text;
            existingProperties["motd"] = motdTextBox.Text;
            existingProperties["debug"] = debugComboBox.Text;
            existingProperties["broadcast-console-to-ops"] = broadcastCComboBox.Text;
            existingProperties["broadcast-rcon-to-ops"] = broadcastRconComboBox.Text;
            existingProperties["enable-command-block"] = commandBlockComboBox.Text;
            existingProperties["enable-jmx-monitoring"] = jmxComboBox.Text;
            existingProperties["enable-query"] = jmxComboBox.Text;
            existingProperties["enable-rcon"] = rconComboBox.Text;
            existingProperties["enable-status"] = statusComboBox.Text;
            existingProperties["enforce-whitelist"] = statusComboBox.Text;
            existingProperties["entity-broadcast-range-percentage"] = rangePercentageTextBox.Text;
            existingProperties["function-permission-level"] = permissionLevelTextBox.Text;
            existingProperties["max-players"] = maxPlayerTextBox.Text;
            existingProperties["max-tick-time"] = tickTimeTextBox.Text;
            existingProperties["network-compression-threshold"] = networkCompressionTextBox.Text;
            existingProperties["online-mode"] = onlineModeComboBox.Text;
            existingProperties["op-permission-level"] = opLevelComboBox.Text;
            existingProperties["player-idle-timeout"] = playerTimeoutTextBox.Text;
            existingProperties["max-world-size"] = worldSizeTextBox.Text;
            existingProperties["prevent-proxy-connections"] = proxyConnectionsComboBox.Text;
            existingProperties["query.port"] = queryPortTextBox.Text;
            existingProperties["rate-limit"] = rateLimitTextBox.Text;
            existingProperties["rcon.password"] = rconPasswordTextBox.Text;
            existingProperties["rcon.port"] = rconPortTextBox.Text;
            existingProperties["require-resource-pack"] = requireResourcePComboBox.Text;
            existingProperties["resource-pack"] = resourcePackTextBox.Text;
            existingProperties["resource-pack-prompt"] = resourcePackPromptTextBox.Text;
            existingProperties["recource-pack-sha1"] = resourcePackShaTextBox.Text;
            existingProperties["server-ip"] = serverIPTextBox.Text;
            existingProperties["server-port"] = serverPortTextBox.Text;
            existingProperties["white-list"] = whiteListComboBox.Text;

            // Настройки мира
            existingProperties["allow-flight"] = allowflightComboBox.Text;
            existingProperties["allow-nether"] = allowNetherComboBox.Text;
            existingProperties["difficulty"] = difficultyComboBox.Text;
            existingProperties["force-gamemode"] = forceGMComboBox.Text;
            existingProperties["gamemode"] = gmComboBox.Text;
            existingProperties["generate-structures"] = generateStructuresComboBox.Text;
            existingProperties["hardcore"] = hardcoreComboBox.Text;
            existingProperties["level-seed"] = levelSeedTextBox.Text;
            existingProperties["level-type"] = levelTypeComboBox.Text;
            existingProperties["pvp"] = pvpComboBox.Text;
            existingProperties["simulation-distance"] = pvpComboBox.Text;
            existingProperties["spawn-animals"] = pvpComboBox.Text;
            existingProperties["spawn-monsters"] = pvpComboBox.Text;
            existingProperties["spawn-npcs"] = SpawnNPCsComboBox.Text;
            existingProperties["spawn-protection"] = spawnProtectionComboBox.Text;
            existingProperties["sync-chunk-writes"] = syncChunkComboBox.Text;
            existingProperties["text-filtering-config"] = textFilteringTextBox.Text;
            existingProperties["use-native-transport"] = useNativeTransportComboBox.Text;
            existingProperties["view-distance"] = viewDistanceTextBox.Text;

            // Настройки приложения
            Settings.Default.selectMaxValueRAM = (int)valueMaxRamNumeric.Value;
            Settings.Default.selectMaxGBorMB = selectMaxGBOrMBComboBox.Text;

            Settings.Default.selectMinValueRAM = (int)valueMinRamNumeric.Value;
            Settings.Default.selectMinGBorMB = selectMinGbOrMbComboBox.Text;

            if (selectMaxGBOrMBComboBox.Text == "Gb")
            {
                Settings.Default.isGbMax = true;
                Settings.Default.startUpMaxGb = "-Xmx" + Settings.Default.selectMaxValueRAM + "G";
            }
            else
            {
                Settings.Default.isGbMax = false;
                Settings.Default.startUpMaxGb = "-Xmx" + Settings.Default.selectMaxValueRAM + "M";
            }

            if (selectMinGbOrMbComboBox.Text == "Gb")
            {
                Settings.Default.startUpMinGb = "-Xms" + Settings.Default.selectMinValueRAM + "G";
            }
            else
            {
                Settings.Default.startUpMinGb = "-Xms" + Settings.Default.selectMinValueRAM + "M";
            }

            try
            {
                ServerPropertiesManager.SaveServerProperties(existingProperties);
                MessageBox.Show("Настройки успешно сохранены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении настроек: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Settings.Default.Save();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveServerProperties();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            LoadServerProperties();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadServerProperties();
        }

        private void sendCommand_Click(object sender, EventArgs e)
        {
            string commandText = commandTextBox.Text;
            consoleWrite.Items.Add(commandText);
            server.sendCommand(javaPath, commandText);
            commandTextBox.Text = "";
        }

        private void commandTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                sendCommand_Click(sender, e);
            }
        }

        private void selectMaxGBOrMBComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectMaxGBOrMBComboBox.Text == "Gb")
            {
                valueMaxRamNumeric.Maximum = 64;
            }
            else
            {
                valueMaxRamNumeric.Maximum = 64000;
            }
        }

        private void selectMinGbOrMbComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectMinGbOrMbComboBox.Text == "Gb")
            {
                valueMinRamNumeric.Maximum = 32;
            }
            else
            {
                valueMinRamNumeric.Maximum = 32000;
            }
        }
    }
}
