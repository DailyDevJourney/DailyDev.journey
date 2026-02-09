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
            SuspendLayout();
            // 
            // BTNAdd
            // 
            BTNAdd.Location = new Point(429, 393);
            BTNAdd.Name = "BTNAdd";
            BTNAdd.Size = new Size(150, 46);
            BTNAdd.TabIndex = 0;
            BTNAdd.Text = "Ajouter";
            BTNAdd.UseVisualStyleBackColor = true;
            BTNAdd.Click += BTNAdd_Click;
            // 
            // DueDateTextBox
            // 
            DueDateTextBox.Location = new Point(30, 191);
            DueDateTextBox.Mask = "00/00/0000";
            DueDateTextBox.Name = "DueDateTextBox";
            DueDateTextBox.Size = new Size(549, 39);
            DueDateTextBox.TabIndex = 1;
            DueDateTextBox.ValidatingType = typeof(DateTime);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 113);
            label1.Name = "label1";
            label1.Size = new Size(344, 64);
            label1.TabIndex = 2;
            label1.Text = "Date échéance :\r\nsi vide alors pas de d'échéance";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 19);
            label2.Name = "label2";
            label2.Size = new Size(74, 32);
            label2.TabIndex = 3;
            label2.Text = "Titre :";
            // 
            // TitleTextBox
            // 
            TitleTextBox.Location = new Point(30, 54);
            TitleTextBox.Name = "TitleTextBox";
            TitleTextBox.Size = new Size(549, 39);
            TitleTextBox.TabIndex = 4;
            // 
            // BTNCancel
            // 
            BTNCancel.Location = new Point(248, 393);
            BTNCancel.Name = "BTNCancel";
            BTNCancel.Size = new Size(150, 46);
            BTNCancel.TabIndex = 5;
            BTNCancel.Text = "Annuler";
            BTNCancel.UseVisualStyleBackColor = true;
            BTNCancel.Click += BTNCancel_Click;
            // 
            // ProprietyComboBox
            // 
            ProprietyComboBox.FormattingEnabled = true;
            ProprietyComboBox.Location = new Point(30, 334);
            ProprietyComboBox.Name = "ProprietyComboBox";
            ProprietyComboBox.Size = new Size(549, 40);
            ProprietyComboBox.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 288);
            label3.Name = "label3";
            label3.Size = new Size(102, 32);
            label3.TabIndex = 7;
            label3.Text = "Priorité :";
            // 
            // OverCheckBox
            // 
            OverCheckBox.AutoSize = true;
            OverCheckBox.Location = new Point(30, 403);
            OverCheckBox.Name = "OverCheckBox";
            OverCheckBox.Size = new Size(152, 36);
            OverCheckBox.TabIndex = 8;
            OverCheckBox.Text = " Terminée";
            OverCheckBox.UseVisualStyleBackColor = true;
            // 
            // Ajout
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(616, 462);
            Controls.Add(OverCheckBox);
            Controls.Add(label3);
            Controls.Add(ProprietyComboBox);
            Controls.Add(BTNCancel);
            Controls.Add(TitleTextBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(DueDateTextBox);
            Controls.Add(BTNAdd);
            Name = "Ajout";
            Text = "Ajout";
            Load += Ajout_Load;
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
    }
}