namespace QuickTick
{
    partial class RegistryEditor
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
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            numericUpDown1 = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(12, 12);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(428, 424);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(497, 48);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(291, 23);
            textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(497, 77);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(291, 23);
            textBox2.TabIndex = 2;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(497, 106);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(291, 23);
            textBox3.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(446, 51);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 4;
            label1.Text = "Name :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(454, 80);
            label2.Name = "label2";
            label2.Size = new Size(37, 15);
            label2.TabIndex = 5;
            label2.Text = "Type :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(450, 109);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 6;
            label3.Text = "Value :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(446, 12);
            label4.Name = "label4";
            label4.Size = new Size(342, 15);
            label4.TabIndex = 7;
            label4.Text = "Registry entry editing is recommended for advanced users only!";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(497, 106);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(111, 23);
            numericUpDown1.TabIndex = 8;
            numericUpDown1.Visible = false;
            // 
            // RegistryEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(numericUpDown1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(listBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "RegistryEditor";
            Text = "Registry Editor";
            FormClosing += RegistryEditor_FormClosing;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private NumericUpDown numericUpDown1;
    }
}