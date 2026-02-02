using System.Text;
using static System.Windows.Forms.LinkLabel;

namespace OneDayOneDev
{
    public class FileHandler
    {
        readonly string CreationSavePath = $"{AppContext.BaseDirectory}CheminDossier.txt";

        public FileHandler()
        {
        }
        public string LoadFolderPath()
        {
            if (!File.Exists(CreationSavePath))
            {
                var myFile = File.Create(CreationSavePath);
                myFile.Close();
            }

            return File.ReadLines(CreationSavePath).FirstOrDefault();
        }

        public void SaveCreationPath(string FolderPath)
        {
            
            if (!string.IsNullOrWhiteSpace(FolderPath))
            {
                File.WriteAllText(CreationSavePath,
                    contents:  FolderPath);
            }
                

        }

    }
}
