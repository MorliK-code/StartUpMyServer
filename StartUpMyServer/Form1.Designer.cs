namespace StartUpMyServer
{
    partial class Form1
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            groupBox3 = new GroupBox();
            consoleWrite = new ListBox();
            groupBox2 = new GroupBox();
            button5 = new Button();
            button3 = new Button();
            groupBox5 = new GroupBox();
            comboBox1 = new ComboBox();
            button7 = new Button();
            button6 = new Button();
            groupBox4 = new GroupBox();
            statusLabel = new Label();
            statusLabelText = new Label();
            groupBox1 = new GroupBox();
            button4 = new Button();
            button2 = new Button();
            button1 = new Button();
            tabPage2 = new TabPage();
            openFileDialog1 = new OpenFileDialog();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
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
            tabPage1.Controls.Add(groupBox2);
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
            groupBox3.Size = new Size(750, 360);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Console";
            // 
            // consoleWrite
            // 
            consoleWrite.FormattingEnabled = true;
            consoleWrite.ItemHeight = 15;
            consoleWrite.Location = new Point(6, 22);
            consoleWrite.Name = "consoleWrite";
            consoleWrite.Size = new Size(738, 334);
            consoleWrite.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(button5);
            groupBox2.Controls.Add(button3);
            groupBox2.Location = new Point(18, 141);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(143, 86);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Задачи";
            // 
            // button5
            // 
            button5.Location = new Point(6, 51);
            button5.Name = "button5";
            button5.Size = new Size(131, 25);
            button5.TabIndex = 0;
            button5.Text = "Убить все";
            button5.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(6, 22);
            button3.Name = "button3";
            button3.Size = new Size(131, 25);
            button3.TabIndex = 0;
            button3.Text = "Убить";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(comboBox1);
            groupBox5.Controls.Add(button7);
            groupBox5.Controls.Add(button6);
            groupBox5.Location = new Point(18, 233);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(292, 60);
            groupBox5.TabIndex = 0;
            groupBox5.TabStop = false;
            groupBox5.Text = "Выбор сборки";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(6, 22);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(211, 23);
            comboBox1.TabIndex = 3;
            // 
            // button7
            // 
            button7.Location = new Point(256, 20);
            button7.Name = "button7";
            button7.Size = new Size(27, 25);
            button7.TabIndex = 0;
            button7.Text = "-";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button6
            // 
            button6.Location = new Point(223, 20);
            button6.Name = "button6";
            button6.Size = new Size(27, 25);
            button6.TabIndex = 0;
            button6.Text = "+";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(statusLabel);
            groupBox4.Controls.Add(statusLabelText);
            groupBox4.Location = new Point(167, 17);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(143, 118);
            groupBox4.TabIndex = 0;
            groupBox4.TabStop = false;
            groupBox4.Text = "Статус";
            // 
            // statusLabel
            // 
            statusLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(61, 19);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(73, 15);
            statusLabel.TabIndex = 3;
            statusLabel.Text = "Не запущен";
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
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Location = new Point(18, 17);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(143, 118);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Сервер";
            // 
            // button4
            // 
            button4.Location = new Point(6, 84);
            button4.Name = "button4";
            button4.Size = new Size(131, 25);
            button4.TabIndex = 0;
            button4.Text = "Рестарт";
            button4.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(6, 53);
            button2.Name = "button2";
            button2.Size = new Size(131, 25);
            button2.TabIndex = 0;
            button2.Text = "Остановка";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(6, 22);
            button1.Name = "button1";
            button1.Size = new Size(131, 25);
            button1.TabIndex = 0;
            button1.Text = "Запуск";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1074, 576);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "JAR Files|*.jar";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1082, 604);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
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
        private Button button3;
        private Button button2;
        private Button button1;
        private Button button4;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private Button button5;
        private GroupBox groupBox4;
        private ComboBox comboBox1;
        private ListBox consoleWrite;
        private GroupBox groupBox5;
        private Button button6;
        private Button button7;
        private OpenFileDialog openFileDialog1;
        private Label statusLabelText;
        private Label statusLabel;
    }
}
