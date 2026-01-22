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
            SelectFolder = new Button();
            FolderPath = new Label();
            CreateFolder = new Button();
            SuspendLayout();
            // 
            // SelectFolder
            // 
            SelectFolder.Location = new Point(83, 27);
            SelectFolder.Name = "SelectFolder";
            SelectFolder.Size = new Size(192, 46);
            SelectFolder.TabIndex = 0;
            SelectFolder.Text = " Select folder";
            SelectFolder.UseVisualStyleBackColor = true;
            SelectFolder.Click += SelectFolder_Click;
            // 
            // FolderPath
            // 
            FolderPath.AutoSize = true;
            FolderPath.Location = new Point(87, 85);
            FolderPath.Name = "FolderPath";
            FolderPath.Size = new Size(0, 32);
            FolderPath.TabIndex = 1;
            FolderPath.Click += FolderPath_Click;
            // 
            // CreateFolder
            // 
            CreateFolder.Location = new Point(83, 379);
            CreateFolder.Name = "CreateFolder";
            CreateFolder.Size = new Size(658, 46);
            CreateFolder.TabIndex = 2;
            CreateFolder.Text = " Create a new folder here";
            CreateFolder.UseVisualStyleBackColor = true;
            CreateFolder.Click += CreateFolder_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}
