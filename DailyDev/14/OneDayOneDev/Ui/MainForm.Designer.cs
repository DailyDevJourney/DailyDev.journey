

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
            ListeTache = new DataGridView();
            BTNModifier = new Button();
            BTNDelete = new Button();
            BTNOver = new Button();
            BTNUndo = new Button();
            BTNRedo = new Button();
            ((System.ComponentModel.ISupportInitialize)ListeTache).BeginInit();
            SuspendLayout();
            // 
            // BTNAjouter
            // 
            BTNAjouter.Location = new Point(11, 15);
            BTNAjouter.Margin = new Padding(2, 1, 2, 1);
            BTNAjouter.Name = "BTNAjouter";
            BTNAjouter.Size = new Size(195, 25);
            BTNAjouter.TabIndex = 3;
            BTNAjouter.Text = "Ajouter une tâche";
            BTNAjouter.UseVisualStyleBackColor = true;
            BTNAjouter.Click += BTNAjouter_Click;
            // 
            // ListeTache
            // 
            ListeTache.AllowUserToAddRows = false;
            ListeTache.AllowUserToDeleteRows = false;
            ListeTache.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ListeTache.Location = new Point(11, 124);
            ListeTache.Margin = new Padding(2, 1, 2, 1);
            ListeTache.Name = "ListeTache";
            ListeTache.ReadOnly = true;
            ListeTache.RowHeadersWidth = 82;
            ListeTache.Size = new Size(519, 165);
            ListeTache.TabIndex = 5;
            ListeTache.CellDoubleClick += ListeTache_DoubleClic;
            // 
            // BTNModifier
            // 
            BTNModifier.Location = new Point(11, 68);
            BTNModifier.Margin = new Padding(2, 1, 2, 1);
            BTNModifier.Name = "BTNModifier";
            BTNModifier.Size = new Size(195, 25);
            BTNModifier.TabIndex = 6;
            BTNModifier.Text = "Modifier une tâche";
            BTNModifier.UseVisualStyleBackColor = true;
            BTNModifier.Click += BTNModifier_Click;
            // 
            // BTNDelete
            // 
            BTNDelete.Location = new Point(11, 96);
            BTNDelete.Margin = new Padding(2, 1, 2, 1);
            BTNDelete.Name = "BTNDelete";
            BTNDelete.Size = new Size(195, 25);
            BTNDelete.TabIndex = 7;
            BTNDelete.Text = "Supprimer une tâche";
            BTNDelete.UseVisualStyleBackColor = true;
            BTNDelete.Click += BTNDelete_Click;
            // 
            // BTNOver
            // 
            BTNOver.Location = new Point(11, 43);
            BTNOver.Margin = new Padding(2, 1, 2, 1);
            BTNOver.Name = "BTNOver";
            BTNOver.Size = new Size(195, 22);
            BTNOver.TabIndex = 8;
            BTNOver.Text = "Terminée une tâche";
            BTNOver.UseVisualStyleBackColor = true;
            BTNOver.Click += BTNOver_Click;
            // 
            // BTNUndo
            // 
            BTNUndo.Location = new Point(455, 96);
            BTNUndo.Name = "BTNUndo";
            BTNUndo.Size = new Size(75, 23);
            BTNUndo.TabIndex = 9;
            BTNUndo.Text = " Undo";
            BTNUndo.UseVisualStyleBackColor = true;
            BTNUndo.Click += BTNUndo_Click;
            // 
            // BTNRedo
            // 
            BTNRedo.Location = new Point(374, 96);
            BTNRedo.Name = "BTNRedo";
            BTNRedo.Size = new Size(75, 23);
            BTNRedo.TabIndex = 10;
            BTNRedo.Text = " Redo";
            BTNRedo.UseVisualStyleBackColor = true;
            BTNRedo.Click += BTNRedo_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(537, 295);
            Controls.Add(BTNRedo);
            Controls.Add(BTNUndo);
            Controls.Add(BTNOver);
            Controls.Add(BTNDelete);
            Controls.Add(BTNModifier);
            Controls.Add(ListeTache);
            Controls.Add(BTNAjouter);
            Margin = new Padding(2, 1, 2, 1);
            Name = "MainForm";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)ListeTache).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button BTNAjouter;
        private DataGridView ListeTache;
        private Button BTNModifier;
        private Button BTNDelete;
        private Button BTNOver;
        private Button BTNUndo;
        private Button BTNRedo;
    }
}
