using Microsoft.EntityFrameworkCore;
using OneDayOneDev_DayThirteen;

namespace OneDayOneDev
{
    public partial class MainForm : Form
    {
        private readonly SystemDateTimeProvider _dateTimeProvider;
        private readonly TaskRules _taskRules;
        private readonly TaskRepository _taskRepository;
        public MainForm(TaskRepository _taskRepository, TaskRules _taskRules ,SystemDateTimeProvider _dateTimeProvider)
        {

            InitializeComponent();

            this._dateTimeProvider = _dateTimeProvider;
            this._taskRepository = _taskRepository;
            this._taskRules = _taskRules;


            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

            FileHandler temp = new FileHandler(_dateTimeProvider);
            var tasks = temp.LoadTaskData();
            if (tasks != null)
            {
                foreach (var task in tasks)
                {
                    this._taskRepository.AddTask(task);
                }

                temp.DeleteFile(temp.TaskDataPath);
                
            }
            this.Text = "OneDayOneDev";
            ListeTache.AutoGenerateColumns = true;
            RafraichirList();
        }

        private void RafraichirList()
        {


            ListeTache.DataSource = null;
            ListeTache.DataSource = this._taskRepository.GetAllTask();

            foreach (DataGridViewRow row in ListeTache.Rows)
            {
                var parsable = int.TryParse(row?.Cells["id"].Value.ToString(), out var id);

                if (parsable)
                {
                    var taskFromRow = this._taskRepository.GetTaskById(id);

                    if (taskFromRow != null)
                    {
                        if (this._taskRules.IsTaskLate(taskFromRow, _dateTimeProvider.Today))
                        {
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                        }

                        if (taskFromRow.Iscompleted)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightBlue;
                        }
                    }
                }
            }

        }



        private void button2_Click(object sender, EventArgs e)
        {
            RafraichirList();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private DataGridViewRow? GetSelectedRow()
        {

            DataGridViewRow? row = null;
            if (ListeTache.CurrentCell != null)
            {
                var actualPlace = ListeTache.CurrentCell.RowIndex;
                row = ListeTache.Rows[actualPlace];
            }
            else if (ListeTache.CurrentRow != null)
            {
                row = ListeTache.CurrentRow;
            }

            return row;
        }

        private void BTNAjouter_Click(object sender, EventArgs e)
        {
            using (var addForm = new Ajout(this._taskRepository, this._dateTimeProvider))
            {
                addForm.Text = "Ajouter une tâche";
                addForm.ShowDialog(this);
                RafraichirList();
            }

        }
        private void BTNModifier_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = GetSelectedRow();


            if (row != null)
            {
                var parsable = int.TryParse(row?.Cells["id"].Value.ToString(), out var id);

                if (parsable)
                {

                    using (var addForm = new Ajout(this._taskRepository, this._dateTimeProvider, this._taskRepository.GetTaskById(id)))
                    {
                        addForm.Text = "Modifier une tâche";
                        addForm.ShowDialog(this);
                        RafraichirList();
                    }
                }


            }


        }
        private void BTNDelete_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = GetSelectedRow();


            if (row != null)
            {
                var parsable = int.TryParse(row?.Cells["id"].Value.ToString(), out var id);

                if (parsable)
                {

                    var result = MessageBox.Show($"souhaitez-vous supprimer la tâche n° {id}?", "Confirmation demandée", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        var deletetask = this._taskRepository.DeleteTask(id);
                        MessageBox.Show(deletetask.message);
                    }

                }


            }


            RafraichirList();
        }


        private void BTNOver_Click(object sender, EventArgs e)
        {

            DataGridViewRow row = GetSelectedRow();

            if (row != null)
            {
                var parsable = int.TryParse(row?.Cells["id"].Value.ToString(), out var id);

                if (parsable)
                {

                    var result = MessageBox.Show($"souhaitez-vous terminée la tâche n° {id}?", "Confirmation demandée", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        var EndTask = this._taskRepository.SetTaskCompleted(id);
                        MessageBox.Show(EndTask.message);
                    }

                }


            }


            RafraichirList();
        }



        

        private void ListeTache_DoubleClic(object sender, DataGridViewCellEventArgs e)
        {
            BTNModifier_Click(sender, e);
        }
    }
}
