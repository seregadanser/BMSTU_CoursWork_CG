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
            this.button9 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button10 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
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
            this.numericUpDown11 = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.numericUpDown12 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown13 = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown13)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.Location = new System.Drawing.Point(8, 119);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(831, 339);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            this.pictureBox2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDoubleClick);
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            this.pictureBox2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseMove);
            this.pictureBox2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseUp);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(858, 49);
            this.button6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(71, 48);
            this.button6.TabIndex = 20;
            this.button6.Text = "Сделать активным";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // comboBox5
            // 
            this.comboBox5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Location = new System.Drawing.Point(857, 25);
            this.comboBox5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(129, 23);
            this.comboBox5.TabIndex = 27;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(843, 263);
            this.button5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(125, 20);
            this.button5.TabIndex = 20;
            this.button5.Text = "Возврат фигуры";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(843, 215);
            this.button4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(235, 20);
            this.button4.TabIndex = 21;
            this.button4.Text = "Выбрать трансформирование";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(843, 239);
            this.button9.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(235, 20);
            this.button9.TabIndex = 22;
            this.button9.Text = "Применить трансформирование";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(865, 379);
            this.button10.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(127, 20);
            this.button10.TabIndex = 23;
            this.button10.Text = "загрузить модель";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Location = new System.Drawing.Point(8, 155);
            this.panel1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0, 0);
            this.panel1.TabIndex = 29;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(813, 543);
            this.button11.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(115, 43);
            this.button11.TabIndex = 31;
            this.button11.Text = "Остановить отрисовку";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(933, 543);
            this.button12.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(94, 43);
            this.button12.TabIndex = 32;
            this.button12.Text = "Возобновить отрисовку";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(843, 474);
            this.button13.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(190, 23);
            this.button13.TabIndex = 33;
            this.button13.Text = "Список активных элементов";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.методРендераToolStripMenuItem,
            this.выборЭлементовToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1166, 24);
            this.menuStrip1.TabIndex = 34;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
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
            this.выборЭлементовToolStripMenuItem.Click += new System.EventHandler(this.выборЭлементовToolStripMenuItem_Click);
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
            this.button1.Location = new System.Drawing.Point(843, 287);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 20);
            this.button1.TabIndex = 35;
            this.button1.Text = "Возврат камеры";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 478);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 15);
            this.label1.TabIndex = 36;
            this.label1.Text = "Время отрисовки предыдущей сцены:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(241, 478);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 37;
            this.label2.Text = "label2";
            // 
            // numericUpDown11
            // 
            this.numericUpDown11.Location = new System.Drawing.Point(475, 500);
            this.numericUpDown11.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown11.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown11.Name = "numericUpDown11";
            this.numericUpDown11.Size = new System.Drawing.Size(126, 23);
            this.numericUpDown11.TabIndex = 39;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(613, 526);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 15);
            this.label13.TabIndex = 43;
            this.label13.Text = "Z";
            // 
            // numericUpDown12
            // 
            this.numericUpDown12.Location = new System.Drawing.Point(475, 524);
            this.numericUpDown12.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown12.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown12.Name = "numericUpDown12";
            this.numericUpDown12.Size = new System.Drawing.Size(126, 23);
            this.numericUpDown12.TabIndex = 40;
            // 
            // numericUpDown13
            // 
            this.numericUpDown13.Location = new System.Drawing.Point(475, 478);
            this.numericUpDown13.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown13.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown13.Name = "numericUpDown13";
            this.numericUpDown13.Size = new System.Drawing.Size(126, 23);
            this.numericUpDown13.TabIndex = 38;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(613, 504);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(14, 15);
            this.label14.TabIndex = 41;
            this.label14.Text = "Y";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(613, 479);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(14, 15);
            this.label15.TabIndex = 42;
            this.label15.Text = "X";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 606);
            this.Controls.Add(this.numericUpDown11);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.numericUpDown12);
            this.Controls.Add(this.numericUpDown13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox5);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown13)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private PictureBox pictureBox2;
        private Button button5;
        private Button button6;
        private Button button4;
        private Button button9;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Button button10;
        private System.Windows.Forms.Timer timer1;
        private ComboBox comboBox5;
        private Panel panel1;
        private Button button11;
        private Button button12;
        private Button button13;
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
        private NumericUpDown numericUpDown11;
        private Label label13;
        private NumericUpDown numericUpDown12;
        private NumericUpDown numericUpDown13;
        private Label label14;
        private Label label15;
    }
}