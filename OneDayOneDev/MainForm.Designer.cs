

using Microsoft.VisualBasic;

namespace OneDayOneDev
{
    partial class MainForm
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
            BTNAjouter = new Button();
            button2 = new Button();
            ListeTache = new DataGridView();
            BTNModifier = new Button();
            BTNDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)ListeTache).BeginInit();
            SuspendLayout();
            // 
            // BTNAjouter
            // 
            BTNAjouter.Location = new Point(21, 12);
            BTNAjouter.Name = "BTNAjouter";
            BTNAjouter.Size = new Size(363, 54);
            BTNAjouter.TabIndex = 3;
            BTNAjouter.Text = "Ajouter une tâche";
            BTNAjouter.UseVisualStyleBackColor = true;
            BTNAjouter.Click += BTNAjouter_Click;
            // 
            // button2
            // 
            button2.Location = new Point(835, 161);
            button2.Name = "button2";
            button2.Size = new Size(150, 46);
            button2.TabIndex = 4;
            button2.Text = "Rafraichir";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // ListeTache
            // 
            ListeTache.AllowUserToAddRows = false;
            ListeTache.AllowUserToDeleteRows = false;
            ListeTache.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ListeTache.Location = new Point(21, 213);
            ListeTache.Name = "ListeTache";
            ListeTache.ReadOnly = true;
            ListeTache.RowHeadersWidth = 82;
            ListeTache.Size = new Size(964, 404);
            ListeTache.TabIndex = 5;
            // 
            // BTNModifier
            // 
            BTNModifier.Location = new Point(21, 72);
            BTNModifier.Name = "BTNModifier";
            BTNModifier.Size = new Size(363, 54);
            BTNModifier.TabIndex = 6;
            BTNModifier.Text = "Modifier une tâche";
            BTNModifier.UseVisualStyleBackColor = true;
            BTNModifier.Click += BTNModifier_Click;
            // 
            // BTNDelete
            // 
            BTNDelete.Location = new Point(21, 132);
            BTNDelete.Name = "BTNDelete";
            BTNDelete.Size = new Size(363, 54);
            BTNDelete.TabIndex = 7;
            BTNDelete.Text = "Supprimer une tâche";
            BTNDelete.UseVisualStyleBackColor = true;
            BTNDelete.Click += BTNDelete_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(997, 629);
            Controls.Add(BTNDelete);
            Controls.Add(BTNModifier);
            Controls.Add(ListeTache);
            Controls.Add(button2);
            Controls.Add(BTNAjouter);
            Name = "MainForm";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)ListeTache).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button BTNAjouter;
        private Button button2;
        private DataGridView ListeTache;
        private Button BTNModifier;
        private Button BTNDelete;
    }
}
