using MyForms = System.Windows.Forms;

namespace DailyDevJourney
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }



        private void SelectFolder_Click(object sender, EventArgs e)
        {
            MyForms.FolderBrowserDialog folderBrowserDialog = new MyForms.FolderBrowserDialog();
            folderBrowserDialog.InitialDirectory = "D:\\Dev\\DailyDevJourney";
            MyForms.DialogResult Result = folderBrowserDialog.ShowDialog();

            if (Result == MyForms.DialogResult.OK)
            {
                FolderPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void CreateFolder_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(FolderPath.Text))
            {
                string[] Folders = Directory.GetDirectories(FolderPath.Text);
                int lastfoldernum = Folders.Length;

                lastfoldernum++;

                Directory.CreateDirectory(FolderPath.Text + Path.DirectorySeparatorChar + lastfoldernum);
            }
            else
            {
                MessageBox.Show("No path selected yet");
            }
        }

        private void FolderPath_Click(object sender, EventArgs e)
        {
            
        }
    }
}
