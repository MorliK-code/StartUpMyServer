namespace StartUpMyServer
{
    partial class MainServer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            groupBox3 = new GroupBox();
            consoleWrite = new ListBox();
            groupBox7 = new GroupBox();
            groupBox8 = new GroupBox();
            selectLogLevel = new ComboBox();
            exportLog = new Button();
            groupBox2 = new GroupBox();
            killAllJava = new Button();
            killandrestartServer = new Button();
            killServer = new Button();
            groupBox6 = new GroupBox();
            groupBox10 = new GroupBox();
            cpuUsageBar1 = new ProgressBar();
            cpuUsageLabel = new Label();
            label2 = new Label();
            groupBox9 = new GroupBox();
            memoryUsageServerLabel = new Label();
            memoryUsageLabel = new Label();
            label3 = new Label();
            label1 = new Label();
            memoryUsageServerBar = new ProgressBar();
            memoryUsageBar = new ProgressBar();
            groupBox5 = new GroupBox();
            selectAssembly = new ComboBox();
            deleteAssembly = new Button();
            addAssembly = new Button();
            groupBox4 = new GroupBox();
            progressStartUp = new ProgressBar();
            statusServer = new Label();
            statusLabelText = new Label();
            groupBox1 = new GroupBox();
            restartServer = new Button();
            stopServer = new Button();
            startServer = new Button();
            tabPage2 = new TabPage();
            openFileDialog1 = new OpenFileDialog();
            timerProcess = new System.Windows.Forms.Timer(components);
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox10.SuspendLayout();
            groupBox9.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1082, 604);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(groupBox3);
            tabPage1.Controls.Add(groupBox7);
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(groupBox6);
            tabPage1.Controls.Add(groupBox5);
            tabPage1.Controls.Add(groupBox4);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1074, 576);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Сеть";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(consoleWrite);
            groupBox3.Location = new Point(316, 17);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(750, 551);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Console";
            // 
            // consoleWrite
            // 
            consoleWrite.FormattingEnabled = true;
            consoleWrite.ItemHeight = 15;
            consoleWrite.Location = new Point(6, 16);
            consoleWrite.Name = "consoleWrite";
            consoleWrite.Size = new Size(738, 529);
            consoleWrite.TabIndex = 0;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(groupBox8);
            groupBox7.Controls.Add(exportLog);
            groupBox7.Location = new Point(167, 141);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(143, 115);
            groupBox7.TabIndex = 1;
            groupBox7.TabStop = false;
            groupBox7.Text = "Логи";
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(selectLogLevel);
            groupBox8.Location = new Point(7, 51);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(127, 56);
            groupBox8.TabIndex = 1;
            groupBox8.TabStop = false;
            groupBox8.Text = "Вывод";
            // 
            // selectLogLevel
            // 
            selectLogLevel.FormattingEnabled = true;
            selectLogLevel.Items.AddRange(new object[] { "Весь вывод", "Только INFO", "Только WARN", "Только ERROR" });
            selectLogLevel.Location = new Point(6, 22);
            selectLogLevel.Name = "selectLogLevel";
            selectLogLevel.Size = new Size(115, 23);
            selectLogLevel.TabIndex = 3;
            selectLogLevel.SelectedIndexChanged += selectLogLevel_SelectedIndexChanged;
            // 
            // exportLog
            // 
            exportLog.Location = new Point(7, 22);
            exportLog.Name = "exportLog";
            exportLog.Size = new Size(131, 25);
            exportLog.TabIndex = 0;
            exportLog.Text = "Экспорт логов";
            exportLog.UseVisualStyleBackColor = true;
            exportLog.Click += exportLog_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(killAllJava);
            groupBox2.Controls.Add(killandrestartServer);
            groupBox2.Controls.Add(killServer);
            groupBox2.Location = new Point(18, 141);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(143, 115);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Задачи";
            // 
            // killAllJava
            // 
            killAllJava.Location = new Point(6, 82);
            killAllJava.Name = "killAllJava";
            killAllJava.Size = new Size(131, 25);
            killAllJava.TabIndex = 0;
            killAllJava.Text = "Убить все Java";
            killAllJava.UseVisualStyleBackColor = true;
            killAllJava.Click += killAllJava_Click;
            // 
            // killandrestartServer
            // 
            killandrestartServer.Location = new Point(6, 51);
            killandrestartServer.Name = "killandrestartServer";
            killandrestartServer.Size = new Size(131, 25);
            killandrestartServer.TabIndex = 0;
            killandrestartServer.Text = "Перезапуск";
            killandrestartServer.UseVisualStyleBackColor = true;
            killandrestartServer.Click += killandrestartServer_Click;
            // 
            // killServer
            // 
            killServer.Location = new Point(6, 22);
            killServer.Name = "killServer";
            killServer.Size = new Size(131, 25);
            killServer.TabIndex = 0;
            killServer.Text = "Убить";
            killServer.UseVisualStyleBackColor = true;
            killServer.Click += killServer_Click;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(groupBox10);
            groupBox6.Controls.Add(groupBox9);
            groupBox6.Location = new Point(18, 328);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(292, 240);
            groupBox6.TabIndex = 0;
            groupBox6.TabStop = false;
            groupBox6.Text = "Использование данных";
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(cpuUsageBar1);
            groupBox10.Controls.Add(cpuUsageLabel);
            groupBox10.Controls.Add(label2);
            groupBox10.Location = new Point(7, 112);
            groupBox10.Name = "groupBox10";
            groupBox10.Size = new Size(280, 75);
            groupBox10.TabIndex = 7;
            groupBox10.TabStop = false;
            groupBox10.Text = "ЦП";
            // 
            // cpuUsageBar1
            // 
            cpuUsageBar1.Location = new Point(6, 43);
            cpuUsageBar1.Name = "cpuUsageBar1";
            cpuUsageBar1.Size = new Size(265, 17);
            cpuUsageBar1.Step = 1;
            cpuUsageBar1.TabIndex = 7;
            // 
            // cpuUsageLabel
            // 
            cpuUsageLabel.AutoSize = true;
            cpuUsageLabel.Location = new Point(144, 19);
            cpuUsageLabel.Name = "cpuUsageLabel";
            cpuUsageLabel.Size = new Size(65, 15);
            cpuUsageLabel.TabIndex = 6;
            cpuUsageLabel.Text = "0 % / 100%";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 19);
            label2.Name = "label2";
            label2.Size = new Size(132, 15);
            label2.TabIndex = 6;
            label2.Text = "Используется/Общий:";
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(memoryUsageServerLabel);
            groupBox9.Controls.Add(memoryUsageLabel);
            groupBox9.Controls.Add(label3);
            groupBox9.Controls.Add(label1);
            groupBox9.Controls.Add(memoryUsageServerBar);
            groupBox9.Controls.Add(memoryUsageBar);
            groupBox9.Location = new Point(7, 16);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(280, 94);
            groupBox9.TabIndex = 7;
            groupBox9.TabStop = false;
            groupBox9.Text = "ОЗУ";
            // 
            // memoryUsageServerLabel
            // 
            memoryUsageServerLabel.AutoSize = true;
            memoryUsageServerLabel.Location = new Point(135, 54);
            memoryUsageServerLabel.Name = "memoryUsageServerLabel";
            memoryUsageServerLabel.Size = new Size(90, 15);
            memoryUsageServerLabel.TabIndex = 6;
            memoryUsageServerLabel.Text = "0 Мб / 4000 Мб";
            // 
            // memoryUsageLabel
            // 
            memoryUsageLabel.AutoSize = true;
            memoryUsageLabel.Location = new Point(135, 18);
            memoryUsageLabel.Name = "memoryUsageLabel";
            memoryUsageLabel.Size = new Size(96, 15);
            memoryUsageLabel.TabIndex = 6;
            memoryUsageLabel.Text = "0 Мб / 16000 Мб";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 54);
            label3.Name = "label3";
            label3.Size = new Size(132, 15);
            label3.TabIndex = 6;
            label3.Text = "Используется/Общий:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 18);
            label1.Name = "label1";
            label1.Size = new Size(132, 15);
            label1.TabIndex = 6;
            label1.Text = "Используется/Общий:";
            // 
            // memoryUsageServerBar
            // 
            memoryUsageServerBar.Location = new Point(6, 72);
            memoryUsageServerBar.MarqueeAnimationSpeed = 10000;
            memoryUsageServerBar.Maximum = 4000;
            memoryUsageServerBar.Name = "memoryUsageServerBar";
            memoryUsageServerBar.Size = new Size(265, 15);
            memoryUsageServerBar.Step = 1;
            memoryUsageServerBar.Style = ProgressBarStyle.Continuous;
            memoryUsageServerBar.TabIndex = 5;
            // 
            // memoryUsageBar
            // 
            memoryUsageBar.Location = new Point(6, 36);
            memoryUsageBar.MarqueeAnimationSpeed = 10000;
            memoryUsageBar.Maximum = 16000;
            memoryUsageBar.Name = "memoryUsageBar";
            memoryUsageBar.Size = new Size(265, 15);
            memoryUsageBar.Step = 1;
            memoryUsageBar.Style = ProgressBarStyle.Continuous;
            memoryUsageBar.TabIndex = 5;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(selectAssembly);
            groupBox5.Controls.Add(deleteAssembly);
            groupBox5.Controls.Add(addAssembly);
            groupBox5.Location = new Point(18, 262);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(292, 60);
            groupBox5.TabIndex = 0;
            groupBox5.TabStop = false;
            groupBox5.Text = "Выбор сборки";
            // 
            // selectAssembly
            // 
            selectAssembly.FormattingEnabled = true;
            selectAssembly.Location = new Point(6, 22);
            selectAssembly.Name = "selectAssembly";
            selectAssembly.Size = new Size(211, 23);
            selectAssembly.TabIndex = 3;
            selectAssembly.SelectedIndexChanged += selectAssembly_SelectedIndexChanged;
            // 
            // deleteAssembly
            // 
            deleteAssembly.Location = new Point(256, 20);
            deleteAssembly.Name = "deleteAssembly";
            deleteAssembly.Size = new Size(27, 25);
            deleteAssembly.TabIndex = 0;
            deleteAssembly.Text = "-";
            deleteAssembly.UseVisualStyleBackColor = true;
            deleteAssembly.Click += deleteAssembly_Click;
            // 
            // addAssembly
            // 
            addAssembly.Location = new Point(223, 20);
            addAssembly.Name = "addAssembly";
            addAssembly.Size = new Size(27, 25);
            addAssembly.TabIndex = 0;
            addAssembly.Text = "+";
            addAssembly.UseVisualStyleBackColor = true;
            addAssembly.Click += addAssembly_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(progressStartUp);
            groupBox4.Controls.Add(statusServer);
            groupBox4.Controls.Add(statusLabelText);
            groupBox4.Location = new Point(167, 17);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(143, 118);
            groupBox4.TabIndex = 0;
            groupBox4.TabStop = false;
            groupBox4.Text = "Статус";
            // 
            // progressStartUp
            // 
            progressStartUp.Location = new Point(6, 37);
            progressStartUp.Name = "progressStartUp";
            progressStartUp.Size = new Size(131, 17);
            progressStartUp.Style = ProgressBarStyle.Continuous;
            progressStartUp.TabIndex = 4;
            // 
            // statusServer
            // 
            statusServer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            statusServer.AutoSize = true;
            statusServer.BackColor = Color.Transparent;
            statusServer.Location = new Point(55, 19);
            statusServer.Name = "statusServer";
            statusServer.Size = new Size(73, 15);
            statusServer.TabIndex = 3;
            statusServer.Text = "Не запущен";
            statusServer.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // statusLabelText
            // 
            statusLabelText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            statusLabelText.AutoSize = true;
            statusLabelText.Location = new Point(7, 19);
            statusLabelText.Name = "statusLabelText";
            statusLabelText.Size = new Size(50, 15);
            statusLabelText.TabIndex = 3;
            statusLabelText.Text = "Сервер:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(restartServer);
            groupBox1.Controls.Add(stopServer);
            groupBox1.Controls.Add(startServer);
            groupBox1.Location = new Point(18, 17);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(143, 118);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Сервер";
            // 
            // restartServer
            // 
            restartServer.Location = new Point(6, 84);
            restartServer.Name = "restartServer";
            restartServer.Size = new Size(131, 25);
            restartServer.TabIndex = 0;
            restartServer.Text = "Рестарт";
            restartServer.UseVisualStyleBackColor = true;
            restartServer.Click += restartServer_Click;
            // 
            // stopServer
            // 
            stopServer.Location = new Point(6, 53);
            stopServer.Name = "stopServer";
            stopServer.Size = new Size(131, 25);
            stopServer.TabIndex = 0;
            stopServer.Text = "Остановка";
            stopServer.UseVisualStyleBackColor = true;
            stopServer.Click += stopServer_Click;
            // 
            // startServer
            // 
            startServer.Location = new Point(6, 22);
            startServer.Name = "startServer";
            startServer.Size = new Size(131, 25);
            startServer.TabIndex = 0;
            startServer.Text = "Запуск";
            startServer.UseVisualStyleBackColor = true;
            startServer.Click += startServer_Click;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1074, 576);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Настройки сервера";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "JAR Files|*.jar";
            // 
            // timerProcess
            // 
            timerProcess.Enabled = true;
            timerProcess.Interval = 500;
            timerProcess.Tick += timerProcess_Tick;
            // 
            // MainServer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1082, 604);
            Controls.Add(tabControl1);
            Name = "MainServer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "StartUpMyServer";
            FormClosing += MainServer_FormClosing;
            FormClosed += MainServer_FormClosed;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox8.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox10.ResumeLayout(false);
            groupBox10.PerformLayout();
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private GroupBox groupBox1;
        private Button killServer;
        private Button stopServer;
        private Button startServer;
        private Button restartServer;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private Button killandrestartServer;
        private GroupBox groupBox4;
        private ComboBox selectAssembly;
        private ListBox consoleWrite;
        private GroupBox groupBox5;
        private Button addAssembly;
        private Button deleteAssembly;
        private OpenFileDialog openFileDialog1;
        private Label statusLabelText;
        private Label statusServer;
        public ProgressBar progressStartUp;
        private Button killAllJava;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        private Button exportLog;
        private GroupBox groupBox8;
        private ComboBox selectLogLevel;
        private GroupBox groupBox10;
        private Label label2;
        private GroupBox groupBox9;
        private Label label1;
        private System.Windows.Forms.Timer timerProcess;
        private ProgressBar memoryUsageBar;
        private Label memoryUsageLabel;
        private Label label3;
        private ProgressBar memoryUsageServerBar;
        private Label memoryUsageServerLabel;
        private Label cpuUsageLabel;
        private ProgressBar cpuUsageBar1;
    }
}
