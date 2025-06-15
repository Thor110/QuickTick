namespace QuickTick
{
    partial class MainForm
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
            listBox1 = new ListBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            pictureBox1 = new PictureBox();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(12, 12);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(324, 424);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Location = new Point(342, 146);
            button1.Name = "button1";
            button1.Size = new Size(108, 23);
            button1.TabIndex = 1;
            button1.Text = "Launch Game";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Enabled = false;
            button2.Location = new Point(342, 175);
            button2.Name = "button2";
            button2.Size = new Size(108, 23);
            button2.TabIndex = 2;
            button2.Text = "Registry Entries";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Enabled = false;
            button3.Location = new Point(342, 204);
            button3.Name = "button3";
            button3.Size = new Size(108, 23);
            button3.TabIndex = 3;
            button3.Text = "Launch Editor";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Enabled = false;
            button4.Location = new Point(456, 146);
            button4.Name = "button4";
            button4.Size = new Size(108, 23);
            button4.TabIndex = 4;
            button4.Text = "Game Fix";
            button4.UseVisualStyleBackColor = true;
            button4.Visible = false;
            button4.Click += button4_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(342, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(512, 128);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // button5
            // 
            button5.Location = new Point(456, 204);
            button5.Name = "button5";
            button5.Size = new Size(108, 23);
            button5.TabIndex = 6;
            button5.Text = "Editor Fix";
            button5.UseVisualStyleBackColor = true;
            button5.Visible = false;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Enabled = false;
            button6.Location = new Point(342, 233);
            button6.Name = "button6";
            button6.Size = new Size(108, 23);
            button6.TabIndex = 7;
            button6.Text = "Suggested Mods";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Enabled = false;
            button7.Location = new Point(456, 175);
            button7.Name = "button7";
            button7.Size = new Size(108, 23);
            button7.TabIndex = 8;
            button7.Text = "Game INI";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(868, 450);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(pictureBox1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(listBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Quick Tick";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private PictureBox pictureBox1;
        private Button button5;
        private Button button6;
        private Button button7;
    }
}