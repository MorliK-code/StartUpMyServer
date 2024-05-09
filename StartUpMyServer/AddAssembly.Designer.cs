namespace StartUpMyServer
{
    partial class AddAssemblyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            addJar = new Button();
            customFolder = new CheckBox();
            assemblyName = new TextBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            assemblySave = new Button();
            label3 = new Label();
            label2 = new Label();
            startupFolder = new TextBox();
            jarFileName = new TextBox();
            addStartupFolder = new Button();
            openFileDialog1 = new OpenFileDialog();
            folderBrowserDialog1 = new FolderBrowserDialog();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // addJar
            // 
            addJar.Location = new Point(197, 37);
            addJar.Name = "addJar";
            addJar.Size = new Size(39, 23);
            addJar.TabIndex = 2;
            addJar.Text = "...";
            addJar.UseVisualStyleBackColor = true;
            addJar.Click += addJar_Click;
            // 
            // customFolder
            // 
            customFolder.AutoSize = true;
            customFolder.Location = new Point(52, 119);
            customFolder.Name = "customFolder";
            customFolder.Size = new Size(133, 19);
            customFolder.TabIndex = 3;
            customFolder.Text = "Своя папка запуска";
            customFolder.UseVisualStyleBackColor = true;
            customFolder.CheckedChanged += customFolder_CheckedChanged;
            // 
            // assemblyName
            // 
            assemblyName.Location = new Point(6, 35);
            assemblyName.Name = "assemblyName";
            assemblyName.Size = new Size(230, 23);
            assemblyName.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(70, 17);
            label1.Name = "label1";
            label1.Size = new Size(102, 15);
            label1.TabIndex = 6;
            label1.Text = "Название сборки";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(assemblyName);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(243, 67);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Название сборки";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(assemblySave);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(customFolder);
            groupBox2.Controls.Add(startupFolder);
            groupBox2.Controls.Add(jarFileName);
            groupBox2.Controls.Add(addStartupFolder);
            groupBox2.Controls.Add(addJar);
            groupBox2.Location = new Point(12, 85);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(243, 181);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "Сервер";
            // 
            // assemblySave
            // 
            assemblySave.Location = new Point(68, 144);
            assemblySave.Name = "assemblySave";
            assemblySave.Size = new Size(104, 23);
            assemblySave.TabIndex = 7;
            assemblySave.Text = "Создать сборку";
            assemblySave.UseVisualStyleBackColor = true;
            assemblySave.Click += assemblySave_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(60, 69);
            label3.Name = "label3";
            label3.Size = new Size(125, 15);
            label3.TabIndex = 6;
            label3.Text = "Выбор папки запуска";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(68, 19);
            label2.Name = "label2";
            label2.Size = new Size(104, 15);
            label2.TabIndex = 6;
            label2.Text = "Выбор JAR файла";
            // 
            // startupFolder
            // 
            startupFolder.Enabled = false;
            startupFolder.Location = new Point(6, 90);
            startupFolder.Name = "startupFolder";
            startupFolder.Size = new Size(185, 23);
            startupFolder.TabIndex = 5;
            // 
            // jarFileName
            // 
            jarFileName.Location = new Point(6, 37);
            jarFileName.Name = "jarFileName";
            jarFileName.Size = new Size(185, 23);
            jarFileName.TabIndex = 5;
            // 
            // addStartupFolder
            // 
            addStartupFolder.Enabled = false;
            addStartupFolder.Location = new Point(197, 90);
            addStartupFolder.Name = "addStartupFolder";
            addStartupFolder.Size = new Size(39, 23);
            addStartupFolder.TabIndex = 2;
            addStartupFolder.Text = "...";
            addStartupFolder.UseVisualStyleBackColor = true;
            addStartupFolder.Click += addStartupFolder_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "JAR file | *.jar";
            // 
            // AddAssemblyForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(270, 278);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddAssemblyForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Редактор сборки";
            TopMost = true;
            Load += AddAssemblyForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button addJar;
        private CheckBox customFolder;
        private TextBox assemblyName;
        private Label label1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label2;
        private TextBox jarFileName;
        private Button assemblySave;
        private Label label3;
        private TextBox startupFolder;
        private Button addStartupFolder;
        private OpenFileDialog openFileDialog1;
        private FolderBrowserDialog folderBrowserDialog1;
    }
}