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
        public string selectedFolderForm { get; private set; }

        public AddAssemblyForm(string assemblyName, string jarFilePath, string selectedFolder)
        {
            InitializeComponent();
            AssemblyName = assemblyName;
            JarFilePath = jarFilePath;
            selectedFolderForm = selectedFolder;
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
                jarFileName.Text = "";
                JarFilePath = openFileDialog1.FileName;
                LoadFormData();
            }
        }

        private void addStartupFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedFolderForm = folderBrowserDialog1.SelectedPath;
                startupFolder.Text = "";
                startupFolder.Text = selectedFolderForm;
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
                MessageBox.Show("Введіть назву збірки.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(JarFileName))
            {
                MessageBox.Show("Оберіть JAR файл.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (customFolder.Checked && string.IsNullOrWhiteSpace(selectedFolderForm))
            {
                MessageBox.Show("Оберіть місце запуску сервера.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            selectedFolderForm = startupFolder.Text;
            return true;
        }

        private void SaveJarFile()
        {
            JarFile newJarFile = new JarFile
            {
                Name = AssemblyName,
                JarFileName = JarFileName,
                JarFilePath = JarFilePath,
                Folder = selectedFolderForm
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
