using OneDayOneDev_DayThirteen;

namespace OneDayOneDev
{
    public partial class MainForm : Form
    {
        private readonly SystemDateTimeProvider _dateTimeProvider;
        private readonly TaskService taskService;
        public MainForm(TaskService taskService, SystemDateTimeProvider _dateTimeProvider)
        {

            InitializeComponent();

            this._dateTimeProvider = _dateTimeProvider;
            this.taskService = taskService;
            

            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "OneDayOneDev";
            ListeTache.DataSource = this.taskService.GetTaskList();
            ListeTache.AutoGenerateColumns = true;
            

        }

        private void RafraichirList()
        {


            ListeTache.DataSource = this.taskService.GetTaskList();

        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            RafraichirList();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            taskService.SaveData();
        }

        private void BTNAjouter_Click(object sender ,EventArgs e)
        {
            using(var addForm = new Ajout(this.taskService,this._dateTimeProvider))
            {
                addForm.Text = "Ajouter une tâche";
                addForm.ShowDialog(this);
                RafraichirList();
            }
            
        }
        private void BTNModifier_Click(object sender ,EventArgs e)
        {
            DataGridViewRow row = null;
            if (ListeTache.CurrentCell != null)
            {
                var actualPlace = ListeTache.CurrentCell.RowIndex;
                row = ListeTache.Rows[actualPlace];
            }
            else if (ListeTache.CurrentRow != null)
            {
                row = ListeTache.CurrentRow;
            }


            if (row != null)
            {
                var parsable = int.TryParse(row?.Cells["id"].Value.ToString(), out var id);

                if (parsable)
                {

                    using (var addForm = new Ajout(this.taskService, this._dateTimeProvider,this.taskService.GetTaskById(id)))
                    {
                        addForm.Text = "Modifier une tâche";
                        addForm.ShowDialog(this);
                        RafraichirList();
                    }
                }


            }

            
        }
        private void BTNDelete_Click(object sender ,EventArgs e)
        {
            DataGridViewRow row = null;
            if (ListeTache.CurrentCell != null)
            {
                var actualPlace = ListeTache.CurrentCell.RowIndex;
                row = ListeTache.Rows[actualPlace];
            }
            else if (ListeTache.CurrentRow != null)
            {
                row = ListeTache.CurrentRow;
            }


            if (row != null)
            {
                var parsable = int.TryParse(row?.Cells["id"].Value.ToString(),out var id);

                if (parsable)
                {

                    var result = MessageBox.Show($"souhaitez-vous supprimer la tâche n° {id}?", "Confirmation demandée", MessageBoxButtons.OKCancel);
                    if(result == DialogResult.OK)
                    {
                        var deletetask = taskService.DeleteTask(id);
                        MessageBox.Show(deletetask.message);
                    }
                    
                }

                
            }

            
            RafraichirList();
        }
    }
}
