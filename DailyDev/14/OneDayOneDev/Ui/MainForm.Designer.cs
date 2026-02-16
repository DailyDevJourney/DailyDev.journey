

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
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)ListeTache).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // BTNAjouter
            // 
            BTNAjouter.Location = new Point(2, 1);
            BTNAjouter.Margin = new Padding(2, 1, 2, 1);
            BTNAjouter.Name = "BTNAjouter";
            BTNAjouter.Size = new Size(84, 29);
            BTNAjouter.TabIndex = 3;
            BTNAjouter.Text = " Add task";
            BTNAjouter.UseVisualStyleBackColor = true;
            BTNAjouter.Click += BTNAjouter_Click;
            // 
            // ListeTache
            // 
            ListeTache.AllowUserToAddRows = false;
            ListeTache.AllowUserToDeleteRows = false;
            ListeTache.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            ListeTache.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllHeaders;
            ListeTache.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ListeTache.Dock = DockStyle.Fill;
            ListeTache.Location = new Point(2, 1);
            ListeTache.Margin = new Padding(2, 1, 2, 1);
            ListeTache.Name = "ListeTache";
            ListeTache.ReadOnly = true;
            ListeTache.RowHeadersWidth = 82;
            ListeTache.Size = new Size(535, 260);
            ListeTache.TabIndex = 5;
            ListeTache.CellDoubleClick += ListeTache_DoubleClic;
            // 
            // BTNModifier
            // 
            BTNModifier.Location = new Point(90, 1);
            BTNModifier.Margin = new Padding(2, 1, 2, 1);
            BTNModifier.Name = "BTNModifier";
            BTNModifier.Size = new Size(84, 29);
            BTNModifier.TabIndex = 6;
            BTNModifier.Text = " Edit task";
            BTNModifier.UseVisualStyleBackColor = true;
            BTNModifier.Click += BTNModifier_Click;
            // 
            // BTNDelete
            // 
            BTNDelete.Location = new Point(178, 1);
            BTNDelete.Margin = new Padding(2, 1, 2, 1);
            BTNDelete.Name = "BTNDelete";
            BTNDelete.Size = new Size(84, 29);
            BTNDelete.TabIndex = 7;
            BTNDelete.Text = " Delete task";
            BTNDelete.UseVisualStyleBackColor = true;
            BTNDelete.Click += BTNDelete_Click;
            // 
            // BTNOver
            // 
            BTNOver.Location = new Point(266, 1);
            BTNOver.Margin = new Padding(2, 1, 2, 1);
            BTNOver.Name = "BTNOver";
            BTNOver.Size = new Size(84, 29);
            BTNOver.TabIndex = 8;
            BTNOver.Text = " End task";
            BTNOver.UseVisualStyleBackColor = true;
            BTNOver.Click += BTNOver_Click;
            // 
            // BTNUndo
            // 
            BTNUndo.Location = new Point(355, 3);
            BTNUndo.Name = "BTNUndo";
            BTNUndo.Size = new Size(82, 25);
            BTNUndo.TabIndex = 9;
            BTNUndo.Text = " Undo";
            BTNUndo.UseVisualStyleBackColor = true;
            BTNUndo.Click += BTNUndo_Click;
            // 
            // BTNRedo
            // 
            BTNRedo.Location = new Point(443, 3);
            BTNRedo.Name = "BTNRedo";
            BTNRedo.Size = new Size(84, 25);
            BTNRedo.TabIndex = 10;
            BTNRedo.Text = " Redo";
            BTNRedo.UseVisualStyleBackColor = true;
            BTNRedo.Click += BTNRedo_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(BTNRedo, 5, 0);
            tableLayoutPanel1.Controls.Add(BTNModifier, 1, 0);
            tableLayoutPanel1.Controls.Add(BTNDelete, 2, 0);
            tableLayoutPanel1.Controls.Add(BTNOver, 3, 0);
            tableLayoutPanel1.Controls.Add(BTNUndo, 4, 0);
            tableLayoutPanel1.Controls.Add(BTNAjouter, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(537, 33);
            tableLayoutPanel1.TabIndex = 11;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(ListeTache, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 33);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(537, 262);
            tableLayoutPanel2.TabIndex = 12;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(537, 295);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(2, 1, 2, 1);
            Name = "MainForm";
            Text = "Task manager";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)ListeTache).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
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
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}
