namespace DailyDevJourney
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
            components = new System.ComponentModel.Container();
            SelectFolder = new Button();
            FolderPath = new Label();
            CreateFolder = new Button();
            label1 = new Label();
            notifyIcon1 = new NotifyIcon(components);
            SuspendLayout();
            // 
            // SelectFolder
            // 
            SelectFolder.Location = new Point(28, 12);
            SelectFolder.Name = "SelectFolder";
            SelectFolder.Size = new Size(658, 46);
            SelectFolder.TabIndex = 0;
            SelectFolder.Text = " Select folder";
            SelectFolder.UseVisualStyleBackColor = true;
            SelectFolder.Click += SelectFolder_Click;
            // 
            // FolderPath
            // 
            FolderPath.AutoSize = true;
            FolderPath.Location = new Point(28, 122);
            FolderPath.Name = "FolderPath";
            FolderPath.Size = new Size(136, 32);
            FolderPath.TabIndex = 1;
            FolderPath.Text = "Folder path";
            // 
            // CreateFolder
            // 
            CreateFolder.Location = new Point(28, 64);
            CreateFolder.Name = "CreateFolder";
            CreateFolder.Size = new Size(658, 46);
            CreateFolder.TabIndex = 2;
            CreateFolder.Text = " Create a new folder here";
            CreateFolder.UseVisualStyleBackColor = true;
            CreateFolder.Click += CreateFolder_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 170);
            label1.Name = "label1";
            label1.Size = new Size(78, 32);
            label1.TabIndex = 3;
            label1.Text = "label1";
            label1.Click += label1_Click;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(738, 211);
            Controls.Add(label1);
            Controls.Add(CreateFolder);
            Controls.Add(FolderPath);
            Controls.Add(SelectFolder);
            Name = "Form1";
            Text = "DailyDevJourney";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SelectFolder;
        private Label FolderPath;
        private Button CreateFolder;
        private Label label1;
        private NotifyIcon notifyIcon1;
    }
}
