﻿namespace cours_m2G
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button6 = new System.Windows.Forms.Button();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.методРендераToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.растеризацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Interpolation = new System.Windows.Forms.ToolStripMenuItem();
            this.Bariscentric = new System.Windows.Forms.ToolStripMenuItem();
            this.NoCutter = new System.Windows.Forms.ToolStripMenuItem();
            this.трассировкаЛучейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Parallel = new System.Windows.Forms.ToolStripMenuItem();
            this.StepbyStep = new System.Windows.Forms.ToolStripMenuItem();
            this.выборЭлементовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Polys = new System.Windows.Forms.ToolStripMenuItem();
            this.Lines = new System.Windows.Forms.ToolStripMenuItem();
            this.Points = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button8 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button13 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.BackgroundImage = global::cours_m2G.Properties.Resources._1639537185_31_papik_pro_p_setka_risunok_33;
            this.pictureBox2.Location = new System.Drawing.Point(8, 26);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(831, 432);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            this.pictureBox2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseMove);
            this.pictureBox2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseUp);
            // 
            // button6
            // 
            this.button6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button6.Location = new System.Drawing.Point(991, 49);
            this.button6.Margin = new System.Windows.Forms.Padding(2);
            this.button6.MaximumSize = new System.Drawing.Size(71, 48);
            this.button6.MinimumSize = new System.Drawing.Size(71, 48);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(71, 48);
            this.button6.TabIndex = 20;
            this.button6.Text = "Сделать активным";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // comboBox5
            // 
            this.comboBox5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Location = new System.Drawing.Point(851, 49);
            this.comboBox5.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox5.MaximumSize = new System.Drawing.Size(129, 0);
            this.comboBox5.MinimumSize = new System.Drawing.Size(129, 0);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(129, 23);
            this.comboBox5.TabIndex = 27;
            // 
            // button5
            // 
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button5.Location = new System.Drawing.Point(843, 318);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(125, 25);
            this.button5.TabIndex = 20;
            this.button5.Text = "Возврат фигуры";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button4.Location = new System.Drawing.Point(843, 357);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(235, 29);
            this.button4.TabIndex = 21;
            this.button4.Text = "Выбрать трансформирование";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Location = new System.Drawing.Point(8, 155);
            this.panel1.Margin = new System.Windows.Forms.Padding(1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0, 0);
            this.panel1.TabIndex = 29;
            // 
            // button11
            // 
            this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button11.Location = new System.Drawing.Point(8, 504);
            this.button11.Margin = new System.Windows.Forms.Padding(2);
            this.button11.MaximumSize = new System.Drawing.Size(115, 43);
            this.button11.MinimumSize = new System.Drawing.Size(115, 43);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(115, 43);
            this.button11.TabIndex = 31;
            this.button11.Text = "Остановить отрисовку";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button12.Location = new System.Drawing.Point(127, 504);
            this.button12.Margin = new System.Windows.Forms.Padding(2);
            this.button12.MaximumSize = new System.Drawing.Size(94, 43);
            this.button12.MinimumSize = new System.Drawing.Size(94, 43);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(94, 43);
            this.button12.TabIndex = 32;
            this.button12.Text = "Возобновить отрисовку";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.методРендераToolStripMenuItem,
            this.выборЭлементовToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1182, 24);
            this.menuStrip1.TabIndex = 34;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem,
            this.сохранитьToolStripMenuItem});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(48, 22);
            this.toolStripMenuItem3.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // методРендераToolStripMenuItem
            // 
            this.методРендераToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.растеризацияToolStripMenuItem,
            this.трассировкаЛучейToolStripMenuItem});
            this.методРендераToolStripMenuItem.Name = "методРендераToolStripMenuItem";
            this.методРендераToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.методРендераToolStripMenuItem.Text = "Метод рендера";
            // 
            // растеризацияToolStripMenuItem
            // 
            this.растеризацияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Interpolation,
            this.Bariscentric,
            this.NoCutter});
            this.растеризацияToolStripMenuItem.Name = "растеризацияToolStripMenuItem";
            this.растеризацияToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.растеризацияToolStripMenuItem.Text = "Растеризация";
            // 
            // Interpolation
            // 
            this.Interpolation.Checked = true;
            this.Interpolation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Interpolation.Name = "Interpolation";
            this.Interpolation.Size = new System.Drawing.Size(248, 22);
            this.Interpolation.Text = "Интерполяция";
            this.Interpolation.Click += new System.EventHandler(this.Interpolation_Click);
            // 
            // Bariscentric
            // 
            this.Bariscentric.Name = "Bariscentric";
            this.Bariscentric.Size = new System.Drawing.Size(248, 22);
            this.Bariscentric.Text = "Барицентрические координаты";
            this.Bariscentric.Click += new System.EventHandler(this.Bariscentric_Click);
            // 
            // NoCutter
            // 
            this.NoCutter.Name = "NoCutter";
            this.NoCutter.Size = new System.Drawing.Size(248, 22);
            this.NoCutter.Text = "Без отсечения";
            this.NoCutter.Click += new System.EventHandler(this.NoCutter_Click);
            // 
            // трассировкаЛучейToolStripMenuItem
            // 
            this.трассировкаЛучейToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Parallel,
            this.StepbyStep});
            this.трассировкаЛучейToolStripMenuItem.Name = "трассировкаЛучейToolStripMenuItem";
            this.трассировкаЛучейToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.трассировкаЛучейToolStripMenuItem.Text = "Трассировка лучей";
            // 
            // Parallel
            // 
            this.Parallel.Name = "Parallel";
            this.Parallel.Size = new System.Drawing.Size(177, 22);
            this.Parallel.Text = "Параллельная";
            this.Parallel.Click += new System.EventHandler(this.Parallel_Click);
            // 
            // StepbyStep
            // 
            this.StepbyStep.Name = "StepbyStep";
            this.StepbyStep.Size = new System.Drawing.Size(177, 22);
            this.StepbyStep.Text = "Последовательная";
            this.StepbyStep.Click += new System.EventHandler(this.StepbyStep_Click);
            // 
            // выборЭлементовToolStripMenuItem
            // 
            this.выборЭлементовToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Polys,
            this.Lines,
            this.Points});
            this.выборЭлементовToolStripMenuItem.Name = "выборЭлементовToolStripMenuItem";
            this.выборЭлементовToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.выборЭлементовToolStripMenuItem.Text = "Выбор элементов";
            // 
            // Polys
            // 
            this.Polys.Checked = true;
            this.Polys.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Polys.Name = "Polys";
            this.Polys.Size = new System.Drawing.Size(132, 22);
            this.Polys.Text = "Полигоны";
            this.Polys.Click += new System.EventHandler(this.Polys_Click);
            // 
            // Lines
            // 
            this.Lines.Name = "Lines";
            this.Lines.Size = new System.Drawing.Size(132, 22);
            this.Lines.Text = "Ребра";
            this.Lines.Click += new System.EventHandler(this.Lines_Click);
            // 
            // Points
            // 
            this.Points.Name = "Points";
            this.Points.Size = new System.Drawing.Size(132, 22);
            this.Points.Text = "Вершины";
            this.Points.Click += new System.EventHandler(this.Points_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.Location = new System.Drawing.Point(843, 289);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 25);
            this.button1.TabIndex = 35;
            this.button1.Text = "Возврат камеры";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 478);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.MaximumSize = new System.Drawing.Size(218, 15);
            this.label1.MinimumSize = new System.Drawing.Size(218, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 15);
            this.label1.TabIndex = 36;
            this.label1.Text = "Время отрисовки предыдущей сцены:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(241, 478);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.MinimumSize = new System.Drawing.Size(38, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 37;
            this.label2.Text = "label2";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(843, 119);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(315, 166);
            this.tabControl1.TabIndex = 44;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.button7);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.textBox3);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(307, 138);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Добавить полигон";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(29, 98);
            this.button8.Margin = new System.Windows.Forms.Padding(2);
            this.button8.MaximumSize = new System.Drawing.Size(128, 20);
            this.button8.MinimumSize = new System.Drawing.Size(128, 20);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(128, 20);
            this.button8.TabIndex = 12;
            this.button8.Text = "Добавить полигон";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(144, 71);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(12, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "/";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(112, 68);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.MaximumSize = new System.Drawing.Size(28, 20);
            this.button7.MinimumSize = new System.Drawing.Size(28, 20);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(28, 20);
            this.button7.TabIndex = 10;
            this.button7.Text = "ок";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(155, 70);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.MaximumSize = new System.Drawing.Size(151, 15);
            this.label8.MinimumSize = new System.Drawing.Size(151, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 15);
            this.label8.TabIndex = 9;
            this.label8.Text = "Добавить существующую";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(4, 68);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2);
            this.textBox3.MaximumSize = new System.Drawing.Size(105, 23);
            this.textBox3.MinimumSize = new System.Drawing.Size(105, 23);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(105, 23);
            this.textBox3.TabIndex = 8;
            this.textBox3.Text = "Добавить новую";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(144, 44);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "/";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(112, 41);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.MaximumSize = new System.Drawing.Size(28, 20);
            this.button3.MinimumSize = new System.Drawing.Size(28, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(28, 20);
            this.button3.TabIndex = 6;
            this.button3.Text = "ок";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(155, 43);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.MaximumSize = new System.Drawing.Size(151, 15);
            this.label6.MinimumSize = new System.Drawing.Size(151, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Добавить существующую";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(4, 41);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.MaximumSize = new System.Drawing.Size(105, 23);
            this.textBox2.MinimumSize = new System.Drawing.Size(105, 23);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(105, 23);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "Добавить новую";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(144, 19);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "/";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(112, 16);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.MaximumSize = new System.Drawing.Size(28, 20);
            this.button2.MinimumSize = new System.Drawing.Size(28, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(28, 20);
            this.button2.TabIndex = 2;
            this.button2.Text = "ок";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(155, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.MaximumSize = new System.Drawing.Size(151, 15);
            this.label3.MinimumSize = new System.Drawing.Size(151, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Добавить существующую";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(4, 16);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.MaximumSize = new System.Drawing.Size(105, 23);
            this.textBox1.MinimumSize = new System.Drawing.Size(105, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(105, 23);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Добавить новую";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button13);
            this.tabPage2.Controls.Add(this.textBox4);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(307, 138);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Изменить позицию точки";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(130, 56);
            this.button13.Margin = new System.Windows.Forms.Padding(2);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(28, 20);
            this.button13.TabIndex = 45;
            this.button13.Text = "ок";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(13, 58);
            this.textBox4.Margin = new System.Windows.Forms.Padding(2);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(106, 23);
            this.textBox4.TabIndex = 1;
            this.textBox4.Text = "Ввести координаты";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 16);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 15);
            this.label9.TabIndex = 0;
            this.label9.Text = "Выбрать точку";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1182, 637);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1180, 634);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public PictureBox pictureBox2;
        private Button button5;
        private Button button6;
        private Button button4;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private ComboBox comboBox5;
        private Panel panel1;
        private Button button11;
        private Button button12;
        private MenuStrip menuStrip1;
        private Button button1;
        private ToolStripMenuItem методРендераToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem Polys;
        private ToolStripMenuItem Lines;
        private ToolStripMenuItem Points;
        private ToolStripMenuItem выборЭлементовToolStripMenuItem;
        private ToolStripMenuItem растеризацияToolStripMenuItem;
        private ToolStripMenuItem Interpolation;
        private ToolStripMenuItem Bariscentric;
        private ToolStripMenuItem NoCutter;
        private ToolStripMenuItem трассировкаЛучейToolStripMenuItem;
        private ToolStripMenuItem Parallel;
        private ToolStripMenuItem StepbyStep;
        private Label label1;
        public Label label2;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Label label7;
        private Button button7;
        private Label label8;
        private TextBox textBox3;
        private Label label5;
        private Button button3;
        private Label label6;
        private TextBox textBox2;
        private Label label4;
        private Button button2;
        private Label label3;
        private TextBox textBox1;
        private TabPage tabPage2;
        private Button button8;
        private Label label9;
        private Button button13;
        private TextBox textBox4;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem открытьToolStripMenuItem;
        private ToolStripMenuItem сохранитьToolStripMenuItem;
        private OpenFileDialog openFileDialog1;
    }
}