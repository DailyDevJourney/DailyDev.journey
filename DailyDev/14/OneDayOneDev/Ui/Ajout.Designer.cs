namespace OneDayOneDev_DayThirteen
{
    partial class Ajout
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
            BTNAdd = new Button();
            DueDateTextBox = new MaskedTextBox();
            label1 = new Label();
            label2 = new Label();
            TitleTextBox = new TextBox();
            BTNCancel = new Button();
            ProprietyComboBox = new ComboBox();
            label3 = new Label();
            OverCheckBox = new CheckBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // BTNAdd
            // 
            BTNAdd.Location = new Point(177, 1);
            BTNAdd.Margin = new Padding(2, 1, 2, 1);
            BTNAdd.Name = "BTNAdd";
            BTNAdd.Size = new Size(93, 27);
            BTNAdd.TabIndex = 0;
            BTNAdd.Text = "Ajouter";
            BTNAdd.UseVisualStyleBackColor = true;
            BTNAdd.Click += BTNAdd_Click;
            // 
            // DueDateTextBox
            // 
            DueDateTextBox.Location = new Point(2, 105);
            DueDateTextBox.Margin = new Padding(2, 1, 2, 1);
            DueDateTextBox.Mask = "00/00/0000";
            DueDateTextBox.Name = "DueDateTextBox";
            DueDateTextBox.Size = new Size(297, 23);
            DueDateTextBox.TabIndex = 1;
            DueDateTextBox.ValidatingType = typeof(DateTime);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2, 51);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(168, 30);
            label1.TabIndex = 2;
            label1.Text = "Date échéance :\r\nsi vide alors pas de d'échéance";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(2, 0);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(37, 14);
            label2.TabIndex = 3;
            label2.Text = "Titre :";
            // 
            // TitleTextBox
            // 
            TitleTextBox.Location = new Point(2, 15);
            TitleTextBox.Margin = new Padding(2, 1, 2, 1);
            TitleTextBox.Name = "TitleTextBox";
            TitleTextBox.Size = new Size(297, 23);
            TitleTextBox.TabIndex = 4;
            // 
            // BTNCancel
            // 
            BTNCancel.Location = new Point(2, 1);
            BTNCancel.Margin = new Padding(2, 1, 2, 1);
            BTNCancel.Name = "BTNCancel";
            BTNCancel.Size = new Size(101, 27);
            BTNCancel.TabIndex = 5;
            BTNCancel.Text = "Annuler";
            BTNCancel.UseVisualStyleBackColor = true;
            BTNCancel.Click += BTNCancel_Click;
            // 
            // ProprietyComboBox
            // 
            ProprietyComboBox.FormattingEnabled = true;
            ProprietyComboBox.Location = new Point(2, 183);
            ProprietyComboBox.Margin = new Padding(2, 1, 2, 1);
            ProprietyComboBox.Name = "ProprietyComboBox";
            ProprietyComboBox.Size = new Size(297, 23);
            ProprietyComboBox.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(2, 156);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 7;
            label3.Text = "Priorité :";
            // 
            // OverCheckBox
            // 
            OverCheckBox.AutoSize = true;
            OverCheckBox.Location = new Point(2, 224);
            OverCheckBox.Margin = new Padding(2, 1, 2, 1);
            OverCheckBox.Name = "OverCheckBox";
            OverCheckBox.Size = new Size(78, 19);
            OverCheckBox.TabIndex = 8;
            OverCheckBox.Text = " Terminée";
            OverCheckBox.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(label2, 0, 0);
            tableLayoutPanel1.Controls.Add(label1, 0, 2);
            tableLayoutPanel1.Controls.Add(OverCheckBox, 0, 6);
            tableLayoutPanel1.Controls.Add(TitleTextBox, 0, 1);
            tableLayoutPanel1.Controls.Add(DueDateTextBox, 0, 3);
            tableLayoutPanel1.Controls.Add(label3, 0, 4);
            tableLayoutPanel1.Controls.Add(ProprietyComboBox, 0, 5);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 28.787878F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 71.21212F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 41F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.Size = new Size(351, 264);
            tableLayoutPanel1.TabIndex = 9;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(BTNCancel, 0, 0);
            tableLayoutPanel2.Controls.Add(BTNAdd, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Bottom;
            tableLayoutPanel2.Location = new Point(0, 328);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(351, 29);
            tableLayoutPanel2.TabIndex = 10;
            // 
            // Ajout
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(351, 357);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(2, 1, 2, 1);
            Name = "Ajout";
            Text = "Ajout";
            Load += Ajout_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BTNAdd;
        private MaskedTextBox DueDateTextBox;
        private Label label1;
        private Label label2;
        private TextBox TitleTextBox;
        private Button BTNCancel;
        private ComboBox ProprietyComboBox;
        private Label label3;
        private CheckBox OverCheckBox;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}