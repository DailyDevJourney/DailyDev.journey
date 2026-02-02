using OneDayOneDev;
using MyForms = System.Windows.Forms;

namespace DailyDevJourney
{
    public partial class Form1 : Form
    {
        public readonly FileHandler fileHandler;

        public string FolderPathstring { get; set; }
        public Form1()
        {
            InitializeComponent();
            fileHandler = new FileHandler();
            this.FormClosing += Form1_FormClosing;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FolderPathstring = fileHandler.LoadFolderPath();

            ValidationOfSaveFolder();
        }




        private void SelectFolder_Click(object sender, EventArgs e)
        {
            MyForms.FolderBrowserDialog folderBrowserDialog = new MyForms.FolderBrowserDialog();
            folderBrowserDialog.InitialDirectory = FolderPathstring;
            MyForms.DialogResult Result = folderBrowserDialog.ShowDialog();

            if (Result == MyForms.DialogResult.OK)
            {

                FolderPathstring = folderBrowserDialog.SelectedPath;
                FolderPath.Text = FolderPathstring;
                ValidationOfSaveFolder();



            }
        }

        private void CreateFolder_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(FolderPath.Text) && FolderPath.ForeColor == Color.Green)
            {
                var folderName = FolderPathstring + Path.DirectorySeparatorChar + (Directory.GetDirectories(FolderPathstring).Length + 1);
                if (Directory.CreateDirectory(folderName) != null)
                {
                    toastaffiche($"{folderName} créer");
                    label1.Text = $"Nombre de dossier : {Directory.GetDirectories(FolderPathstring).Length}";
                }
            }
            else
            {
                if (FolderPath.ForeColor == Color.Red)
                {
                    MessageBox.Show("The folder path does not exist");
                }
                if (string.IsNullOrEmpty(FolderPath.Text))
                {
                    MessageBox.Show("No path selected yet");
                }

            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Si fermeture via la croix, Alt+F4, fermeture Windows, etc.
            if (e.CloseReason == CloseReason.UserClosing)
            {
                fileHandler.SaveCreationPath(FolderPathstring);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toastaffiche(string message)
        {
            notifyIcon1.Visible = true;
            notifyIcon1.Icon = SystemIcons.Information;

            notifyIcon1.BalloonTipTitle = "Information";
            notifyIcon1.BalloonTipText = message;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;

            notifyIcon1.ShowBalloonTip(3000); 
        }

        private void ValidationOfSaveFolder()
        {
            if (!string.IsNullOrWhiteSpace(FolderPathstring))
            {
                FolderPath.Text = FolderPathstring;

                FolderPath.ForeColor = Directory.Exists(FolderPathstring) ? Color.Green : Color.Red;
                toastaffiche($"{FolderPathstring} Valide");
                label1.Text = $"Nombre de dossier : {Directory.GetDirectories(FolderPathstring).Length}";

            }
        }
    }
}
