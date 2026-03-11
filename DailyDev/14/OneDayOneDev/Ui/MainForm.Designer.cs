

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
            components = new System.ComponentModel.Container();
            BTNAjouter = new Button();
            ListeTache = new DataGridView();
            BTNModifier = new Button();
            BTNDelete = new Button();
            BTNOver = new Button();
            BTNUndo = new Button();
            BTNRedo = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            UserNameTextBox = new TextBox();
            ConnectionButton = new Button();
            PasswordTextBox = new TextBox();
            apiexpiretime = new System.Windows.Forms.Timer(components);
            labelSession = new Label();
            ((System.ComponentModel.ISupportInitialize)ListeTache).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
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
            ListeTache.Dock = DockStyle.Bottom;
            ListeTache.Location = new Point(2, 6);
            ListeTache.Margin = new Padding(2, 1, 2, 1);
            ListeTache.Name = "ListeTache";
            ListeTache.ReadOnly = true;
            ListeTache.RowHeadersWidth = 82;
            ListeTache.Size = new Size(711, 190);
            ListeTache.TabIndex = 5;
            ListeTache.CellContentClick += ListeTache_CellContentClick;
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
            tableLayoutPanel1.Size = new Size(715, 39);
            tableLayoutPanel1.TabIndex = 11;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(ListeTache, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(0, 39);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(715, 217);
            tableLayoutPanel2.TabIndex = 12;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 3;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.1278763F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.8721237F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 323F));
            tableLayoutPanel4.Controls.Add(label1, 0, 0);
            tableLayoutPanel4.Controls.Add(label2, 1, 0);
            tableLayoutPanel4.Controls.Add(UserNameTextBox, 0, 1);
            tableLayoutPanel4.Controls.Add(ConnectionButton, 2, 1);
            tableLayoutPanel4.Controls.Add(PasswordTextBox, 1, 1);
            tableLayoutPanel4.Controls.Add(labelSession, 2, 0);
            tableLayoutPanel4.Dock = DockStyle.Top;
            tableLayoutPanel4.Location = new Point(0, 256);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 3;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 37.878788F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 62.121212F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel4.Size = new Size(715, 90);
            tableLayoutPanel4.TabIndex = 13;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(102, 15);
            label1.TabIndex = 0;
            label1.Text = " Nom d'utilisateur";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(199, 0);
            label2.Name = "label2";
            label2.Size = new Size(80, 15);
            label2.TabIndex = 1;
            label2.Text = " Mot de passe";
            // 
            // UserNameTextBox
            // 
            UserNameTextBox.Location = new Point(3, 25);
            UserNameTextBox.Name = "UserNameTextBox";
            UserNameTextBox.Size = new Size(171, 23);
            UserNameTextBox.TabIndex = 2;
            // 
            // ConnectionButton
            // 
            ConnectionButton.Location = new Point(394, 25);
            ConnectionButton.Name = "ConnectionButton";
            ConnectionButton.Size = new Size(318, 28);
            ConnectionButton.TabIndex = 4;
            ConnectionButton.Text = " Connection";
            ConnectionButton.UseVisualStyleBackColor = true;
            ConnectionButton.Click += ConnectionButton_Click;
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(199, 25);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.Size = new Size(185, 23);
            PasswordTextBox.TabIndex = 3;
            // 
            // apiexpiretime
            // 
            apiexpiretime.Enabled = true;
            apiexpiretime.Interval = 1000;
            apiexpiretime.Tick += apiexpiretime_Tick;
            // 
            // labelSession
            // 
            labelSession.AutoSize = true;
            labelSession.Location = new Point(394, 0);
            labelSession.Name = "labelSession";
            labelSession.Size = new Size(0, 15);
            labelSession.TabIndex = 5;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(715, 347);
            Controls.Add(tableLayoutPanel4);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(2, 1, 2, 1);
            Name = "MainForm";
            Text = "Task manager";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)ListeTache).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
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
        private TableLayoutPanel tableLayoutPanel4;
        private Label label1;
        private Label label2;
        private TextBox UserNameTextBox;
        private TextBox PasswordTextBox;
        private Button ConnectionButton;
        private System.Windows.Forms.Timer apiexpiretime;
        private Label labelSession;
    }
}
