using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace StartUpMyServer
{
    public partial class AddAssemblyForm : Form
    {
        public string AssemblyName { get; private set; }
        public string JarFileName { get; private set; }
        public string JarFilePath { get; private set; }
        public string selectedFolder { get; private set; }

        public AddAssemblyForm(string assemblyName, string jarFilePath, string selectedFolder)
        {
            InitializeComponent();
            AssemblyName = assemblyName;
            JarFilePath = jarFilePath;
            selectedFolder = selectedFolder;
        }

        private void AddAssemblyForm_Load(object sender, EventArgs e)
        {
            LoadFormData();
        }

        private void LoadFormData()
        {
            if (string.IsNullOrEmpty(assemblyName.Text))
            {
                assemblyName.Text = AssemblyName;
            }
            JarFileName = Path.GetFileName(JarFilePath);
            if (string.IsNullOrEmpty(jarFileName.Text))
            {
                jarFileName.Text = JarFileName;
            }
            startupFolder.Text = Path.GetDirectoryName(JarFilePath);
        }

        private void addJar_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                JarFilePath = openFileDialog1.FileName;
                LoadFormData();
            }
        }

        private void addStartupFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedFolder = folderBrowserDialog1.SelectedPath;
                startupFolder.Text = selectedFolder;
            }
        }

        private void assemblySave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            SaveJarFile();
            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidateForm()
        {
            AssemblyName = assemblyName.Text.Trim();
            if (string.IsNullOrWhiteSpace(AssemblyName))
            {
                MessageBox.Show("Введите название сборки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(JarFileName))
            {
                MessageBox.Show("Выберите JAR файл.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (customFolder.Checked && string.IsNullOrWhiteSpace(selectedFolder))
            {
                MessageBox.Show("Выберите путь для запуска сборки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            selectedFolder = startupFolder.Text;
            return true;
        }

        private void SaveJarFile()
        {
            JarFile newJarFile = new JarFile
            {
                Name = AssemblyName,
                JarFileName = JarFileName,
                JarFilePath = JarFilePath,
                Folder = selectedFolder
            };

            List<JarFile> jarFiles = JarFileManager.Load();
            jarFiles.Add(newJarFile);
            JarFileManager.Save(jarFiles);
        }

        private void customFolder_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = customFolder.Checked;
            startupFolder.Enabled = isChecked;
            addStartupFolder.Enabled = isChecked;
        }
    }
}
