

using Microsoft.VisualBasic;

namespace OneDayOneDev_DayThirten
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
            TitleTextBox = new TextBox();
            PriorityComboBox = new ComboBox();
            button1 = new Button();
            button2 = new Button();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            DateTextBox = new MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // TitleTextBox
            // 
            TitleTextBox.Location = new Point(668, 3);
            TitleTextBox.Name = "TitleTextBox";
            TitleTextBox.Size = new Size(317, 39);
            TitleTextBox.TabIndex = 0;
            // 
            // PriorityComboBox
            // 
            PriorityComboBox.FormattingEnabled = true;
            PriorityComboBox.Location = new Point(668, 110);
            PriorityComboBox.Name = "PriorityComboBox";
            PriorityComboBox.Size = new Size(317, 40);
            PriorityComboBox.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(668, 161);
            button1.Name = "button1";
            button1.Size = new Size(317, 46);
            button1.TabIndex = 3;
            button1.Text = "Ajouter une tâche";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(492, 161);
            button2.Name = "button2";
            button2.Size = new Size(150, 46);
            button2.TabIndex = 4;
            button2.Text = "Rafraichir";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.ReadOnly = true;
            dataGridView1.Location = new Point(21, 213);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 82;
            dataGridView1.Size = new Size(964, 404);
            dataGridView1.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(568, 10);
            label1.Name = "label1";
            label1.Size = new Size(74, 32);
            label1.TabIndex = 6;
            label1.Text = "Titre :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(465, 62);
            label3.Name = "label3";
            label3.Size = new Size(177, 32);
            label3.TabIndex = 8;
            label3.Text = "Date échénace:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(540, 113);
            label4.Name = "label4";
            label4.Size = new Size(102, 32);
            label4.TabIndex = 9;
            label4.Text = "Priorité :";
            
            // 
            // DateTextBox
            // 
            DateTextBox.Location = new Point(668, 60);
            DateTextBox.Mask = "00/00/0000";
            DateTextBox.Name = "DateTextBox";
            DateTextBox.Size = new Size(317, 39);
            DateTextBox.TabIndex = 10;
            DateTextBox.ValidatingType = typeof(DateTime);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(997, 629);
            Controls.Add(DateTextBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(PriorityComboBox);
            Controls.Add(TitleTextBox);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TitleTextBox;
        private ComboBox PriorityComboBox;
        private Button button1;
        private Button button2;
        private DataGridView dataGridView1;
        private Label label1;
        private Label label3;
        private Label label4;
        private MaskedTextBox DateTextBox;
    }
}
